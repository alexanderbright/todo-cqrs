using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using Dapper.Contrib.Extensions;
using TodoCQRS.Domain.Aggregates;
using TodoCQRS.Domain.Models;
using TodoCQRS.Infrastructure.Framework.Repositories;
using TodoCQRS.Infrastructure.SQLite.Models;

namespace TodoCQRS.Infrastructure.SQLite.Repositories
{
  public class TodoListAggregateRepository : IRepository<int, TodoListAggregate>, ITodoListAggregateRepository
  {
    private readonly ISqlConnectionProvider _sqlConnectionProvider;

    public TodoListAggregateRepository(ISqlConnectionProvider sqlConnectionProvider)
    {
      _sqlConnectionProvider = sqlConnectionProvider;
    }

    public TodoListAggregate GetById(int id)
    {
      const string sql = "SELECT [Id], [Name] FROM TodoList list " +
                         "INNER JOIN [TodoItem] item ON item.[TodoList_Id] = list.[Id] " +
                         "WHERE list.[Id] = @id";

      TodoListDto todoList = null;
      var items = new List<TodoItem>();
      using (var c = _sqlConnectionProvider.GetConnection())
      {
        c.Query<TodoListDto, TodoListItemDto, TodoListDto>(sql, (list, item) =>
        {
          if (todoList == null)
          {
            todoList = list;
            todoList.Items = new List<TodoListItemDto>();
          }
          todoList.Items.Add(item);

          return todoList;
        }, id, splitOn: "[Id]");
      }
      if (todoList == null)
        return null;
      return TodoListAggregate.Factory.Create(todoList.Id, todoList.Name, items.Select(i => new TodoItem(i.Id, i.Text, i.IsCompleted)));
    }

    public void Save(TodoListAggregate entity)
    {
      using (var c = _sqlConnectionProvider.GetConnection())
      {
        using (var tran = c.BeginTransaction())
        {
          try
          {
            if (entity.Id == 0)
            {
              const string insertTodoListSql = "INSERT INTO [TodoList]([Name]) VALUES(@Name)";
              c.Execute(insertTodoListSql, new { entity.Name }, tran);
              entity.Id = (int)c.LastInsertRowId;
            }
            else
            {
              //const string insertTodoListSql = "UPDATE [TodoList]([Name]) VALUES(@Name)";
              c.Update<dynamic>(new { entity.Id, entity.Name }, tran);
            }

            foreach (var item in entity.Items)
            {
              if (item.Id == 0)
              {
                c.Insert(new TodoListItemDto()
                {
                  Text = item.Text,
                  IsCompleted = item.IsCompleted,
                  TodoList_Id = entity.Id
                }, tran);
                item.Id = (int)c.LastInsertRowId;
              }
              else if (item.Id > 0)
              {
                if (item.IsDeleted)
                {
                  c.Delete(item);
                }
                else
                {
                  c.Update<dynamic>(new TodoListItemDto()
                  {
                    Id = item.Id,
                    Text = item.Text,
                    IsCompleted = item.IsCompleted,
                    TodoList_Id = entity.Id
                  }, tran);
                }
              }
            }

            tran.Commit();
          }
          catch
          {
            tran.Rollback();
            throw;
          }
        }
      }
    }

    public void DeleteById(int id)
    {
      using (var c = _sqlConnectionProvider.GetConnection())
      {
        c.Delete(new TodoListDto() { Id = id });
      }
    }
  }
}