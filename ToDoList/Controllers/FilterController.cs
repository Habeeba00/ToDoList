using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using ToDoList.DTOs;
using ToDoList.Models;

namespace ToDoList.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilterController : ControllerBase
    {
        ToDoListDBContext db;
        IMapper mapper;
        public FilterController(ToDoListDBContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }
        [HttpGet("completed")]
        [SwaggerOperation(Summary = "Get tasks by completion status", Description = "Retrieve tasks based on their completion status (completed or uncompleted).")]
        [SwaggerResponse(200, "Tasks found", typeof(List<DisplayTaskDTO>))]
        [SwaggerResponse(404, "No tasks found with the specified status")]
        public IActionResult GetByCompletionStatus([FromQuery] bool completed)
        {
            var status = completed ? "Completed" : "incompleted";
            var tasks = db.Tasks.Where(t => t.Status == status).ToList();

            if (tasks == null || !tasks.Any())
            {
                return NotFound($"No tasks found with status '{status}'.");
            }

            List<DisplayTaskDTO> displayTaskDTOs = mapper.Map<List<DisplayTaskDTO>>(tasks);
            return Ok(displayTaskDTOs);
        }

     

        [HttpGet("duedate/after")]
        [SwaggerOperation(Summary = "Get tasks due after a certain date", Description = "Retrieve tasks that are due after the specified date.")]
        [SwaggerResponse(200, "Tasks found", typeof(List<DisplayTaskDTO>))]
        [SwaggerResponse(404, "No tasks found after the specified date")]
        public IActionResult GetTasksDueAfter([FromQuery] DateOnly date)
        {
            var tasks = db.Tasks.Where(t => t.DueDate > date).ToList();

            if (tasks == null || !tasks.Any())
            {
                return NotFound($"No tasks found due after {date.ToString("yyyy-MM-dd")}.");
            }

            List<DisplayTaskDTO> displayTaskDTOs = mapper.Map<List<DisplayTaskDTO>>(tasks);
            return Ok(displayTaskDTOs);
        }



    }
}
