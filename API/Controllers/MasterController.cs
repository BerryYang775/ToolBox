using API.Model;
using API.Model.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MasterController : BaseController
    {
        private readonly ILogger<MasterController> logger;
        private readonly BaseDbContext db;

        public MasterController(ILogger<MasterController> logger, BaseDbContext db)
        {
            this.logger = logger;
            this.db = db;
        }

        /// <summary>
        /// Get Todo Categories
        /// </summary>
        /// <param name="IsShowActive">Is show active</param>
        /// <returns code="200">List of Todo Category</returns>
        [Route("TodoCategory/List", Name = "GetTodoCategories")]
        [HttpGet]
        [ProducesResponseType(typeof(List<TodoCategory>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> GetTodoCategoriesAsync([FromQuery] bool IsShowActive = true)
        {
            var result = await this.db.TodoCategories.Include(r => r.Todos).Where(r => r.Active == IsShowActive).ToListAsync();
            return StatusCode((int)HttpStatusCode.OK, result);
        }

        /// <summary>
        /// Create Todo Category
        /// </summary>
        /// <param name="todoCategory">todo Category</param>
        /// <returns></returns>
        [Route("TodoCategory/Create", Name = "CreateTodoCategory")]
        [HttpPost]
        [ProducesResponseType(typeof(List<TodoCategory>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> CreateTodoCategoriesAsync([FromQuery] string todoCategory)
        {
            if (!string.IsNullOrEmpty(todoCategory))
            {
                var newTodoCategory = new TodoCategory { 
                    Title = todoCategory,
                    CreatedBy = CurrentUser,
                    CreatedDate = DateTime.Now,
                };
                await this.db.TodoCategories.AddAsync(newTodoCategory);
                await this.db.SaveChangesAsync();
                await this.db.Entry(newTodoCategory).ReloadAsync();
                return StatusCode((int)HttpStatusCode.Created, newTodoCategory);
            }
            return StatusCode((int)HttpStatusCode.NoContent);
        }

    }
}
