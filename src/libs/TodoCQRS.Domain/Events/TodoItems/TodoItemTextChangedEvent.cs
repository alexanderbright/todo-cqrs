using System;

namespace TodoCQRS.Domain.Events.TodoItems
{
  public class TodoItemTextChangedEvent : TodoItemEvent
  {
    public string Text { get; }

    public TodoItemTextChangedEvent(Guid listId, Guid itemId, string text)
      :base(listId, itemId)
    {
      Text = text;
    }
  }
}