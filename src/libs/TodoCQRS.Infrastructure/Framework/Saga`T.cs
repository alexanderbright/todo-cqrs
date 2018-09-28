using System;
using System.Collections.Generic;
using System.Text;

namespace TodoCQRS.Infrastructure.Framework
{
  public abstract class Saga<TData> : Saga
  {
    public TData Data { get; set; }
  }
}
