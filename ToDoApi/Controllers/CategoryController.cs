using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoApi.Data;

namespace ToDoApi.Controllers
{
    public class CategoryController : ControllerBase
    {
        private readonly ToDoDbContex _context;

        public CategoryController(ToDoDbContex context)
        {
            _context = context;
        }

        [Authorize]
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
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

    }
}
