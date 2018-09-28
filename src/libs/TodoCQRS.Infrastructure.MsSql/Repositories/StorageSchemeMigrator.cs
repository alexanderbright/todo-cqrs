using Dapper;

namespace TodoCQRS.Infrastructure.MsSql.Repositories
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
        con.Execute(@"IF NOT EXISTS (SELECT * FROM sysobjects WHERE id = object_id(N'[dbo].[TodoList]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1) 
                         BEGIN 
                              CREATE TABLE [TodoList] ([Id] INTEGER IDENTITY(1,1) PRIMARY KEY, [ListId] UNIQUEIDENTIFIER, [Name] TEXT)
                         END");
      }
    }

    public void CreateTodoItemTableIfNotExists()
    {
      using (var con = _connectionProvider.GetConnection())
      {
        con.Execute(@"IF NOT EXISTS (SELECT * FROM sysobjects WHERE id = object_id(N'[dbo].[TodoItem]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1)
                    BEGIN
                      CREATE TABLE [TodoItem] ([Id] INTEGER IDENTITY(1,1) PRIMARY KEY, [ItemId] UNIQUEIDENTIFIER, [Text] TEXT, [IsCompleted] INTEGER, [TodoList_Id] INTEGER)
                      ALTER TABLE [TodoItem]
                          ADD FOREIGN KEY (TodoList_Id) REFERENCES TodoList([Id]);
                    END");
      }
    }
  }
}