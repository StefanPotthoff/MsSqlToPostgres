using Microsoft.Data.SqlClient;
using Npgsql;
using System.Diagnostics;
using System.Globalization;

namespace MsSqlToPostgres
{
	public partial class frmMain : Form
	{
		private List<TableInfo> tables;
		public frmMain()
		{
			InitializeComponent();
			lnkGithub.LinkClicked += LnkGithub_LinkClicked;
		}

		private void LnkGithub_LinkClicked(object? sender, LinkLabelLinkClickedEventArgs e)
		{
			Process.Start(new ProcessStartInfo
			{
				FileName = "https://github.com/StefanPotthoff/MsSqlToPostgres",
				UseShellExecute = true
			});
		}

		private void btnDonate_Click(object sender, EventArgs e)
		{
			Process.Start(new ProcessStartInfo
			{
				FileName = "https://www.paypal.com/donate?business=Stefan.Potthoff@gmx.net&currency_code=EUR",
				UseShellExecute = true
			});
		}

		private void btnTestMsqSql_Click(object sender, EventArgs e)
		{
			string mssqlConnString = txtMsqlConnectionString.Text;

			try
			{
				using (SqlConnection conn = new SqlConnection(mssqlConnString))
				{
					conn.Open();

					string tableQuery = "SELECT TABLE_SCHEMA, TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE'";
					using (SqlCommand tableCmd = new SqlCommand(tableQuery, conn))
					using (SqlDataReader tableReader = tableCmd.ExecuteReader())

						MessageBox.Show("MSSQL Connection Test is successful!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
			}
			catch
			{
				MessageBox.Show("MSSQL Connection Test is NOT successful!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void btnTestPostgres_Click(object sender, EventArgs e)
		{
			string postgresConnString = txtPostgresConnectionString.Text;

			try
			{
				using (NpgsqlConnection conn = new NpgsqlConnection(postgresConnString))
				{
					conn.Open();

					string tableQuery = "SELECT table_schema, table_name FROM information_schema.tables WHERE table_type = 'BASE TABLE'";
					using (NpgsqlCommand tableCmd = new NpgsqlCommand(tableQuery, conn))
					using (NpgsqlDataReader tableReader = tableCmd.ExecuteReader())

						MessageBox.Show("PostgreSQL Connection Test is successful!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("PostgreSQL Connection Test is NOT successful: " + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void SetProgressVisibility(bool isVisible)
		{
			pbTotal.Visible = isVisible;
			lblTotal.Visible = isVisible;
			pbCurrentStep.Visible = isVisible;
			lblCurrentStep.Visible = isVisible;
		}

		private async void btnTransfer_Click(object sender, EventArgs e)
		{
			SetProgressVisibility(true);
			pbTotal.Value = 0;
			lblTotal.Text = "Reading database structure from MS-SQL...";
			this.Enabled = false;

			var progressTotal = new Progress<(string message, int progressValue)>(report =>
			{
				lblTotal.Text = report.message;
				pbTotal.Value = Math.Min(report.progressValue, pbTotal.Maximum);
				lblTotal.Refresh();
			});

			var progressCurrentStep = new Progress<(string message, int progressValue)>(report =>
			{
				lblCurrentStep.Text = report.message;
				pbCurrentStep.Value = Math.Min(report.progressValue, pbCurrentStep.Maximum);
				lblCurrentStep.Refresh();
			});

			try
			{
				await Task.Run(async () =>
				{
					GetMsSqlData();

					int totalTables = tables.Count;
					int completedTables = 0;

					var migrationManager = new MigrationManager();
					await migrationManager.MigrateStructureToPostgres(tables, txtPostgresConnectionString.Text);

					foreach (var table in tables)
					{
						((IProgress<(string, int)>)progressTotal).Report(($"Copying table {table.Name}...", (int)((completedTables / (double)totalTables) * 100)));

						// Prüfen, ob die Tabelle bereits Daten enthält
						var postgresHelper = new PostgresHelper(txtPostgresConnectionString.Text);
						string sql = $"SELECT COUNT(1) FROM \"{table.Name}\"";
						var result = await postgresHelper.ExecuteScalarAsync(sql);
						if (result != null && ((long)result) > 0)
						{
							// Tabelle schon vorhanden, überspringen
							Interlocked.Increment(ref completedTables);
							((IProgress<(string, int)>)progressTotal).Report(($"Skipping table {table.Name}...", (int)((completedTables / (double)totalTables) * 100)));
							continue;
						}

						// Daten in Chunks kopieren (Parallelverarbeitung)
						string sourceQuery = $"SELECT * FROM {table.Name}";
						await CopyDataInChunksAsync(txtMsqlConnectionString.Text, txtPostgresConnectionString.Text, sourceQuery, table.Name, 10000, progressCurrentStep);

						Interlocked.Increment(ref completedTables);
						((IProgress<(string, int)>)progressTotal).Report(($"Finished table {table.Name}...", (int)((completedTables / (double)totalTables) * 100)));
					}
				});

				var successDlg = new frmSuccess();
				successDlg.ShowDialog();
			}
			catch (Exception ex)
			{
				MessageBox.Show("Error during migration: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			finally
			{
				SetProgressVisibility(false);
				this.Enabled = true;
			}
		}

		private void GetMsSqlData()
		{
			string mssqlConnString = txtMsqlConnectionString.Text;

			try
			{
				using (SqlConnection conn = new SqlConnection(mssqlConnString))
				{
					conn.Open();

					string tableQuery = "SELECT TABLE_SCHEMA, TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE'";
					SqlCommand tableCmd = new SqlCommand(tableQuery, conn);
					SqlDataReader tableReader = tableCmd.ExecuteReader();
					tables = new List<TableInfo>();

					while (tableReader.Read())
					{
						string schema = tableReader["TABLE_SCHEMA"].ToString();
						string tableName = tableReader["TABLE_NAME"].ToString();
						tables.Add(new TableInfo { Schema = schema, Name = tableName, Columns = new List<ColumnInfo>() });
					}
					tableReader.Close();

					foreach (var table in tables)
					{
						string columnQuery = "SELECT COLUMN_NAME, DATA_TYPE, CHARACTER_MAXIMUM_LENGTH " +
											 "FROM INFORMATION_SCHEMA.COLUMNS " +
											 "WHERE TABLE_SCHEMA = @schema AND TABLE_NAME = @tableName";
						SqlCommand columnCmd = new SqlCommand(columnQuery, conn);
						columnCmd.Parameters.AddWithValue("@schema", table.Schema);
						columnCmd.Parameters.AddWithValue("@tableName", table.Name);

						SqlDataReader columnReader = columnCmd.ExecuteReader();
						while (columnReader.Read())
						{
							string columnName = columnReader["COLUMN_NAME"].ToString();
							string dataType = columnReader["DATA_TYPE"].ToString();
							string maxLength = columnReader["CHARACTER_MAXIMUM_LENGTH"] != DBNull.Value
											   ? columnReader["CHARACTER_MAXIMUM_LENGTH"].ToString()
											   : "n/a";

							table.Columns.Add(new ColumnInfo { Name = columnName, DataType = dataType, MaxLength = maxLength });
						}
						columnReader.Close();
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("Error reading database structure: " + ex.Message);
			}

		}

		public async Task CopyDataInChunksAsync(string mssqlConnString, string postgresConnString, string sourceQuery, string targetTable, int chunkSize, IProgress<(string message, int progressValue)> progressCurrentStep)
		{
			// Sicherstellen, dass MultipleActiveResultSets=True in der Verbindungszeichenfolge enthalten ist
			if (!mssqlConnString.Contains("MultipleActiveResultSets=True", StringComparison.OrdinalIgnoreCase))
			{
				mssqlConnString += ";MultipleActiveResultSets=True";
			}

			using (SqlConnection sqlConn = new SqlConnection(mssqlConnString))
			{
				await sqlConn.OpenAsync();

				string countQuery = $"SELECT COUNT(*) FROM ({sourceQuery}) AS SourceData";
				using (SqlCommand countCmd = new SqlCommand(countQuery, sqlConn))
				{
					int totalRows = (int)await countCmd.ExecuteScalarAsync();
					if (totalRows == 0)
						return;

					pbCurrentStep.Invoke((MethodInvoker)delegate { pbCurrentStep.Maximum = 100; pbCurrentStep.Value = 0; });

					int completedRows = 0;
					var semaphore = new SemaphoreSlim(5); // Maximal 5 parallele Threads für Chunks
					var tasks = new List<Task>();

					for (int offset = 0; offset < totalRows; offset += chunkSize)
					{
						string chunkQuery = $"{sourceQuery} ORDER BY (SELECT 0) OFFSET {offset} ROWS FETCH NEXT {chunkSize} ROWS ONLY";
						tasks.Add(Task.Run(async () =>
						{
							await semaphore.WaitAsync();
							try
							{
								using (SqlCommand chunkCmd = new SqlCommand(chunkQuery, sqlConn))
								using (SqlDataReader reader = await chunkCmd.ExecuteReaderAsync())
								{
									List<Dictionary<string, object>> chunkData = new List<Dictionary<string, object>>();
									while (await reader.ReadAsync())
									{
										Dictionary<string, object> row = new Dictionary<string, object>();
										for (int i = 0; i < reader.FieldCount; i++)
										{
											row[reader.GetName(i)] = reader.GetValue(i);
										}
										chunkData.Add(row);
									}

									await InsertChunkIntoPostgresAsync(chunkData, targetTable, postgresConnString);
									Interlocked.Add(ref completedRows, chunkData.Count);

									int progressPercentage = (int)((completedRows / (double)totalRows) * 100);
									progressCurrentStep.Report(($"Copying data to {targetTable}... {completedRows.ToString("N0", CultureInfo.CurrentCulture)}/{totalRows.ToString("N0", CultureInfo.CurrentCulture)}", progressPercentage));
								}
							}
							finally
							{
								semaphore.Release();
							}
						}));
					}

					await Task.WhenAll(tasks);
					progressCurrentStep.Report(("Copy complete!", 100));
				}
			}
		}


		private async Task InsertChunkIntoPostgresAsync(List<Dictionary<string, object>> rows, string targetTable, string postgresConnString)
		{
			if (rows.Count == 0)
				return;

			using (NpgsqlConnection pgConn = new NpgsqlConnection(postgresConnString))
			{
				await pgConn.OpenAsync();
				using (var transaction = await pgConn.BeginTransactionAsync())
				{
					foreach (var row in rows)
					{
						var columnNames = string.Join(", ", row.Keys.Select(k => $"\"{k}\""));
						var parameterNames = string.Join(", ", row.Keys.Select(k => $"@{k}"));

						string insertQuery = $"INSERT INTO \"{targetTable}\" ({columnNames}) VALUES ({parameterNames})";

						using (NpgsqlCommand pgCmd = new NpgsqlCommand(insertQuery, pgConn))
						{
							foreach (var key in row.Keys)
							{
								pgCmd.Parameters.AddWithValue("@" + key, row[key] ?? DBNull.Value);
							}
							await pgCmd.ExecuteNonQueryAsync();
						}
					}
					await transaction.CommitAsync();
				}
			}
		}
	}
}
