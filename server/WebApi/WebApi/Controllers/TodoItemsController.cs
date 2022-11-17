using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Models;

namespace WebApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class TodoItemsController : ControllerBase
	{
		private readonly TodoContext _context;

		public TodoItemsController(TodoContext context)
		{
			_context = context;
		}

		// GET: api/TodoItems
		[HttpGet]
		public async Task<ActionResult<IEnumerable<TodoItem>>> GetTodoItem()
		{
			return await _context.TodoItems.ToListAsync();
		}


		// GET: api/TodoItems/5
		[HttpGet("{id}")]
		public async Task<ActionResult<TodoItem>> GetTodoItem(long id)
		{
			var item = await _context.TodoItems.FindAsync(id);

			if (item == null)
			{
				return NotFound();
			}

			return item;
		}

        // PUT: api/TodoItems/5

        // POST: api/TodoItems
        [HttpPost]
		public async Task<ActionResult<TodoItem>> PostTodoItem(TodoItem todoItem)
		{
			_context.Add(todoItem);
			await _context.SaveChangesAsync();

			//return CreatedAtAction("GetTodoItem", new { id = todoItem.Id }, todoItem);
			return CreatedAtAction(nameof(GetTodoItem), new { id = todoItem.Id }, todoItem);
		}

		// DELETE: api/TodoItems/5
		[HttpDelete("{id}")]
		public async Task<ActionResult> DeleteTodoItem(long id)
		{
			var item = await _context.TodoItems.FindAsync(id);

			if (item == null)
			{
				return NotFound();
			}

			_context.TodoItems.Remove(item);

			await _context.SaveChangesAsync();

			return NoContent();
		}
    }
}

