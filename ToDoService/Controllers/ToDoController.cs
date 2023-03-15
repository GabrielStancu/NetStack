using Microsoft.AspNetCore.Mvc;
using ToDoService.Extensions;
using ToDoService.Models;
using ToDoService.Repositories;

namespace ToDoService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ToDoController : ControllerBase
{
    private readonly IToDoRepository _repository;
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public ToDoController(IToDoRepository repository, IServiceScopeFactory serviceScopeFactory)
    {
        _repository = repository;
        _serviceScopeFactory = serviceScopeFactory;
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

    public async Task<IActionResult> SetToDosAsDoneInParallel()
    {
        var todos = await _repository.GetToDos();
        var taskList = new List<Task>(todos.Count);

        foreach(var todo in todos)
        {
            var markToDoDoneTask = MarkToDoDone(todo);
            taskList.Add(markToDoDoneTask);
        }

        await Task.WhenAll(taskList);

        return Ok();
    }

    // Create a new instance of the repository within a new scope
    // Wrap the instance into the func object that will use it as callback to set the todo as done
    private async Task MarkToDoDone(ToDo todo)
    {
        var markToDo = async (IToDoRepository toDoRepository) => await toDoRepository.MarkTodoDone(todo.Id);
        await _serviceScopeFactory.DoAsync(markToDo);
    }
}
