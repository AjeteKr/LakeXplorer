using LakeXplorer.Models;
using LakeXplorer.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace LakeXplorer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IRepository<Users> _repository;

        public UserController(ILogger<UserController> logger, IRepository<Users> repository)
        {
            _logger = logger;
            _repository = repository;
        }

        /// <summary>
        /// Retrieves a user by their ID.
        /// </summary>
        /// <param name="id">The ID of the user to retrieve.</param>
        /// <param name="token">A cancellation token.</param>
        /// <returns>Returns 404 Not Found if the user does not exist, or 200 OK with the user information if found.</returns>

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id, CancellationToken token)
        {
            var user = await _repository.Get(id, token);

            if (user == null)
            {
                return NotFound("User not found.");
            }

            return Ok(user);
        }

        /// <summary>
        /// Creates a new user with the provided data.
        /// </summary>
        /// <param name="newUser">The Users object containing user data to create.</param>
        /// <returns>Returns 400 Bad Request if the user data is invalid, or 201 Created with the created user information if successful. Returns 500 Internal Server Error if an exception occurs.</returns>
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] Users newUser)
        {
            if (newUser == null || !ModelState.IsValid)
            {
                return BadRequest("Invalid user data.");
            }

            try
            {
                var createdUser = await _repository.Add(newUser);
                return CreatedAtAction(nameof(GetUser), new { id = createdUser.Id }, createdUser);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating user.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error: " + ex.Message);
            }
        }


        /// <summary>
        /// Deletes a user by their ID.
        /// </summary>
        /// <param name="id">The ID of the user to delete.</param>
        /// <returns>Returns 204 No Content if the user is successfully deleted. Returns 500 Internal Server Error if an exception occurs.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                var existingUser = await _repository.Get(id, CancellationToken.None);
                if (existingUser == null)
                {
                    return NotFound("User not found.");
                }

                await _repository.Delete(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting user.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error: " + ex.Message);
            }
        }


        /// <summary>
        /// Updates an existing user with the provided data.
        /// </summary>
        /// <param name="id">The ID of the user to update.</param>
        /// <param name="updatedUser">The Users object containing updated user data.</param>
        /// <returns>Returns 400 Bad Request if the user data is invalid, 404 Not Found if the user does not exist, or 200 OK with the updated user information if successful. Returns 500 Internal Server Error if an exception occurs.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] Users updatedUser)
        {
            if (updatedUser == null || !ModelState.IsValid)
            {
                return BadRequest("Invalid user data.");
            }

            var existingUser = await _repository.Get(id, CancellationToken.None);
            if (existingUser == null)
            {
                return NotFound("User not found.");
            }

            existingUser.Email = updatedUser.Email;
            existingUser.Username = updatedUser.Username;
            existingUser.Password = updatedUser.Password;

            try
            {
                _repository.Update(existingUser);
                return Ok(existingUser);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating user.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error: " + ex.Message);
            }
        }
    }
}
