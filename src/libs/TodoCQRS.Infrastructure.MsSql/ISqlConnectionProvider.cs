using System.Data;
using System.Data.SqlClient;

namespace TodoCQRS.Infrastructure.MsSql
{
  public interface ISqlConnectionProvider
  {
    SqlConnection GetConnection();
  }
}