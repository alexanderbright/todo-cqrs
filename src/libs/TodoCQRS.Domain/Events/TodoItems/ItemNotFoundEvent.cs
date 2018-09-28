using System;

namespace TodoCQRS.Domain.Events.TodoItems
{
  public class ItemNotFoundEvent : TodoItemEvent
  {
    public ItemNotFoundEvent(Guid listId, Guid itemId) : base(listId, itemId)
    {
    }
  }
}