using Microsoft.AspNetCore.SignalR;

namespace TodoCQRS.Web.Application.Hubs
{
  public class EventHub : Hub<IEventListener>
  {
    public void SendEvent()
    {
      Clients.All.TodoItemCreatedEvent(null);
    }
  }
}