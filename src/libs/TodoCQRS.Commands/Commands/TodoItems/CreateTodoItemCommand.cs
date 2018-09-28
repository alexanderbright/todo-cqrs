using System;
using TodoCQRS.Infrastructure.Framework;

namespace TodoCQRS.Commands.Commands.TodoItems
{
  public class CreateTodoItemCommand : TodoItemCommand
  {
    public string Text { get; }
    public bool IsCompleted { get; }

    public CreateTodoItemCommand(Guid listId, Guid itemId, string text, bool isCompleted) :base(listId, itemId)
    {
      Text = text;
      IsCompleted = isCompleted;
    }
  }
}