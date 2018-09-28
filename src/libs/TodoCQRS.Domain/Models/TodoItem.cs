using System;
using TodoCQRS.Infrastructture.Persistance.Models;
using TodoCQRS.Infrastructure.Framework;

namespace TodoCQRS.Domain.Models
{
  public class TodoItem : Entity<int>
  {
    public Guid ItemId { get; internal set; }
    public string Text { get; internal set; }
    public bool IsCompleted { get; internal set; }

    public static class Map
    {
      public static TodoItem From(TodoListItemDto dto)
      {
        var model = new TodoItem()
        {
          Key = dto.Id,
          ItemId = dto.ItemId,
          IsCompleted = dto.IsCompleted,
          Text = dto.Text,
        };
        return model;
      }
    }
  }
}