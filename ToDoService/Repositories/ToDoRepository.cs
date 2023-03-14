using Microsoft.EntityFrameworkCore;
using ToDoService.Data;
using ToDoService.Models;

namespace ToDoService.Repositories;

public class ToDoRepository : IToDoRepository
{
    private readonly AppDbContext _context;

    public ToDoRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IList<ToDo>> GetToDos()
    {
        // they need to be untracked because otherwise we cannot change them inside a foreach loop
        var todos = await _context.ToDos
            .AsNoTracking().ToListAsync();

        return todos;
    }

    public async Task MarkTodoDone(int todoId)
    {
        var todo = await _context.ToDos.FirstOrDefaultAsync(td => td.Id == todoId);

        if (todo is null)
            return;

        todo.Status = "Done";
        _context.ToDos.Update(todo);
    }
}
