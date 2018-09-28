using System;
using System.Collections.Generic;
using System.Text;

namespace TodoCQRS.Commands.Sagas.Data
{
  public class CreateTodoListSagaData
  {
    public string TodoListName { get; set; }
  }
}
