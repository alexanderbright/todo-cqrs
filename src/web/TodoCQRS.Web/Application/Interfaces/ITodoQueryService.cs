using System;
using System.Collections.Generic;
using TodoCQRS.Infrastructure.LiteDb.Model;

namespace TodoCQRS.Web.Application.Interfaces
{
  public interface ITodoQueryService
  {
    List<TodoItemViewModel> GetItems();
    List<TodoListViewModel> GetLists();
  }
}