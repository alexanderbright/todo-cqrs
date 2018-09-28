using System;
using System.Collections.Generic;
using System.Linq;
using TodoCQRS.Infrastructure.Framework.EventStore;

namespace TodoCQRS.Infrastructure.Framework
{
  public class EventStoreImpl : IEventStore
  {
    public void Save<T>(T theEvent) where T : DomainEvent
    {
      
    }
  }

  public class InMemoryBus : IBus
  {
    private readonly IDependencyService _dependencyService;
    public IEventStore EventStore { get; private set; }

    private static readonly IDictionary<Type, Type> RegisteredSagas = new Dictionary<Type, Type>();
    private static readonly IDictionary<Guid, object> SagaInstances = new Dictionary<Guid, object>();
    private static readonly IList<Type> RegisteredHandlers = new List<Type>();

    public InMemoryBus(IDependencyService dependencyService, IEventStore eventStore)
    {
      _dependencyService = dependencyService;
      if (eventStore == null)
      {
        throw new ArgumentNullException("eventStore");
      }
      EventStore = eventStore;
    }


    #region IBus
    void IBus.RegisterSaga<T>()
    {
      var sagaType = typeof(T);
      if (sagaType.GetInterfaces().Count(i => i.Name.StartsWith(typeof(IStartWithMessage<>).Name)) != 1)
      {
        throw new InvalidOperationException("The specified saga must implement the IStartWithMessage<T> interface.");
      }
      var messageType = sagaType.
          GetInterfaces().First(i => i.Name.StartsWith(typeof(IStartWithMessage<>).Name)).
          GenericTypeArguments.
          First();
      RegisteredSagas.Add(messageType, sagaType);
    }
    void IBus.RegisterHandler<T>()
    {
      RegisteredHandlers.Add(typeof(T));
    }

    void IBus.Send<T>(T message)
    {
      SendInternal(message);
    }
    void IBus.RaiseEvent<T>(T theEvent)
    {
      if (EventStore != null)
        EventStore.Save(theEvent);
      SendInternal(theEvent);
    }
    #endregion


    #region Private Members
    private void SendInternal<T>(T message) where T : Message
    {
      LaunchSagasThatStartWithMessage(message);
      DeliverMessageToRunningSagas(message);
      DeliverMessageToRegisteredHandlers(message);

      // Saga and handlers are similar things. Handlers are  one-off event handlers
      // whereas saga may be persisted and survive sessions, wait for more messages and so forth.
      // Saga are mostly complex workflows; handlers are plain one-off event handlers.
    }

    private void LaunchSagasThatStartWithMessage<T>(T message) where T : Message
    {
      var messageType = message.GetType();
      var openInterface = typeof(IStartWithMessage<>);
      var closedInterface = openInterface.MakeGenericType(messageType);
      var sagasToLaunch = from s in RegisteredSagas.Values
                          where closedInterface.IsAssignableFrom(s)
                          select s;
      foreach (var s in sagasToLaunch)
      {
        dynamic sagaInstance = _dependencyService.Resolve(s);
        sagaInstance.Handle(message);

        SagaInstances.Add(((Saga) sagaInstance).SagaId, sagaInstance); //todo
      }
    }

    private void DeliverMessageToRunningSagas<T>(T message) where T : Message
    {
      var messageType = message.GetType();
      var openInterface = typeof(IHandleMessage<>);
      var closedInterface = openInterface.MakeGenericType(messageType);
      var sagasToNotify = from s in RegisteredSagas.Values
                          where closedInterface.IsAssignableFrom(s)
                          select s;
      foreach (var s in sagasToNotify)
      {
        dynamic sagaInstance = _dependencyService.Resolve(s);
        sagaInstance.Handle(message);
      }
    }

    private void DeliverMessageToRegisteredHandlers<T>(T message) where T : Message
    {
      var messageType = message.GetType();
      var openInterface = typeof(IHandleMessage<>);
      var closedInterface = openInterface.MakeGenericType(messageType);
      var handlersToNotify = from h in RegisteredHandlers
                             where closedInterface.IsAssignableFrom(h)
                             select h;
      foreach (var h in handlersToNotify)
      {
        dynamic sagaInstance = _dependencyService.Resolve(h);     // default ctor is enough
        sagaInstance.Handle(message);
      }
    }
    #endregion
  }
}