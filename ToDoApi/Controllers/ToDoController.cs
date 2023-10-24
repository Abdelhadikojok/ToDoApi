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
        public async Task<ActionResult> GetTasks()
        {
            try
            {
                var userid = Convert.ToInt32(HttpContext.User.FindFirstValue("userId"));
                var id = HttpContext.User.FindFirstValue("userId");
                //var category = await _context.Categories.Where(c=>c.CategoryId = )
                var tasks = await _context.TasksCard
                            .Where(t => t.UserId == userid)
                            .GroupBy(t => t.CategoryId)
                            .Select(group => new
                            {
                                Category = _context.Categories.Select(c => new {c.CategoryId,c.Name}).FirstOrDefault(c => c.CategoryId == group.Key),
                                Tasks = group.Select(t => new
                                {
                                    t.TaskId,
                                    t.CategoryId,                                  
                                    t.Status,
                                    t.DueDate,
                                    t.EstimateDate,
                                    t.Title,
                                    t.importance
                                }).ToList()
                            })
                            .ToListAsync();

                if (tasks == null)
                {
                    return NotFound();
                }
                Console.WriteLine("---------------------------" +  userid);

                return Ok(tasks);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


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
                    EstimateDate = task.EstimateDate != null ? task.DueDate : null,
                    Title = task.Title,
                    importance = task.importance,
    };
                _context.TasksCard.Add(newTask);
                await _context.SaveChangesAsync();
                //Console.WriteLine(Convert.ToInt32(HttpContext.User.FindFirstValue("userId")));

                return Ok();
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

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
             //&& t.UserId == userid
            if (UpdatedTask == null)
            {
                //Console.WriteLine("--------------------------**" + _context.TasksCard.Where(t => t.TaskId == task.TaskId && t.UserId == userid).FirstOrDefault());
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            
            UpdatedTask.CategoryId = task.CategoryId;
            UpdatedTask.Status = task.Status;
            UpdatedTask.DueDate = task.DueDate;
            UpdatedTask.EstimateDate = task.EstimateDate;
            UpdatedTask.Title = task.Title;


            await _context.SaveChangesAsync();

            //Console.WriteLine("----------------------" + UpdatedTask.UserId);

            return Ok();

            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

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

            return StatusCode(200,"success deletion");
        }

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
                        t.EstimateDate,
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
        [Route("/getCategories")]
        public async Task<ActionResult> GetAllCategories()
        {
            try
            {
                var categories = await _context.Categories
                    .Select(c => new
                    {
                        c.CategoryId,
                        c.Name,
                    })
                    .ToListAsync();

                if (categories == null)
                {
                    return NotFound();
                }

                return Ok(categories);
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
            // Query the database to filter TaskCard items based on the filter criteria.
            var query = _context.TasksCard.AsQueryable();

            if (!string.IsNullOrEmpty(filter.Title))
            {
                var titleToMatch = filter.Title.ToLower(); // Convert input to lowercase
                query = query.Where(t => t.Title.ToLower()
                .StartsWith(titleToMatch)
                );
            }

            // Add more filter criteria as needed.

            var taskCards = query.GroupBy(c => c.CategoryId)
                            .Select(group => new
                            {
                                Category = _context.Categories.Select(c => new { c.CategoryId, c.Name }).FirstOrDefault(c => c.CategoryId == group.Key),
                                Tasks = group.Select(t => new
                                {
                                    t.TaskId,
                                    t.CategoryId,
                                    t.Status,
                                    t.DueDate,
                                    t.EstimateDate,
                                    t.Title,
                                    t.importance
                                }).ToList()
                            }).ToList();
            return Ok(taskCards);
        }

    }
}
