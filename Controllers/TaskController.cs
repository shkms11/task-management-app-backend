using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using task_management_app_backend.Models;

namespace task_management_app_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private static List<Task> tasks = new List<Task>
        {
            new Task { Id = 1, Title = "Sample Task 1", Description = "This is a sample task.", IsCompleted = false },
            new Task { Id = 2, Title = "Sample Task 2", Description = "This is another sample task.", IsCompleted = true }
        };

        [HttpGet]
        public ActionResult<IEnumerable<Task>> GetTasks()
        {
            return Ok(tasks);
        }

        [HttpPost]
        public ActionResult<Task> CreateTask([FromBody] Task newTask)
        {
            newTask.Id = tasks.Max(t => t.Id) + 1;
            tasks.Add(newTask);
            return CreatedAtAction(nameof(GetTasks), new { id = newTask.Id }, newTask);
        }

        [HttpPut("{id}")]
        public ActionResult EditTask(int id, [FromBody] Task updatedTask)
        {
            var task = tasks.FirstOrDefault(t => t.Id == id);
            if (task == null)
            {
                return NotFound();
            }

            task.Title = updatedTask.Title;
            task.Description = updatedTask.Description;
            task.IsCompleted = updatedTask.IsCompleted;

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteTask(int id)
        {
            var task = tasks.FirstOrDefault(t => t.Id == id);
            if (task == null)
            {
                return NotFound();
            }

            tasks.Remove(task);
            return NoContent();
        }
    }
}