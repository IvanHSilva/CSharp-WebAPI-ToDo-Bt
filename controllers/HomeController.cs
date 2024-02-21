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
    public IActionResult Get([FromServices] AppDbContext context) // DI
    => Ok(context.ToDos.ToList());

    [HttpGet("/{id:int}")]
    public IActionResult GetById([FromRoute] int id, [FromServices] AppDbContext context) // DI
    {
        ToDoModel todo = context.ToDos.FirstOrDefault(t => t.Id == id)!;
        if (todo == null) return NotFound();

        return Ok(todo);
    }

    [HttpPost("/")]
    public IActionResult Post([FromBody] ToDoModel todo, [FromServices] AppDbContext context)
    {
        context.ToDos.Add(todo);
        context.SaveChanges();

        return Created($"/{todo.Id}", todo);
    }

    [HttpPut("/{id:int}")]
    public IActionResult Put([FromRoute] int id, [FromBody] ToDoModel model,
    [FromServices] AppDbContext context)
    {
        ToDoModel todo = context.ToDos.FirstOrDefault(t => t.Id == id)!;
        if (todo == null) return NotFound();

        todo.Title = model.Title;
        todo.Done = model.Done;

        context.Update(todo);
        context.SaveChanges();

        return Ok(todo);
    }

    [HttpDelete("/{id:int}")]
    public IActionResult Delete([FromRoute] int id, [FromServices] AppDbContext context)
    {
        ToDoModel todo = context.ToDos.FirstOrDefault(t => t.Id == id)!;
        if (todo == null) return NotFound();

        context.Remove(todo);
        context.SaveChanges();

        return Ok(todo);
    }
}
