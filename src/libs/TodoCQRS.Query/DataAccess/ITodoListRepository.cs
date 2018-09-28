using System;
using System.Collections.Generic;
using TodoCQRS.Infrastructure.LiteDb.Model;

namespace TodoCQRS.Query.DataAccess
{
  public interface ITodoListRepository
  {
    List<TodoItemViewModel> GetItems();
    TodoItemViewModel GetItem(Guid id);
    List<TodoListViewModel> GetLists();
  }
}