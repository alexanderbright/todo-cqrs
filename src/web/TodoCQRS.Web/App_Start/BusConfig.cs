using TodoCQRS.Commands.Handlers;
using TodoCQRS.Commands.Sagas;
using TodoCQRS.Infrastructure.Framework;
using TodoCQRS.Query.Handlers;
using TodoCQRS.Web.Application.Handlers;

namespace TodoCQRS.Web
{
  public class BusConfig
  {
    public static void Initialize(IBus bus)
    {
      BusProvider.Bus = bus;
      bus.RegisterHandler<CommandHandlers>();
      bus.RegisterHandler<DenormalizerHandlers>();
      bus.RegisterHandler<SignalRPublishHandler>();
      bus.RegisterSaga<CreateTodoListSaga>();
    }
  }
}
