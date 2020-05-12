using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQL.Data.UserRepository;
using GraphQL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GraphQL.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Produces("application/json")]
    [ProducesResponseType(500)]
    [ProducesResponseType(400)]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository repo;

        public UsersController(IUserRepository repo)
        {
            this.repo = repo;
        }

        /// <summary>
        /// Retrieves all users in the database
        /// </summary>
        /// <returns>A list of users</returns>
        [HttpGet()]
        [ProducesResponseType(200, Type = typeof(List<User>))]
        public async Task<ActionResult<List<User>>> GetAll()
        {
            return Ok(await repo.FetchAll());
        }

        /// <summary>
        /// Retrieves a specific user from the database
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>The retrieved user</returns>
        [HttpGet("{userId}")]
        [ProducesResponseType(200, Type = typeof(User))]
        public async Task<ActionResult<User>> Get(string userId)
        {
            return Ok(await repo.Fetch(userId));
        }

        /// <summary>
        /// Adds a user to the database
        /// </summary>
        /// <param name="user">The new user object</param>
        /// <returns>The added user</returns>
        [HttpPost()]
        [ProducesResponseType(201, Type = typeof(User))]
        public async Task<ActionResult<User>> AddUser([FromBody] User user)
        {
            User addedUser = await repo.Add(user);
            return CreatedAtAction(nameof(Get), new { userId = addedUser.Id }, addedUser);
        }

        /// <summary>
        /// Updates an existing user in the database
        /// </summary>
        /// <param name="user">The updated user object</param>
        /// <returns>The updated user</returns>
        [HttpPut("{userId}")]
        [ProducesResponseType(200, Type = typeof(User))]
        public async Task<ActionResult<User>> UpdateUser([FromBody] User user)
        {
            return Ok(await repo.Update(user));
        }

        /// <summary>
        /// Updates the password for a user
        /// </summary>
        /// <param name="passwordRequest">The updated credentials</param>
        /// <returns>A boolean status</returns>
        [HttpPut("updatepassword")]
        [ProducesResponseType(200, Type = typeof(bool))]
        public async Task<ActionResult<User>> UpdatePassword([FromBody] PasswordChangeRequest passwordRequest)
        {
            await repo.UpdatePassword(passwordRequest);
            return Ok();
        }

        /// <summary>
        /// Deletes a user from the database
        /// </summary>
        /// <param name="userId">The id of the user</param>
        /// <returns></returns>
        [HttpDelete("{taskId}")]
        [ProducesResponseType(204)]
        public async Task<ActionResult> DeleteUser(string userId)
        {
            await repo.Delete(userId);
            return NoContent();
        }
    }
}