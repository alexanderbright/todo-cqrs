using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using Dapper.Contrib.Extensions;
using TodoCQRS.Domain.Aggregates;
using TodoCQRS.Domain.Models;
using TodoCQRS.Infrastructture.Persistance.Models;
using TodoCQRS.Infrastructure.Framework.Repositories;
using TodoCQRS.Infrastructure.MsSql.Models;

namespace TodoCQRS.Infrastructure.MsSql.Repositories
{
  public class TodoListAggregateRepository : IRepository<int, TodoListAggregate>, ITodoListAggregateRepository
  {
    private readonly ISqlConnectionProvider _sqlConnectionProvider;

    public TodoListAggregateRepository(ISqlConnectionProvider sqlConnectionProvider)
    {
      _sqlConnectionProvider = sqlConnectionProvider;
    }

    public TodoListAggregate GetById(object listId)
    {
      const string sql = "SELECT list.[Id] 'Id', list.[ListId] 'ListId', list.[Name], item.[Id] 'Id', item.[ItemId] 'ItemId', item.[Text], item.[IsCompleted], item.[TodoList_Id] " +
                         "FROM TodoList list " +
                         "LEFT JOIN [TodoItem] item ON item.[TodoList_Id] = list.[Id] " +
                         "WHERE list.[ListId] = @listId";

      TodoListDto todoList = null;
      var items = new List<TodoListItemDto>();
      using (var c = _sqlConnectionProvider.GetConnection())
      {
        c.Query<TodoListDto, TodoListItemDto, TodoListDto>(sql, (list, item) =>
        {
          if (todoList == null)
          {
            todoList = list;
          }
          if (item != null) //if there is element
            items.Add(item);

          return todoList;
        }, new { listId = listId }, splitOn: "Id");
      }
      if (todoList == null)
        return null;

      return TodoListAggregate.Map.From(todoList, items);
    }

    public void Save(TodoListAggregate listEntity)
    {
      var listDto = new TodoListDto();
      var resultItemList = new List<TodoListItemDto>();
      DtoMapper.Map(listEntity, listDto);
      using (var c = _sqlConnectionProvider.GetConnection())
      {
        using (var tran = c.BeginTransaction())
        {
          try
          {
            if (listEntity.Key == 0)
            {
              const string insertTodoListSql = "INSERT INTO [TodoList]([ListId],[Name]) OUTPUT INSERTED.[Id] VALUES(@listId, @name)";
              listDto.Id = c.QuerySingle<int>(insertTodoListSql, new {listId = listDto.ListId, name = listDto.Name}, tran);
            }
            else
            {
              //const string insertTodoListSql = "UPDATE [TodoList]([Name]) VALUES(@Name)";
              c.Update(listDto, tran);
            }

            //remove deleted items
            const string deleteRemovedSql = "DELETE FROM [TodoItem] WHERE [TodoList_Id] = @listId AND Id NOT IN @ids";
            c.Execute(deleteRemovedSql, new {listId = listDto.Id, ids = listEntity.Items.Select(e => e.Key)}, tran);

            foreach (var itemEntity in listEntity.Items)
            {
              var dto = new TodoListItemDto();
              DtoMapper.Map(listDto.Id, itemEntity, dto);

              if (itemEntity.Key == 0)
              {
                dto.Id = (int) c.Insert(dto, tran);
              }
              else if (itemEntity.Key > 0)
              {
                c.Update(dto, tran);
              }

              resultItemList.Add(dto);
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

    public void Delete(TodoListAggregate entity)
    {
      using (var c = _sqlConnectionProvider.GetConnection())
      {
        c.Delete(new TodoListDto() { Id = entity.Key });
      }
    }
  }



}