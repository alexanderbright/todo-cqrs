using System;
using TodoCQRS.Infrastructure.Framework.EventStore;

namespace TodoCQRS.Infrastructure.Framework
{
  public abstract class Saga
  {
    public Guid SagaId { get; } = Guid.NewGuid();

    public bool IsCompleted { get; protected set; }
  }

}