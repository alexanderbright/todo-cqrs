using System;

namespace TodoCQRS.Infrastructure.Framework
{
  public class DomainEvent : Message
  {
    public DateTime TimeStamp { get; private set; }

    public DomainEvent()
    {
      TimeStamp = DateTime.Now;
      Name = this.GetType().Name;
    }

  }
}