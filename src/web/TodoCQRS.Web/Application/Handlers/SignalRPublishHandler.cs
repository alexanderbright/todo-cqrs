using Microsoft.AspNetCore.SignalR;
using TodoCQRS.Domain.Events;
using TodoCQRS.Domain.Events.TodoItems;
using TodoCQRS.Infrastructure.Framework;
using TodoCQRS.Web.Application.Hubs;

namespace TodoCQRS.Web.Application.Handlers
{
  public class SignalRPublishHandler : IHandleMessage<TodoListCreatedEvent>
    , IHandleMessage<TodoItemStatusChangedEvent>
    , IHandleMessage<TodoItemTextChangedEvent>
    , IHandleMessage<TodoItemCreatedEvent>
    , IHandleMessage<TodoItemDeletedEvent>
  {
    private readonly IHubContext<EventHub, IEventListener> _hubContext;

    public SignalRPublishHandler(IHubContext<EventHub, IEventListener> hubContext)
    {
      _hubContext = hubContext;
    }

    public async void Handle(TodoListCreatedEvent message)
    {
      await _hubContext.Clients.All.TodoListCreatedEvent(message);
    }

    public async void Handle(TodoItemStatusChangedEvent message)
    {
      await _hubContext.Clients.All.TodoItemStatusChangedEvent(message);
    }

    public async void Handle(TodoItemTextChangedEvent message)
    {
      await _hubContext.Clients.All.TodoItemTextChangedEvent(message);
    }

    public async void Handle(TodoItemCreatedEvent message)
    {
      await _hubContext.Clients.All.TodoItemCreatedEvent(message);
    }

    public async void Handle(TodoItemDeletedEvent message)
    {
      await _hubContext.Clients.All.TodoItemDeletedEvent(message);
    }
  }
}