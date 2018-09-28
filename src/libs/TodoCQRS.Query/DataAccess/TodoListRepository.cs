using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LiteDB;
using TodoCQRS.Infrastructure.LiteDb;
using TodoCQRS.Infrastructure.LiteDb.Model;

namespace TodoCQRS.Query.DataAccess
{
  public class TodoListRepository : ITodoListRepository
  {
    private readonly ILiteDbProvider _provider;

    public TodoListRepository(ILiteDbProvider provider)
    {
      _provider = provider;
    }

    public TodoItemViewModel GetItem(Guid id)
    {
      using (var db = _provider.GetDatabase())
      {
        var todoLists = db.GetCollection<TodoItemViewModel>();
        return todoLists.FindById(id);
      }
    }

    public List<TodoItemViewModel> GetItems()
    {
      using (var db = _provider.GetDatabase())
      {
        var todoLists = db.GetCollection<TodoItemViewModel>();
        return todoLists.FindAll().ToList();
      }
    }

    public List<TodoListViewModel> GetLists()
    {
      using (var db = _provider.GetDatabase())
      {
        var todoLists = db.GetCollection<TodoListViewModel>();
        return todoLists.FindAll().ToList();
      }
    }
  }
}
