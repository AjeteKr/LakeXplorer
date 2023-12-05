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
