using System.Data;
using System.Data.SQLite;

namespace TodoCQRS.Infrastructure.SQLite
{
  public interface ISqlConnectionProvider
  {
    SQLiteConnection GetConnection();
  }
}