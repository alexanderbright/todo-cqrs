namespace TodoCQRS.Infrastructure.LiteDb
{
  public interface ILiteDbConnectionOptions
  {
    string ConnectionString { get; }
  }
}