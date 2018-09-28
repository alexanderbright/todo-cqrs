using System;

namespace TodoCQRS.Infrastructure.Framework
{
  public interface IDependencyService
  {
    T Resolve<T>();
    object Resolve(Type type);
  }
}