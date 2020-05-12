using System.Collections.Generic;
using System.Threading.Tasks;
using GraphQL.Data.TaskItemRepository;
using GraphQL.Models;
using Microsoft.AspNetCore.Mvc;

namespace GraphQL.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Produces("application/json")]
    [ProducesResponseType(500)]
    [ProducesResponseType(400)]
    public class TasksController : ControllerBase
    {
        private readonly ITaskItemRepository repo;

        public TasksController(ITaskItemRepository repo)
        {
            this.repo = repo;
        }

        /// <summary>
        /// Retrieves all tasks in the database
        /// </summary>
        /// <returns>A list of tasks</returns>
        [HttpGet()]
        [ProducesResponseType(200, Type = typeof(List<TaskItem>))]
        public async Task<ActionResult<List<TaskItem>>> GetAll()
        {
            return Ok(await repo.FetchAll());
        }

        /// <summary>
        /// Retrieves a specific task from the database
        /// </summary>
        /// <param name="taskId"></param>
        /// <returns>The retrieved task</returns>
        [HttpGet("{taskId}")]
        [ProducesResponseType(200, Type = typeof(TaskItem))]
        public async Task<ActionResult<TaskItem>> Get(string taskId)
        {
            return Ok(await repo.Fetch(taskId));
        }

        /// <summary>
        /// Retrieves all tasks for a specific user
        /// </summary>
        /// <param name="userId">The id of the user</param>
        /// <returns>A list of the user's tasks</returns>
        [HttpGet("~/Users/{userId}/tasks")]
        [ProducesResponseType(200, Type = typeof(List<TaskItem>))]
        public async Task<ActionResult<List<TaskItem>>> GetForUser(string userId)
        {
            return Ok(await repo.FetchTasksForUser(userId));
        }

        /// <summary>
        /// Adds a task to the database
        /// </summary>
        /// <param name="task">The new task object</param>
        /// <returns>The added task</returns>
        [HttpPost()]
        [ProducesResponseType(201, Type = typeof(TaskItem))]
        public async Task<ActionResult<TaskItem>> AddTask([FromBody] TaskItem task)
        {
            TaskItem addedTask = await repo.Add(task);
            return CreatedAtAction(nameof(Get), new { taskId = addedTask.Id }, addedTask);
        }

        /// <summary>
        /// Updates an existing task in the database
        /// </summary>
        /// <param name="task">The updated task object</param>
        /// <returns>The updated task</returns>
        [HttpPut("{taskId}")]
        [ProducesResponseType(200, Type = typeof(TaskItem))]
        public async Task<ActionResult<TaskItem>> UpdateTask([FromBody] TaskItem task)
        {
            return Ok(await repo.Update(task));
        }

        /// <summary>
        /// Deletes a task from the database
        /// </summary>
        /// <param name="taskId">The id of the task</param>
        /// <returns></returns>
        [HttpDelete("{taskId}")]
        [ProducesResponseType(204)]
        public async Task<ActionResult> DeleteTask(string taskId)
        {
            await repo.Delete(taskId);
            return NoContent();
        }
    }
}