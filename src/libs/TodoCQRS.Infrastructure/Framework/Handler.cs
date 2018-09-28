using System;
using TodoCQRS.Infrastructure.Framework.EventStore;

namespace TodoCQRS.Infrastructure.Framework
{
  public abstract class Handler
  {
    public IEventStore EventStore { get; private set; }


    public Handler(IEventStore eventStore)
    {
      if (eventStore == null)
      {
        throw new ArgumentNullException("eventStore");
      }

      EventStore = eventStore;
    }
  }

}