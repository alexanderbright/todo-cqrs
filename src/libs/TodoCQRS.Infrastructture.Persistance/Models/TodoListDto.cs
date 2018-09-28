using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TodoCQRS.Infrastructture.Persistance.Models
{
  [Table("TodoList")]
  public sealed class TodoListDto
  {
    [Key] public int Id { get; set; }
    public Guid ListId { get; set; }
    public string Name { get; set; }
  }
}
