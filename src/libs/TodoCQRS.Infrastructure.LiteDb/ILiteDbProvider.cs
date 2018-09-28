using LiteDB;

namespace TodoCQRS.Infrastructure.LiteDb
{
  public interface ILiteDbProvider
  {
    LiteDatabase GetDatabase();
  }
}