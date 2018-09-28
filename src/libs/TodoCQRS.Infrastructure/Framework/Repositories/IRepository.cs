namespace TodoCQRS.Infrastructure.Framework.Repositories
{
  public interface IRepository<TKey, TEntity> where TEntity : Entity<TKey>, IAggregateRoot
  {
    TEntity GetById(object id);
    void Save(TEntity listEntity);
    void Delete(TEntity entity);
  }
}