using System.Data;
using System.Data.SQLite;

namespace TodoCQRS.Infrastructure.SQLite
{
  public class SQLiteConnectionProvider : ISqlConnectionProvider
  {
    private readonly string _connectionString;

    public SQLiteConnectionProvider(string connectionString)
    {
      _connectionString = connectionString;
    }
    public SQLiteConnection GetConnection()
    {
      return new SQLiteConnection(_connectionString);
    }
  }
}