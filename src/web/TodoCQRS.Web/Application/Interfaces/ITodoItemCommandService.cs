using System;

namespace TodoCQRS.Web.Application.Interfaces
{
  public interface ITodoItemCommandService
  {
    void ChangeText(Guid listId, Guid itemId, string text);
    void ChangeStatus(Guid listId, Guid itemId, bool isCompleted);
    void DeleteItem(Guid listId, Guid itemId);
    Guid CreateItem(Guid listId, string text, bool isCompleted);
  }
}