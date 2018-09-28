using System;
using TodoCQRS.Domain.Aggregates;

namespace TodoCQRS.Infrastructure.SQLite.Repositories
{
  public interface ITodoListAggregateRepository
  {
    TodoListAggregate GetById(int id);
    void Save(TodoListAggregate entity);
    void DeleteById(int entity);
  }
}