using System;

namespace TodoCQRS.Commands.Commands.TodoItems
{
  public class ChangeTodoItemStatusCommand : TodoItemCommand
  {
    public bool IsCompleted { get; private set; }

    public ChangeTodoItemStatusCommand(Guid listId, Guid itemId, bool isCompleted) : base(listId, itemId)
    {
      IsCompleted = isCompleted;
    }
  }
}