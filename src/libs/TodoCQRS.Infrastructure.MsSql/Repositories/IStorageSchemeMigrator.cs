namespace TodoCQRS.Infrastructure.MsSql.Repositories
{
  public interface IStorageSchemeMigrator
  {
    void CreateIfNotExists();
  }
}