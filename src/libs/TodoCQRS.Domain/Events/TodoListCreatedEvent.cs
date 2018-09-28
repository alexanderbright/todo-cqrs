using System;
using TodoCQRS.Infrastructure.Framework;

namespace TodoCQRS.Domain.Events
{
  public class TodoListCreatedEvent : DomainEvent
  {
    public Guid ListId { get; } 
    public string ListName { get; }

    public TodoListCreatedEvent(Guid listId, string name)
    {
      ListId = listId;
      ListName = name;
    }
  }
}