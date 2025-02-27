using System.Text;

namespace MsSqlToPostgres
{
	public class MigrationManager
	{
		private string MapMssqlTypeToPostgres(string mssqlType, string characterMaximumLength)
		{
			switch (mssqlType.ToLower())
			{
				case "int":
					return "integer";
				case "bigint":
					return "bigint";
				case "smallint":
					return "smallint";
				case "tinyint":
					return "smallint";
				case "bit":
					return "boolean";
				case "nvarchar":
				case "varchar":
					if (characterMaximumLength == "-1" || string.IsNullOrEmpty(characterMaximumLength))
						return "text";
					else
						return $"varchar({characterMaximumLength})";
				case "nchar":
				case "char":
					if (characterMaximumLength == "-1" || string.IsNullOrEmpty(characterMaximumLength))
						return "text";
					else
						return $"char({characterMaximumLength})";
				case "datetime":
				case "smalldatetime":
					return "timestamp";
				case "date":
					return "date";
				case "time":
					return "time";
				case "datetimeoffset":
					return "timestamptz";
				case "decimal":
				case "numeric":
					return "numeric";
				case "money":
					return "money"; 
				case "smallmoney":
					return "money"; 
				case "float":
					return "double precision";
				case "real":
					return "real";
				case "binary":
				case "varbinary":
				case "image":
					return "bytea";
				case "uniqueidentifier":
					return "uuid";
				case "timestamp":
					return "bytea";
				case "xml":
					return "xml";
				case "sql_variant":
					return "text"; 
				default:
					return mssqlType; // Fallback – possible more changes needed
			}
		}

		private string GeneratePostgresCreateTableStatement(TableInfo table)
		{
			StringBuilder sb = new StringBuilder();

			sb.AppendLine($"CREATE TABLE IF NOT EXISTS public.\"{table.Name}\" (");

			for (int i = 0; i < table.Columns.Count; i++)
			{
				var col = table.Columns[i];
				string pgType = MapMssqlTypeToPostgres(col.DataType, col.MaxLength);
				sb.Append($"    \"{col.Name}\" {pgType}");

				if (i < table.Columns.Count - 1)
					sb.AppendLine(",");
				else
					sb.AppendLine();
			}

			sb.AppendLine(");");
			return sb.ToString();
		}

		public async Task MigrateStructureToPostgres(List<TableInfo> mssqlTables, string postgresConnectionString)
		{
			PostgresHelper pgHelper = new PostgresHelper(postgresConnectionString);

			foreach (var table in mssqlTables)
			{
				string createTableSql = GeneratePostgresCreateTableStatement(table);
				try
				{
					await pgHelper.ExecuteSqlAsync(createTableSql);
				}
				catch (Exception ex)
				{
					MessageBox.Show($"Error creating table {table.Name}: {ex.Message}", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
					throw ex;
				}
			}
		}
	}
}
