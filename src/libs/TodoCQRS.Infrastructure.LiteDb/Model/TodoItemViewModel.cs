using System;
using LiteDB;

namespace TodoCQRS.Infrastructure.LiteDb.Model
{
  public class TodoItemViewModel
  {
    [BsonId]
    public Guid ItemId { get; set; }
    public string Text { get; set; }
    public bool IsCompleted { get; set; }
    public Guid ListId { get; set; }
    public string ListName { get; set; }
  }
}