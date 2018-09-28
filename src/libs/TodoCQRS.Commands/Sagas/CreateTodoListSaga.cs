using TodoCQRS.Commands.Commands;
using TodoCQRS.Commands.Sagas.Data;
using TodoCQRS.Domain.Aggregates;
using TodoCQRS.Domain.Events;
using TodoCQRS.Infrastructure.Framework;
using TodoCQRS.Infrastructure.Framework.EventStore;
using TodoCQRS.Infrastructure.Framework.Repositories;
using TodoCQRS.Infrastructure.MsSql.Repositories;

namespace TodoCQRS.Commands.Sagas
{
  public class CreateTodoListSaga : Saga<CreateTodoListSagaData>
    , IStartWithMessage<CreateTodoListCommand>
    , IHandleMessage<TodoListCreatedEvent>
  {
    private readonly IBus _bus;
    private readonly ITodoListAggregateRepository _todoListAggregateRepository;

    public CreateTodoListSaga(IBus bus, ITodoListAggregateRepository todoListAggregateRepository)
    {
      _bus = bus;
      _todoListAggregateRepository = todoListAggregateRepository;
    }

    public void Handle(CreateTodoListCommand message)
    {
      var list = TodoListAggregate.Factory.Create(message.ListId, message.ListName);
      _todoListAggregateRepository.Save(list);
    }

    public void Handle(TodoListCreatedEvent message)
    {
      IsCompleted = true;
    }
  }
}