using LiteDB;

namespace TodoCQRS.Infrastructure.LiteDb
{
  public class LiteDbProvider : ILiteDbProvider
  {
    private readonly ILiteDbConnectionOptions _liteDbConnectionOptions;

    public LiteDbProvider(ILiteDbConnectionOptions liteDbConnectionOptions)
    {
      _liteDbConnectionOptions = liteDbConnectionOptions;
    }

    public LiteDatabase GetDatabase()
    {
      return new LiteDatabase(_liteDbConnectionOptions.ConnectionString);
    }
  }
}