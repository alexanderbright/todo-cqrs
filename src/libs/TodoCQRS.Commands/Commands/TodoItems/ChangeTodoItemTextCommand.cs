using System;

namespace TodoCQRS.Commands.Commands.TodoItems
{
  public class ChangeTodoItemTextCommand : TodoItemCommand
  {
    public string Text { get; private set; }

    public ChangeTodoItemTextCommand(Guid listId, Guid itemId, string text) : base(listId, itemId)
    {
      Text = text;
    }
  }
}