namespace TodoCQRS.Infrastructure.Framework
{
  public class Entity<TKey>
  {
    public TKey Key { get; protected set; }
  }
}