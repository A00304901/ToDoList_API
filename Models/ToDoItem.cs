using System;

namespace TodoListAPI.Models
{
    public class ToDoItem
    {
        public int Id { get; set; }               // Unique identifier for the ToDo item
        public DateTime DueDate { get; set; }     // Due date for the ToDo item
        public DateTime? CompletedDate { get; set; } // Date when the ToDo item was completed (nullable)
        public string Description { get; set; }    // Description of the ToDo item
    }
}
