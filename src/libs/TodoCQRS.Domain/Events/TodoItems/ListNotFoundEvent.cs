using System;
using TodoCQRS.Infrastructure.Framework;

namespace TodoCQRS.Domain.Events.TodoItems
{
  public class ListNotFoundEvent : DomainEvent
  {
    public Guid ListId { get; }
    public ListNotFoundEvent(Guid id)
    {
      ListId = id;
    }
  }
}