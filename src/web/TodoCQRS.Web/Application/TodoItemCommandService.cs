using System;
using TodoCQRS.Commands.Commands.TodoItems;
using TodoCQRS.Infrastructure.Framework;
using TodoCQRS.Web.Application.Interfaces;

namespace TodoCQRS.Web.Application
{
  public class TodoItemCommandService : ITodoItemCommandService
  {
    private readonly IBus _bus;

    public TodoItemCommandService(IBus bus)
    {
      _bus = bus;
    }

    public Guid CreateItem(Guid listId, string text, bool isCompleted)
    {
      var itemId = Guid.NewGuid();
      _bus.Send(new CreateTodoItemCommand(listId, itemId, text, isCompleted));
      return itemId;
    }

    public void ChangeText(Guid listId, Guid itemId, string text)
    {
      _bus.Send(new ChangeTodoItemTextCommand(listId, itemId, text));
    }

    public void ChangeStatus(Guid listId, Guid itemId, bool isCompleted)
    {
      _bus.Send(new ChangeTodoItemStatusCommand(listId, itemId, isCompleted));
    }

    public void DeleteItem(Guid listId, Guid itemId)
    {
      _bus.Send(new DeleteTodoItemCommand(listId, itemId));
    }
  }
}