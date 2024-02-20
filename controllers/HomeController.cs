using Microsoft.AspNetCore.Mvc;
using ToDo.Data;
using ToDo.Models;

namespace Todo.Controllers;

[ApiController]
public class HomeController : ControllerBase
{

    [HttpGet("/")]
    public List<ToDoModel> Get([FromServices] AppDbContext context) // DI
    {
        return [.. context.ToDos];
    }
}
