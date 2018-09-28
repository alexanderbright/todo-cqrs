using System;
using System.Linq;
using LiteDB;
using TodoCQRS.Infrastructure.LiteDb.Model;

namespace TodoCQRS.Infrastructure.LiteDb.Repositories
{
  public class TodoListWriteRepository : ITodoListWriteRepository
  {
    private readonly ILiteDbProvider _provider;


    public TodoListWriteRepository(ILiteDbProvider provider)
    {
      _provider = provider;
    }

    public void SaveList(TodoListViewModel model)
    {
      using (var db = _provider.GetDatabase())
      {
        var collection = db.GetCollection<TodoListViewModel>();
        if (!collection.Update(model))
        {
          collection.Insert(model);
        }
      }
    }

    public void DeleteList(Guid id)
    {
      using (var db = _provider.GetDatabase())
      {
        var collection = db.GetCollection<TodoListViewModel>();
        collection.Delete(id);
      }
    }

    public void SaveItem(TodoItemViewModel model)
    {
      using (var db = _provider.GetDatabase())
      {
        var collection = db.GetCollection<TodoItemViewModel>();
        if (!collection.Update(model))
        {
          collection.Insert(model);
        }
      }
    }

    public void DeleteItem(Guid id)
    {
      using (var db = _provider.GetDatabase())
      {
        var collection = db.GetCollection<TodoItemViewModel>();
        collection.Delete(id);
      }
    }
  }
}