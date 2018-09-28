using Dapper;


namespace TodoCQRS.Infrastructure.SQLite.Repositories
{
  public class StorageSchemeMigrator : IStorageSchemeMigrator
  {
    private readonly ISqlConnectionProvider _connectionProvider;

    public StorageSchemeMigrator(ISqlConnectionProvider connectionProvider)
    {
      _connectionProvider = connectionProvider;
    }

    public void CreateIfNotExists()
    {
      CreateTodoListTableIfNotExists();
      CreateTodoItemTableIfNotExists();
    }

    public void CreateTodoListTableIfNotExists()
    {
      using (var con = _connectionProvider.GetConnection())
      {
        con.Open();
        con.Execute("CREATE TABLE IF NOT EXISTS TodoList (" +
                 " [Id] INTEGER" +
                 ", [Name] TEXT)");
      }
    }

    public void CreateTodoItemTableIfNotExists()
    {
      using (var con = _connectionProvider.GetConnection())
      {
        con.Execute("CREATE TABLE IF NOT EXISTS TodoItem (" +
                 "[Id] INTEGER PRIMARY KEY" +
                 ", [Text] TEXT" +
                 ", [IsCompleted] INTEGER" +
                 ", [TodoList_Id] INTEGER FOREIGN KEY(ListId) REFERENCES TodoList(Id)) ON DELETE CASCADE");
      }
    }
  }
}