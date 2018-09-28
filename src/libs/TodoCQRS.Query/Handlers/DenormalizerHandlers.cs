using TodoCQRS.Domain.Events;
using TodoCQRS.Domain.Events.TodoItems;
using TodoCQRS.Infrastructure.Framework;
using TodoCQRS.Infrastructure.LiteDb.Model;
using TodoCQRS.Infrastructure.LiteDb.Repositories;
using TodoCQRS.Query.DataAccess;

namespace TodoCQRS.Query.Handlers
{
  public class DenormalizerHandlers : IHandleMessage<TodoItemStatusChangedEvent>
    , IHandleMessage<TodoItemTextChangedEvent>, IHandleMessage<TodoItemDeletedEvent>
    , IHandleMessage<TodoItemCreatedEvent>, IHandleMessage<TodoListCreatedEvent>
  {
    private readonly ITodoListWriteRepository _todoListWriteRepository;
    private readonly ITodoListRepository _todoListRepository;

    public DenormalizerHandlers(ITodoListWriteRepository todoListWriteRepository
      , ITodoListRepository todoListRepository)
    {
      _todoListWriteRepository = todoListWriteRepository;
      _todoListRepository = todoListRepository;
    }

    public void Handle(TodoItemStatusChangedEvent message)
    {
      var item = _todoListRepository.GetItem(message.ItemId);
      item.IsCompleted = message.IsCompleted;
      _todoListWriteRepository.SaveItem(item);
    }

    public void Handle(TodoItemTextChangedEvent message)
    {
      var item = _todoListRepository.GetItem(message.ItemId);
      item.Text = message.Text;
      _todoListWriteRepository.SaveItem(item);
    }

    public void Handle(TodoItemDeletedEvent message)
    {
      _todoListWriteRepository.DeleteItem(message.ItemId);
    }

    public void Handle(TodoItemCreatedEvent message)
    {
      var item = new TodoItemViewModel()
      {
        ItemId = message.ItemId,
        Text = message.Text,
        ListId = message.ListId,
        ListName = message.ListName,
        IsCompleted = message.IsCompleted
      };
      _todoListWriteRepository.SaveItem(item);
    }

    public void Handle(TodoListCreatedEvent message)
    {
      _todoListWriteRepository.SaveList(new TodoListViewModel()
      {
        ListId = message.ListId,
        Name = message.ListName
      });
    }
  }
}