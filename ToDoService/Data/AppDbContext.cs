using Microsoft.EntityFrameworkCore;
using ToDoService.Models;

namespace ToDoService.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<ToDo> ToDos { get; set; } = default!;
}
