using System;
using DryIoc;
using TodoCQRS.Infrastructure.Framework;

namespace TodoCQRS.Web.Application
{
  public class DryIocDependencyService : IDependencyService
  {
    private readonly IResolverContext _resolverContext;

    public DryIocDependencyService(IResolverContext resolverContext)
    {
      _resolverContext = resolverContext;
    }

    public object Resolve(Type type)
    {
      return _resolverContext.Resolve(type, IfUnresolved.Throw);
    }

    public T Resolve<T>()
    {
      return (T)_resolverContext.Resolve<T>(true);
    }
  }
}