using API.Model;
using API.Model.Core;
using API.Model.DTO;
using API.Model.DTO.Todo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Identity.Client;
using System.Net;
using static API.Model.Core.Enums;

namespace API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TodoController : BaseController
    {
        private readonly ILogger<TodoController> logger;
        private readonly BaseDbContext db;

        public TodoController(ILogger<TodoController> logger, BaseDbContext db)
        {
            this.logger = logger;
            this.db = db;
        }

        /// <summary>
        /// Get Todo
        /// </summary>
        /// <param name="TodoID">Todo ID</param>
        /// <returns code="200">Todo</returns>
        [Route("{TodoID}", Name = "GetTodo")]
        [HttpGet]
        [ProducesResponseType(typeof(Todo), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> GetTodoAsync([FromRoute] int TodoID)
        {
            List<string> errs = new List<string>();
            var todo = await this.db.Todos.Include(r => r.Category).FirstOrDefaultAsync(r => r.TodoID == TodoID);
            if(todo == null) {
                errs.Add("Todo does not exist");
            }
            if (errs.Count > 0) throw new DataValidationException(Request.Path.Value, errs);

            return StatusCode((int)HttpStatusCode.OK, todo);
        }

        /// <summary>
        /// Add Todo
        /// </summary>
        /// <param name="Data">todo data</param>
        /// <returns code="201">Create todo</returns>
        [Route("", Name = "AddTodo")]
        [HttpPost]
        [ProducesResponseType(typeof(Todo), (int)HttpStatusCode.Created)]
        public async Task<ActionResult> AddTodoAsync([FromBody] PostTodo Data)
        {
            var todo = new Todo()
            {
                Caption = Data.Caption,
                CreatedBy = CurrentUser,
                CreatedDate = DateTime.Now,
                Status = TodoStatus.InProgress,
                Category = await this.db.TodoCategories.FirstOrDefaultAsync(r => r.TodoCategoryID == Data.Category.TodoCategoryID)
            };  

            this.db.Todos.Add(todo);
            await this.db.SaveChangesAsync();
            await this.db.Entry(todo).ReloadAsync();
            return StatusCode((int)HttpStatusCode.Created, todo);
        }

        /// <summary>
        /// Save todo
        /// </summary>
        /// <param name="TodoID">Todo ID</param>
        /// <param name="Data">Todo Data</param>
        /// <returns code="200">Save todo success</returns>
        [Route("{TodoID}", Name = "UpdateTodo")]
        [HttpPut]
        [ProducesResponseType(typeof(Todo), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> SaveTodoAsync([FromRoute] int TodoID, [FromBody] Todo Data)
        {
            List<string> errs = new List<string>();
            var todo = await this.db.Todos.FirstOrDefaultAsync(r => r.TodoID == TodoID);
            #region Data Verify
            if(todo == null)
            {
                errs.Add("Todo is not found");
            }
            if(errs.Count > 0) throw new DataValidationException(Request.Path.Value, errs);
            #endregion

            todo.Caption = Data.Caption;
            todo.UpdatedDate = DateTime.Now;
            todo.UpdatedBy = CurrentUser;
            todo.Category = Data.Category;

            await this.db.SaveChangesAsync();
            await this.db.Entry(todo).ReloadAsync();

            return StatusCode((int)HttpStatusCode.OK, todo);
        }

        /// <summary>
        /// Finish Todo
        /// </summary>
        /// <param name="TodoID">Todo ID</param>
        /// <returns></returns>
        /// <exception cref="DataValidationException"></exception>
        [Route("{TodoID}/Done", Name = "FinishTodo")]
        [HttpPut]
        [ProducesResponseType(typeof(Todo), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> FinishTodoAsync([FromRoute] int TodoID)
        {
            List<string> errs = new List<string>();
            var todo = await this.db.Todos.FirstOrDefaultAsync(r => r.TodoID == TodoID);
            if(todo == null)
            {
                errs.Add("Todo is not found");
            }
            if (errs.Count > 0) throw new DataValidationException(Request.Path.Value, errs);

            todo.Status = TodoStatus.Done;
            todo.FinishBy = CurrentUser;
            todo.FinishDate = DateTime.Now;

            await this.db.SaveChangesAsync();
            await this.db.Entry(todo).ReloadAsync();

            return StatusCode((int)HttpStatusCode.OK, todo);
        }

        /// <summary>
        /// Delete todo
        /// </summary>
        /// <param name="TodoID">TodoID</param>
        /// <returns code="404">Todo not found</returns>
        /// <returns code="200">Delete todo success</returns>
        [Route("{TodoID}", Name = "Deltodo")]
        [HttpDelete]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult> DelTodoAsync([FromRoute] int TodoID)
        {
            List<string> errs = new List<string>();
            var todo = await this.db.Todos.FindAsync(TodoID);
            #region Data verify
            if(todo == null)
            {
                errs.Add("Todo is not found");
            }
            if(errs.Count > 0) throw new DataValidationException(Request.Path.Value, errs);
            #endregion

            this.db.Todos.Remove(todo);
            await this.db.SaveChangesAsync();

            return StatusCode((int)HttpStatusCode.OK);

        }

        /// <summary>
        /// Get todos by Category ID
        /// </summary>
        /// <param name="CategoryID">CategoryID</param>
        /// <returns code="404">The category have not any todos</returns>
        /// <returns code="200">List of todo</returns>
        [Route("Category/{CategoryID}/List", Name = "GetTodoListByCategory")]
        [HttpGet]
        [ProducesResponseType(typeof(List<Todo>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> GetTodoListByCategoryAsync([FromRoute] int CategoryID)
        {
            var todoList = await this.db.TodoCategories.Include(r => r.Todos)
                .Where(r => r.TodoCategoryID == CategoryID)
                .Select(r => r.Todos).ToListAsync();
            return StatusCode((int)HttpStatusCode.OK, todoList);
        }

        /// <summary>
        /// Get todo List
        /// </summary>
        /// <returns code="404">not found</returns>
        /// <returns code="200">List of todo</returns>
        [Route("List", Name = "GetTodoList")]
        [HttpGet]
        [ProducesResponseType(typeof(List<Todo>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> GetTodoList([FromQuery] bool ShowDone = false)
        {
            var todoList = await this.db.Todos.Include(r => r.Category)
                .Where(r => r.Status == TodoStatus.InProgress || ShowDone)
                .ToListAsync();
            return StatusCode((int)HttpStatusCode.OK, todoList);
        }
        
    }
}
