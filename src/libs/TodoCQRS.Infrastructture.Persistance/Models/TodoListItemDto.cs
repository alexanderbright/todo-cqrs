using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TodoCQRS.Infrastructture.Persistance.Models
{
  [Table("[TodoItem]")]
  public sealed class TodoListItemDto
  {
    [Key] public int Id { get; set; }
    public Guid ItemId { get; set; }
    public string Text { get; set; }
    public bool IsCompleted { get; set; }
    public int TodoList_Id { get; set; }
  }
}
