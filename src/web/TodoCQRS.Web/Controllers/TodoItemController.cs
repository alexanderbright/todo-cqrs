using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TodoCQRS.Infrastructure.LiteDb.Model;
using TodoCQRS.Web.Application;
using TodoCQRS.Web.Application.Interfaces;

namespace TodoCQRS.Web.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class TodoItemController : ControllerBase
  {
    private readonly ITodoItemCommandService _todoItemCommandService;
    private readonly ITodoQueryService _todoQueryService;

    public TodoItemController(ITodoItemCommandService todoItemCommandService
      , ITodoQueryService todoQueryService)
    {
      _todoItemCommandService = todoItemCommandService;
      _todoQueryService = todoQueryService;
    }


    [HttpGet]
    [Route("all")]
    public ActionResult<IEnumerable<TodoItemViewModel>> GetItems()
    {
      return _todoQueryService.GetItems();
    }

    [HttpPost]
    [Route("create")]
    public ActionResult<Guid> CreateItem(CreateItemRequestDto dto)
    {
      return _todoItemCommandService.CreateItem(dto.ListId, dto.Text, dto.IsCompleted);
    }


    [HttpPost]
    [Route("changeText")]
    public void ChangeItemText(ChangeItemTextDto dto)
    {
      _todoItemCommandService.ChangeText(dto.ListId, dto.ItemId, dto.Text);
    }

    [HttpPost]
    [Route("changeStatus")]
    public void ChangeItemStatus(ChanteItemStatusDto dto)
    {
      _todoItemCommandService.ChangeStatus(dto.ListId, dto.ItemId, dto.IsCompleted);
    }
    

    [HttpPost]
    [Route("delete")]
    public void DeleteItem(DeleteItemDto dto)
    {
      _todoItemCommandService.DeleteItem(dto.ListId, dto.ItemId);
    }

    #region DTO

    public class DeleteItemDto
    {
      public Guid ListId;
      public Guid ItemId;
    }

    public class CreateItemRequestDto
    {
      public Guid ListId;
      public string Text;
      public bool IsCompleted;
    }

    public class ChangeItemTextDto
    {
      public Guid ListId;
      public Guid ItemId;
      public string Text;
    }

    public class ChanteItemStatusDto
    {
      public Guid ListId;
      public Guid ItemId;
      public bool IsCompleted;
    }

    #endregion
  }
}