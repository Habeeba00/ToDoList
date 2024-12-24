using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;
using ToDoList.Models;

namespace ToDoList.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskStatusController : ControllerBase
    {
        ToDoListDBContext db;
        IMapper mapper;
        public TaskStatusController(ToDoListDBContext db,IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }
        [HttpPut("{id}/status")]
        [SwaggerOperation(Summary = "Update task status", Description = "Updates the status of a task by its ID.")]
        [SwaggerResponse(200, "Status updated successfully")]
        [SwaggerResponse(404, "Task not found")]
        public IActionResult UpdateStatus(int id, [FromBody] string status)
        {
            var task = db.Tasks.FirstOrDefault(t => t.TaskId == id);
            if (task == null)
            {
                return NotFound("Task not found.");
            }

            task.Status = status; //  "Completed" or "Incomplete"
            db.Entry(task).State = EntityState.Modified;
            db.SaveChanges();

            return Ok(new { Message = "Task status updated successfully", Task = task });
        }



    }
}
