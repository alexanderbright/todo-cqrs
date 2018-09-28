using System;
using TodoCQRS.Domain.Aggregates;

namespace TodoCQRS.Infrastructure.MsSql.Repositories
{
  public interface ITodoListAggregateRepository
  {
    TodoListAggregate GetById(object listId);
    void Save(TodoListAggregate listEntity);
    void Delete(TodoListAggregate entity);
  }
}