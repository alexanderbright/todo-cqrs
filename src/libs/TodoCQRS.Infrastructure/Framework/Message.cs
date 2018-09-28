
using System;

namespace TodoCQRS.Infrastructure.Framework
{
  public class Message
  {
    public Guid SagaId { get; set; }
    public string Name { get; protected set; }
  }
}