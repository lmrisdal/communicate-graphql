using System.Collections.Generic;
using System.Threading.Tasks;
using GraphQL.Data.TodoItemRepository;
using GraphQL.Models;
using Microsoft.AspNetCore.Mvc;

namespace GraphQL.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Produces("application/json")]
    [ProducesResponseType(500)]
    [ProducesResponseType(400)]
    public class TodosController : ControllerBase
    {
        private readonly ITodoItemRepository repo;

        public TodosController(ITodoItemRepository repo)
        {
            this.repo = repo;
        }

        /// <summary>
        /// Retrieves all todos in the database
        /// </summary>
        /// <returns>A list of todos</returns>
        [HttpGet()]
        [ProducesResponseType(200, Type = typeof(List<TodoItem>))]
        public async Task<ActionResult<List<TodoItem>>> GetAll()
        {
            return Ok(await repo.FetchAll());
        }

        /// <summary>
        /// Retrieves a specific TodoItem from the database
        /// </summary>
        /// <param name="todoId"></param>
        /// <returns>The retrieved todo</returns>
        [HttpGet("{todoId}")]
        [ProducesResponseType(200, Type = typeof(TodoItem))]
        public async Task<ActionResult<TodoItem>> Get(string todoId)
        {
            return Ok(await repo.Fetch(todoId));
        }

        /// <summary>
        /// Retrieves all todos for a specific user
        /// </summary>
        /// <param name="userId">The id of the user</param>
        /// <returns>A list of the user's todo items</returns>
        [HttpGet("~/Users/{userId}/todos")]
        [ProducesResponseType(200, Type = typeof(List<TodoItem>))]
        public async Task<ActionResult<List<TodoItem>>> GetForUser(string userId)
        {
            return Ok(await repo.FetchTodosForUser(userId));
        }

        /// <summary>
        /// Adds a todo to the database
        /// </summary>
        /// <param name="todo">The new todo object</param>
        /// <returns>The added todo item</returns>
        [HttpPost()]
        [ProducesResponseType(201, Type = typeof(TodoItem))]
        public async Task<ActionResult<TodoItem>> AddTodo([FromBody] TodoItem todo)
        {
            TodoItem addedTodo = await repo.Add(todo);
            return CreatedAtAction(nameof(Get), new { todoId = addedTodo.Id }, addedTodo);
        }

        /// <summary>
        /// Updates an existing todo in the database
        /// </summary>
        /// <param name="todo">The updated todo object</param>
        /// <returns>The updated todo</returns>
        [HttpPut("{todoId}")]
        [ProducesResponseType(200, Type = typeof(TodoItem))]
        public async Task<ActionResult<TodoItem>> UpdateTodo([FromBody] TodoItem todo)
        {
            return Ok(await repo.Update(todo));
        }

        /// <summary>
        /// Deletes a todo from the database
        /// </summary>
        /// <param name="todoId">The id of the todo</param>
        /// <returns></returns>
        [HttpDelete("{todoId}")]
        [ProducesResponseType(204)]
        public async Task<ActionResult> DeleteTodo(string todoId)
        {
            await repo.Delete(todoId);
            return NoContent();
        }
    }
}