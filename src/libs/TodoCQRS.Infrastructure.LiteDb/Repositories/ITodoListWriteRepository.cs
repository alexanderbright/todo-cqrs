using System;
using TodoCQRS.Infrastructure.LiteDb.Model;

namespace TodoCQRS.Infrastructure.LiteDb.Repositories
{
  public interface ITodoListWriteRepository
  {
    void SaveList(TodoListViewModel model);
    void DeleteList(Guid id);
    void SaveItem(TodoItemViewModel model);
    void DeleteItem(Guid id);
  }
}