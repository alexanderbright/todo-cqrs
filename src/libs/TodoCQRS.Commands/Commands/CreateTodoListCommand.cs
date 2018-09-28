using System;
using TodoCQRS.Infrastructure.Framework;

namespace TodoCQRS.Commands.Commands
{
  public class CreateTodoListCommand : Command
  {
    public Guid ListId { get; }
    public string ListName { get; }

    public CreateTodoListCommand(Guid listId, string name)
    {
      ListId = listId;
      ListName = name;
    }
  }
}