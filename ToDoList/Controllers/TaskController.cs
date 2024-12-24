using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;
using ToDoList.DTOs;
using ToDoList.Models;

namespace ToDoList.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        ToDoListDBContext db;
        IMapper mapper;
        public TaskController(ToDoListDBContext db, IMapper mapper)
        {
           this.db = db;
           this.mapper = mapper;
        }
        [HttpGet]
        [SwaggerOperation(Summary = "Get all Tasks", Description = "")]
        [SwaggerResponse(200, "if task exists ", typeof(DisplayTaskDTO))]
        [SwaggerResponse(400, "not founded")]
        public IActionResult Get() 
        {
            List<Tasks> tasks =db.Tasks.ToList();
            if (tasks == null || !tasks.Any())
            {
                return NotFound("No tasks found.");
            }
            List<DisplayTaskDTO> displayTaskDTOs=mapper.Map<List<DisplayTaskDTO>>(tasks);
            return Ok(displayTaskDTOs);
        }



        [HttpPost("Add")]
        [SwaggerOperation(Summary = "Add task", Description = "")]
        [SwaggerResponse(201, "if task created succcesfully")]
        [SwaggerResponse(400, "if invalid task data")]
        public IActionResult Post( AddTaskDTO addTaskDTO) 
        {
            if (ModelState.IsValid) 
            {
                Tasks tasks = mapper.Map<Tasks>(addTaskDTO);
                db.Tasks.Add(tasks);
                int taskCount = db.Tasks.Count();
                db.SaveChanges();
                return Ok(db.Tasks.ToList());

            }
            else
            {
                return BadRequest(ModelState);
            }
        }


        [HttpGet("id")]
        [SwaggerOperation(Summary = "Can search on task by task id ", Description = "")]
        [SwaggerResponse(200, "Return task data", typeof(DisplayTaskDTO))]
        [SwaggerResponse(404, "If no task founded")]
        public IActionResult GetById(int id) 
        {
            Tasks tasks = db.Tasks.Where(n => n.TaskId == id).FirstOrDefault();

            if (tasks == null)
            {
                return NotFound();

            }
            else
            {
                DisplayTaskDTO displayTaskDTO=mapper.Map<DisplayTaskDTO>(tasks);
                return Ok(displayTaskDTO);
            
            }
        }



        [HttpPut("id")]
        [SwaggerOperation(Summary = "Can edit  task by task id ", Description = "")]
        [SwaggerResponse(200, "Edited successfully")]
        [SwaggerResponse(404, "If no task founded")]
        public IActionResult Edit(int id,AddTaskDTO addTaskDTO) 
        {
            var task = db.Tasks.FirstOrDefault(t => t.TaskId== id);
            if (task == null)
            {
                return NotFound();
            }
            mapper.Map(addTaskDTO, task);
            //mapper.Map<Tasks>(addTaskDTO);
            db.Entry(task).State = EntityState.Modified;
            db.SaveChanges();
            return Ok(task);
        }


        [HttpDelete("id")]
        [SwaggerOperation(Summary = "Can delete  task by task id ", Description = "")]
        [SwaggerResponse(200, "Deleted successfully")]
        [SwaggerResponse(404, "If no task founded")]
        public IActionResult Delete(int id) 
        {
            var task = db.Tasks.FirstOrDefault(task => task.TaskId== id);
            if (task == null) return NotFound();
            db.Tasks.Remove(task);
            db.SaveChanges();
            return Ok(task);

        }
    }
}
