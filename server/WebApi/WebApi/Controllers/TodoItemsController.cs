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

		// PUT: api/TodoItems/5
		[HttpPost]
		public async Task<ActionResult<TodoItem>> PostTodoItem(TodoItem todoItem)
		{
			_context.Add(todoItem);
			await _context.SaveChangesAsync();

			return CreatedAtAction(nameof(GetTodoItem), new { id = todoItem.Id }, todoItem);
		}

		// POST: api/TodoItems

		// DELETE: api/TodoItems/5
	}
}

