using System;

namespace TodoCQRS.Web.Application.Interfaces
{
  public interface ITodoCommandService
  {
    Guid CreateList(string name);
  }
}