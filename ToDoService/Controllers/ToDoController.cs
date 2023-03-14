using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ToDoService.Repositories;

namespace ToDoService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ToDoController : ControllerBase
{
    private readonly IToDoRepository _repository;

    public ToDoController(IToDoRepository repository)
    {
        _repository = repository;
    }

    public async Task<IActionResult> SetToDosAsDoneSequentially()
    {
        var todos = await _repository.GetToDos();

        foreach(var todo in todos)
        {
            await _repository.MarkTodoDone(todo.Id);
        }

        return Ok();
    }
}
