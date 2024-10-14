using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoListAPI.Data;
using TodoListAPI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoListAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoItemsController : ControllerBase
    {
        private readonly DataContext _context;

        public ToDoItemsController(DataContext context)
        {
            _context = context;
        }

        // GET: api/todoitems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ToDoItem>>> GetToDoItems()
        {
            var toDoItems = await _context.ToDoItems
                .Where(item => item.CompletedDate == null)
                .ToListAsync();

            return Ok(toDoItems);
        }

        // GET: api/todoitems/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ToDoItem>> GetToDoItem(int id)
        {
            var toDoItem = await _context.ToDoItems.FindAsync(id);

            if (toDoItem == null)
            {
                return NotFound(); // Return 404 if not found
            }

            return Ok(toDoItem); // Return the found ToDoItem
        }

        // POST: api/todoitems
        [HttpPost]
        public async Task<ActionResult<ToDoItem>> CreateToDoItem(ToDoItem toDoItem)
        {
            if (toDoItem == null)
            {
                return BadRequest("ToDoItem cannot be null.");
            }

            _context.ToDoItems.Add(toDoItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetToDoItem), new { id = toDoItem.Id }, toDoItem);
        }

        // PUT: api/todoitems/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<ToDoItem>> UpdateToDoItem(int id, ToDoItem toDoItem)
        {
            if (toDoItem == null)
            {
                return BadRequest("ToDoItem cannot be null.");
            }

            // Find the existing ToDoItem
            var existingToDoItem = await _context.ToDoItems.FindAsync(id);
            if (existingToDoItem == null)
            {
                return NotFound(); // Return 404 if not found
            }

            // Update the CompletedDate to the current datetime
            existingToDoItem.CompletedDate = DateTime.UtcNow; // or DateTime.Now based on your preference

            // Save changes to the database
            await _context.SaveChangesAsync();

            return Ok(existingToDoItem); // Return the updated ToDoItem
        }

        // DELETE: api/todoitems/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteToDoItem(int id)
        {
            // Find the existing ToDoItem
            var toDoItem = await _context.ToDoItems.FindAsync(id);
            if (toDoItem == null)
            {
                return NotFound(); // Return 404 if not found
            }

            // Remove the ToDoItem from the context
            _context.ToDoItems.Remove(toDoItem);
            await _context.SaveChangesAsync();

            return NoContent(); // Return 204 No Content on successful deletion
        }
    }
}
