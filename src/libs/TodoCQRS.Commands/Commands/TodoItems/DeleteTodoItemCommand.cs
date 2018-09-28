using System;

namespace TodoCQRS.Commands.Commands.TodoItems
{
  public class DeleteTodoItemCommand : TodoItemCommand
  {
    public DeleteTodoItemCommand(Guid listId, Guid itemId) : base(listId, itemId)
    {
    }
  }
}