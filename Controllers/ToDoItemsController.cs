using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using TodoListAPI.Models;

namespace TodoListAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoItemsController : ControllerBase
    {
        private static List<ToDoItem> _toDoItems = new List<ToDoItem>();
        private static int _nextId = 1;

        [HttpGet]
        public ActionResult<List<ToDoItem>> Get()
        {
            return _toDoItems;
        }

        [HttpGet("{id}")]
        public ActionResult<ToDoItem> Get(int id)
        {
            var item = _toDoItems.FirstOrDefault(i => i.Id == id);
            if (item == null) return NotFound();
            return item;
        }

        [HttpPost]
        public ActionResult<ToDoItem> Post(ToDoItem item)
        {
            item.Id = _nextId++;
            _toDoItems.Add(item);
            return CreatedAtAction(nameof(Get), new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, ToDoItem updatedItem)
        {
            var item = _toDoItems.FirstOrDefault(i => i.Id == id);
            if (item == null) return NotFound();
            item.Title = updatedItem.Title;
            item.Description = updatedItem.Description;
            item.IsCompleted = updatedItem.IsCompleted;
            item.DueDate = updatedItem.DueDate;
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var item = _toDoItems.FirstOrDefault(i => i.Id == id);
            if (item == null) return NotFound();
            _toDoItems.Remove(item);
            return NoContent();
        }
    }
}
