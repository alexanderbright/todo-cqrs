using System;
using TodoCQRS.Commands.Commands;
using TodoCQRS.Infrastructure.Framework;
using TodoCQRS.Web.Application.Interfaces;

namespace TodoCQRS.Web.Application
{
  public class TodoCommandService : ITodoCommandService
  {
    private readonly IBus _bus;

    public TodoCommandService(IBus bus)
    {
      _bus = bus;
    }
    public Guid CreateList(string name)
    {
      var id = Guid.NewGuid();
      _bus.Send(new CreateTodoListCommand(id, name));
      return id;
    }
  }
}