using System.Collections.Generic;
using System.Linq;
using TodoCQRS.Domain.Aggregates;
using TodoCQRS.Domain.Models;
using TodoCQRS.Infrastructture.Persistance.Models;

namespace TodoCQRS.Infrastructure.MsSql.Models
{
  public static class DtoMapper
  {
    public static void Map(TodoListAggregate entity, TodoListDto dto)
    {
      dto.Id = entity.Key;
      dto.ListId = entity.ListId;
      dto.Name = entity.Name;
    }

    public static void Map(int listId, TodoItem entity, TodoListItemDto dto)
    {
      dto.Id = entity.Key;
      dto.ItemId = entity.ItemId;
      dto.Text = entity.Text;
      dto.IsCompleted = entity.IsCompleted;
      dto.TodoList_Id = listId;
    }
  }
}