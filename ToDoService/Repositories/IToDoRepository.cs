using ToDoService.Models;

namespace ToDoService.Repositories;

public interface IToDoRepository
{
    Task<IList<ToDo>> GetToDos();
    Task MarkTodoDone(int todoId);
}
