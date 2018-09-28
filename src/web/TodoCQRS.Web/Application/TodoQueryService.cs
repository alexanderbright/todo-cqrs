using System;
using System.Collections.Generic;
using TodoCQRS.Infrastructure.LiteDb.Model;
using TodoCQRS.Query.DataAccess;
using TodoCQRS.Web.Application.Interfaces;

namespace TodoCQRS.Web.Application
{
  public class TodoQueryService : ITodoQueryService
  {
    private readonly ITodoListRepository _todoListRepository;

    public TodoQueryService(ITodoListRepository todoListRepository)
    {
      _todoListRepository = todoListRepository;
    }

    public List<TodoItemViewModel> GetItems()
    {
      return _todoListRepository.GetItems();
    }

    public List<TodoListViewModel> GetLists()
    {
      return _todoListRepository.GetLists();
    }
  }
}