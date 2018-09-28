namespace TodoCQRS.Infrastructure.LiteDb
{
  public class LiteDbConnectionOptions : ILiteDbConnectionOptions
  {
    public string ConnectionString { get; }

    public LiteDbConnectionOptions(string connectionString)
    {
      ConnectionString = connectionString;
    }
  }
}