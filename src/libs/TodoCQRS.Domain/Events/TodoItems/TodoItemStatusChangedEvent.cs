using System;

namespace TodoCQRS.Domain.Events.TodoItems
{
  public class TodoItemStatusChangedEvent : TodoItemEvent
  {
    public bool IsCompleted { get; }
    public TodoItemStatusChangedEvent(Guid listId, Guid itemId, bool isCompleted) : base(listId, itemId)
    {
      IsCompleted = isCompleted;
    }
  }
}