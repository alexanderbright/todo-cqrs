using System;
using TodoCQRS.Commands.Commands.TodoItems;
using TodoCQRS.Domain.Aggregates;
using TodoCQRS.Domain.Events.TodoItems;
using TodoCQRS.Infrastructure.Framework;
using TodoCQRS.Infrastructure.MsSql.Repositories;

namespace TodoCQRS.Commands.Handlers
{
  public class CommandHandlers : IHandleMessage<ChangeTodoItemStatusCommand>
    , IHandleMessage<ChangeTodoItemTextCommand>
    , IHandleMessage<CreateTodoItemCommand>
    , IHandleMessage<DeleteTodoItemCommand>
  {
    private readonly IBus _bus;
    private readonly ITodoListAggregateRepository _repository;

    public CommandHandlers(IBus bus, ITodoListAggregateRepository repository)
    {
      _bus = bus;
      _repository = repository;
    }

    public void Handle(ChangeTodoItemStatusCommand message)
    {
      var list = FindList(message.ListId);
      if (list == null)
        return;

      list.ChangeItemStatus(message.ItemId, message.IsCompleted);
      _repository.Save(list);
    }

    public void Handle(ChangeTodoItemTextCommand message)
    {
      var list = FindList(message.ListId);
      if (list == null)
        return;

      list.ChangeItemText(message.ItemId, message.Text);
      _repository.Save(list);
    }

    public void Handle(CreateTodoItemCommand message)
    {
      var list = FindList(message.ListId);
      if (list == null)
        return;

      list.AddItem(message.ItemId, message.Text, message.IsCompleted);
      _repository.Save(list);
    }

    public void Handle(DeleteTodoItemCommand message)
    {
      var list = FindList(message.ListId);
      if (list == null)
        return;

      list.DeleteItem(message.ItemId);
      _repository.Save(list);
    }

    private TodoListAggregate FindList(Guid listId)
    {
      var list = _repository.GetById(listId);
      if (list == null)
      {
        _bus.RaiseEvent(new ListNotFoundEvent(listId));
      }
      return list;
    }
  }
}