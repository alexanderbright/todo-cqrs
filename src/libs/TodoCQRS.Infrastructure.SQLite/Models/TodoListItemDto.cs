using System;
using System.Collections.Generic;
using System.Text;

namespace TodoCQRS.Infrastructure.SQLite.Models
{
  public class TodoListItemDto
  {
    public int Id;
    public string Text;
    public bool IsCompleted;
    public int TodoList_Id;
  }
}
