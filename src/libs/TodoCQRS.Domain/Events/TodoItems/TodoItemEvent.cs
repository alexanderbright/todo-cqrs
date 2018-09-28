using System;
using TodoCQRS.Infrastructure.Framework;

namespace TodoCQRS.Domain.Events.TodoItems
{
  public abstract class TodoItemEvent : DomainEvent
  {
    public Guid ListId { get; }
    public Guid ItemId { get; }

    public TodoItemEvent(Guid listId, Guid itemId)
    {
      ListId = listId;
      ItemId = itemId;
    }
  }
}