using System;
using TodoCQRS.Infrastructure.Framework;

namespace TodoCQRS.Commands.Commands.TodoItems
{
  public abstract class TodoItemCommand : Command
  {
    public Guid ListId { get; protected set; }
    public Guid ItemId { get; protected set; }

    protected TodoItemCommand(Guid listId, Guid itemId)
    {
      ListId = listId;
      ItemId = itemId;
    }
  }
}