using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TodoCQRS.Infrastructure.SQLite.Models
{
  [Table("TodoList")]
  public class TodoListDto
  {
    public int Id;
    public string Name;
    public List<TodoListItemDto> Items;
  }
}
