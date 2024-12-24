using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;
using ToDoList.DTOs;
using ToDoList.Models;

namespace ToDoList.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PriorityController : ControllerBase
    {
        ToDoListDBContext db;
        IMapper mapper;
        public PriorityController(ToDoListDBContext db,IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
            
        }

        [HttpGet("Priority")]
        [SwaggerOperation(Summary = "Get tasks by priority level", Description = "Retrieve tasks based on their priority level (Low, Medium, High).")]
        [SwaggerResponse(200, "Tasks found", typeof(List<DisplayTaskDTO>))]
        [SwaggerResponse(400, "Invalid priority level. Allowed values are 'Low', 'Medium', or 'High'.")]
        [SwaggerResponse(404, "No tasks found with the specified priority")]
        public IActionResult GetByPriority([FromQuery] string priority)
        {
            var task = db.Tasks.Where(t => t.PriorityLevel.ToLower() == priority.ToLower()).ToList();

            if (task == null || !task.Any())
            {
                return NotFound($"No tasks found with priority '{priority}'.");
            }
            List<DisplayTaskDTO> displayTaskDTOs = mapper.Map<List<DisplayTaskDTO>>(task);
            return Ok(displayTaskDTOs);
        }




        [HttpPut("{id}/priority")]
        [SwaggerOperation(Summary = "Update task priority", Description = "Updates the priority level of a task by its ID.")]
        [SwaggerResponse(200, "Priority updated successfully")]
        [SwaggerResponse(404, "Task not found")]
        public IActionResult UpdatePriority(int id, [FromBody] string priorityLevel)
        {
            var task = db.Tasks.FirstOrDefault(t => t.TaskId == id);
            if (task == null)
            {
                return NotFound("Task not found.");
            }

            task.PriorityLevel = priorityLevel; // "Low", "Medium", "High"
            db.Entry(task).State = EntityState.Modified;
            db.SaveChanges();

            return Ok(new { Message = "Task priority updated successfully", Task = task });
        }
    }
}
