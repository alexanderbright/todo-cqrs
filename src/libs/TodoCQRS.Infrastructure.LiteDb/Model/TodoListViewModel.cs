using System;
using LiteDB;

namespace TodoCQRS.Infrastructure.LiteDb.Model
{
  public class TodoListViewModel
  {
    [BsonId]
    public Guid ListId { get; set; }
    public string Name { get; set; }
  }
}