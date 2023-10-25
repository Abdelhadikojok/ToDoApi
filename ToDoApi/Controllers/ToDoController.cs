using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System.Security.Claims;
using System.Threading.Tasks;
using ToDoApi.Data;
using ToDoApi.Dto;
using ToDoApi.Models;

namespace ToDoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoController : ControllerBase
    {
        private readonly ToDoDbContex _context; 

        public ToDoController(ToDoDbContex context)
        {
            _context = context;
        }

        [Authorize]
        [HttpGet]
        [Route("/getTasks")]
        public async Task<ActionResult> GetAllTasks()
        {
            try
            {
                var userId = Convert.ToInt32(HttpContext.User.FindFirstValue("userId"));

                var tasks = await _context.TasksCard
                    .Where(t => t.UserId == userId)
                    .GroupBy(t => t.CategoryId)
                    .Select(group => new
                    {
                        Tasks = group.Select(t => new
                        {
                            t.TaskId,
                            t.CategoryId,
                            t.Status,
                            t.DueDate,
                            t.EstimateDatenumber,
                            t.EstimateDateUnit,
                            t.Title,
                            t.importance,
                            t.Category.Name
                        }).OrderByDescending(t => t.TaskId).ToList()
                    })
                    .ToListAsync();



                return Ok(tasks);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPost]
        [Route("/addTasks")]
        public async Task<ActionResult> addTask([FromBody] TaskDto task)
        {
            try
            {
               var userid = Convert.ToInt32( HttpContext.User.FindFirstValue("userId"));
                Console.WriteLine("---------------------------" + userid);
                if (userid == null || userid < 0)
                {
                    return BadRequest("u cant add task you are un authorized");
                }
                if (task == null)
                {
                    return BadRequest("there is no task to add please enter task");
                }

                var newTask = new TaskCard
                {
                    UserId = userid,
                    CategoryId = task.CategoryId,
                    Status = task.Status,
                    DueDate = task.DueDate != null ? task.DueDate : null,
                    EstimateDatenumber = task.EstimateDatenumber,
                    EstimateDateUnit = task.EstimateDateUnit,
                    Title = task.Title,
                    importance = task.importance,
                };
                _context.TasksCard.Add(newTask);
                await _context.SaveChangesAsync();

                return Ok();
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPut]
        [Route("/updateTask")]

        public async Task<ActionResult> updateTask([FromBody] TaskDto task)
        {
            try
            {
            var userid = Convert.ToInt32(HttpContext.User.FindFirstValue("userId"));
            Console.WriteLine("---------------------------" +  userid);
            if (userid == null || userid < 0)
            {
                return BadRequest("u cant update task you are un authorized");
            }
            if (task == null)
            {
                return BadRequest("there is no task to update please enter task");
            }

            var UpdatedTask = _context.TasksCard.Where(t=> t.TaskId == task.TaskId && t.UserId == userid).FirstOrDefault();
            if (UpdatedTask == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            
            UpdatedTask.CategoryId = task.CategoryId;
            UpdatedTask.Status = task.Status;
            UpdatedTask.DueDate = task.DueDate;
            UpdatedTask.EstimateDatenumber = task.EstimateDatenumber;
            UpdatedTask.EstimateDateUnit = task.EstimateDateUnit;
            UpdatedTask.Title = task.Title;
            UpdatedTask.importance = task.importance;


            await _context.SaveChangesAsync();

            return Ok();

            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpDelete]
        [Route("/deleteTask")]
        public async Task<ActionResult> deleteTask(int taskid)
        {
            var userid = Convert.ToInt32(HttpContext.User.FindFirstValue("userId"));
            Console.WriteLine("---------------------------" + userid);
            if (userid == null || userid < 0)
            {
                return BadRequest("u cant delete task you are un authorized");
            }
            var existingTask = _context.TasksCard.FirstOrDefault(t => t.TaskId == taskid);
            if (existingTask == null)
            {
                return NotFound();
            }

            _context.TasksCard.Remove(existingTask);
            _context.SaveChanges();

            return Ok();
        }

        [Authorize]
        [HttpGet]
        [Route("/getTask")]

        public async Task<ActionResult> GetTaskByTaskId(int taskId)
        {
                try
                {
                    var userid = Convert.ToInt32(HttpContext.User.FindFirstValue("userId"));

                    if (userid == null || userid < 0)
                    {
                        return BadRequest("u cant get this task you are un authorized");
                    }

                    var task = _context.TasksCard
                    .Select(t => new
                    {
                        t.TaskId,
                        t.UserId,
                        t.CategoryId,
                        t.Status,
                        t.DueDate,
                        t.EstimateDatenumber,
                        t.EstimateDateUnit,
                        t.Title
                    })
                    .FirstOrDefault(t => t.TaskId == taskId);

                    if (task == null)
                    {
                       return NotFound();
                    }

                    return Ok();

                }
                catch(Exception ex)
                {
                    return BadRequest(ex.Message);
                }
         }

        
        [HttpGet]
        [Route("/filter")]
        public IActionResult GetTaskCards([FromQuery] TaskCardFilterDto filter)
        {
            var userId = Convert.ToInt32(HttpContext.User.FindFirstValue("userId"));

            var query = _context.TasksCard.AsQueryable();

            if (!string.IsNullOrEmpty(filter.Title))
            {
                var titleToMatch = filter.Title.ToLower();
                query = query.Where(t => t.Title.ToLower()
                .StartsWith(titleToMatch)
                );
            }

            var taskCards = query
                    .Where(t => t.UserId == userId)
                    .GroupBy(t => t.CategoryId)
                    .Select(group => new
                    {
                        Tasks = group.Select(t => new
                        {
                            t.TaskId,
                            t.CategoryId,
                            t.Status,
                            t.DueDate,
                            t.EstimateDatenumber,
                            t.EstimateDateUnit,
                            t.Title,
                            t.importance,
                            t.Category.Name
                        }).ToList()
                    })
                    .ToList();


            return Ok(taskCards);
        }

    }
}
