using System;
using TodoCQRS.Infrastructure.Framework;

namespace TodoCQRS.Domain.Events.TodoItems
{
  public class TodoItemCreatedEvent : DomainEvent
  {
    public Guid ListId { get; }
    public string ListName { get; }
    public Guid ItemId { get; }
    public string Text { get; }
    public bool IsCompleted { get; }
    public TodoItemCreatedEvent(Guid listId, string listName, Guid itemId, string text, bool isCompleted)
    {
      ListId = listId;
      ListName = listName;
      ItemId = itemId;
      Text = text;
      IsCompleted = isCompleted;
    }
  }
}