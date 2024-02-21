using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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

    [HttpGet("/{id:int}")]
    public ToDoModel GetById([FromRoute] int id, [FromServices] AppDbContext context) // DI
    {
        return context.ToDos.FirstOrDefault(t => t.Id == id)!;
    }

    [HttpPost("/")]
    public ToDoModel Post([FromBody] ToDoModel todo, [FromServices] AppDbContext context)
    {
        context.ToDos.Add(todo);
        context.SaveChanges();

        return todo;
    }

    [HttpPut("/{id:int}")]
    public ToDoModel Put([FromRoute] int id, [FromBody] ToDoModel model,
    [FromServices] AppDbContext context)
    {
        ToDoModel todo = context.ToDos.FirstOrDefault(t => t.Id == id)!;
        if (todo == null) return todo!;

        todo.Title = model.Title;
        todo.Done = model.Done;

        context.Update(todo);
        context.SaveChanges();

        return todo;
    }

    [HttpDelete("/{id:int}")]
    public ToDoModel Delete([FromRoute] int id, [FromServices] AppDbContext context)
    {
        ToDoModel todo = context.ToDos.FirstOrDefault(t => t.Id == id)!;
        if (todo == null) return todo!;

        context.Remove(todo);
        context.SaveChanges();

        return todo;
    }
}
