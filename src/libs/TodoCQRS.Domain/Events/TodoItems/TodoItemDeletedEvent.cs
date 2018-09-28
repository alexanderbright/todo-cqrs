using System;
using TodoCQRS.Infrastructure.Framework;

namespace TodoCQRS.Domain.Events.TodoItems
{
  public class TodoItemDeletedEvent : DomainEvent
  {
    public Guid ListId { get; }
    public Guid ItemId { get; }
    public TodoItemDeletedEvent(Guid listId, Guid itemId)
    {
      ListId = listId;
      ItemId = itemId;
    }
  }
}