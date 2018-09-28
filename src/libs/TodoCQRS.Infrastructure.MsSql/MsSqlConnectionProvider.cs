using System.Data;
using System.Data.SqlClient;

namespace TodoCQRS.Infrastructure.MsSql
{
  public class MsSqlConnectionProvider : ISqlConnectionProvider
  {
    private readonly string _connectionString;

    public MsSqlConnectionProvider(string connectionString)
    {
      _connectionString = connectionString;
    }
    public SqlConnection GetConnection()
    {
      var c = new SqlConnection(_connectionString);
      c.Open();
      return c;
    }
  }
}