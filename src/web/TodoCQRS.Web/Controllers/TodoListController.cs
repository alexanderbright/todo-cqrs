using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TodoCQRS.Infrastructure.LiteDb.Model;
using TodoCQRS.Web.Application;
using TodoCQRS.Web.Application.Interfaces;

namespace TodoCQRS.Web.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class TodoListController : ControllerBase
  {
    private readonly ITodoQueryService _todoQueryService;
    private readonly ITodoCommandService _todoCommandService;

    public TodoListController(ITodoQueryService todoQueryService
      , ITodoCommandService todoCommandService)
    {
      _todoQueryService = todoQueryService;
      _todoCommandService = todoCommandService;
    }

    [HttpGet]
    [Route("all")]
    public ActionResult<List<TodoListViewModel>> GetLists()
    {
      return _todoQueryService.GetLists();
    }

    [HttpPost]
    [Route("create")]
    public ActionResult<Guid> CreateList(CreateListRequestDto request)
    {
      return _todoCommandService.CreateList(request.Name);
    }

    #region DTO

    public class CreateListRequestDto
    {
      public string Name;
    }

    #endregion
  }
}
