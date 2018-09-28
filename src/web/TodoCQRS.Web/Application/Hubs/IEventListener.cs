using System.Threading.Tasks;
using TodoCQRS.Domain.Events;
using TodoCQRS.Domain.Events.TodoItems;

namespace TodoCQRS.Web.Application.Hubs
{
  public interface IEventListener
  {
    Task TodoItemCreatedEvent(TodoItemCreatedEvent e);
    Task TodoItemStatusChangedEvent(TodoItemStatusChangedEvent e);
    Task TodoItemTextChangedEvent(TodoItemTextChangedEvent e);
    Task TodoItemDeletedEvent(TodoItemDeletedEvent e);
    Task TodoListCreatedEvent(TodoListCreatedEvent e);
  }
}
