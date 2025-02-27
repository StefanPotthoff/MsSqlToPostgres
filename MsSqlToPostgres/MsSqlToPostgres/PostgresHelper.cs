using System.Text;
using Npgsql;

namespace MsSqlToPostgres
{	

	public class PostgresHelper
	{
		private readonly string _connectionString;

		public PostgresHelper(string connectionString)
		{
			_connectionString = connectionString;
		}

		public NpgsqlConnection CreateConnection()
		{
			var connection = new NpgsqlConnection(_connectionString);
			connection.Open();
			return connection;
		}

		public async Task ExecuteSqlAsync(string sql)
		{
			using (var conn = CreateConnection())
			{
				using (var cmd = new NpgsqlCommand(sql, conn))
				{
					cmd.CommandTimeout = 50000;
					await cmd.ExecuteNonQueryAsync();
				}
			}
		}

		public async Task<object> ExecuteScalarAsync(string sql)
		{
			using (var conn = CreateConnection())
			{
				using (var cmd = new NpgsqlCommand(sql, conn))
				{
					return await cmd.ExecuteScalarAsync();
				}
			}
		}
	}
}