using Microsoft.EntityFrameworkCore;
using ToDo.Models;

namespace ToDo.Data;

public class AppDbContext : DbContext
{
    private const string connectionString =
"Data Source=.\\SQLSERVER;Database=ToDo;Integrated Security=True;Encrypt=False;";

    public DbSet<ToDoModel> ToDos { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    => options.UseSqlServer(connectionString);
}
