namespace TodoCQRS.Infrastructure.SQLite.Repositories
{
  public interface IStorageSchemeMigrator
  {
    void CreateIfNotExists();
  }
}