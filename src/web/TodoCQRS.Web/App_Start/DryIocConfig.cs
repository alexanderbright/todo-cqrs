using DryIoc;
using TodoCQRS.Commands.Handlers;
using TodoCQRS.Commands.Sagas;
using TodoCQRS.Infrastructure.Framework;
using TodoCQRS.Infrastructure.Framework.EventStore;
using TodoCQRS.Infrastructure.LiteDb;
using TodoCQRS.Infrastructure.LiteDb.Repositories;
using TodoCQRS.Infrastructure.MsSql;
using TodoCQRS.Infrastructure.MsSql.Repositories;
using TodoCQRS.Query.DataAccess;
using TodoCQRS.Query.Handlers;
using TodoCQRS.Web.Application;
using TodoCQRS.Web.Application.Handlers;
using TodoCQRS.Web.Application.Hubs;
using TodoCQRS.Web.Application.Interfaces;

namespace TodoCQRS.Web
{
  public class DryIocConfig
  {
    public DryIocConfig(IRegistrator registrator)
    {
      registrator.Register<ITodoListAggregateRepository, TodoListAggregateRepository>();
      registrator.Register<ITodoListWriteRepository, TodoListWriteRepository>();
      registrator.Register<ITodoCommandService, TodoCommandService>();
      registrator.Register<ITodoItemCommandService, TodoItemCommandService>();
      registrator.Register<ITodoQueryService, TodoQueryService>();
      registrator.Register<ITodoListRepository, TodoListRepository>();
      registrator.Register<ITodoItemCommandService, TodoItemCommandService>();
      registrator.Register<IBus, InMemoryBus>();
      registrator.Register<IEventStore, EventStoreImpl>();
      registrator.RegisterDelegate<ILiteDbConnectionOptions>(s => new LiteDbConnectionOptions("App_Data/TodoListQueryDb.db"), Reuse.Singleton);
      registrator.Register<ILiteDbProvider, LiteDbProvider>();
      registrator.RegisterDelegate<IDependencyService>(s => new DryIocDependencyService(s));
      registrator.Register<IStorageSchemeMigrator, StorageSchemeMigrator>();

      registrator.RegisterDelegate<ISqlConnectionProvider>(s => new MsSqlConnectionProvider(@"Data Source=localhost\sqlexpress;Database=TodoCQRS;user id=todo-cqrs;password=123456"));

      registrator.Register<CommandHandlers>();
      registrator.Register<DenormalizerHandlers>();
      registrator.Register<SignalRPublishHandler>();
      registrator.Register<CreateTodoListSaga>();
      registrator.Register<EventHub>();
    }
  }
}