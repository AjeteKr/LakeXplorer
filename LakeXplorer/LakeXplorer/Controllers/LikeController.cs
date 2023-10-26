using LakeXplorer.Models;
using LakeXplorer.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace LakeXplorer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LikesController : ControllerBase
    {
        private readonly ILogger<LikesController> _logger;
        private readonly IRepository<Likes> _repository;

        public LikesController(ILogger<LikesController> logger, IRepository<Likes> repository)
        {
            _logger = logger;
            _repository = repository;
        }


        /// <summary>
        /// Retrieves likes by their ID.
        /// </summary>
        /// <param name="id">The ID of the likes to retrieve.</param>
        /// <returns>Returns 404 Not Found if the likes do not exist, or 200 OK with the likes information if found.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetLikes(int id)
        {
            var likes = await _repository.Get(id, default);
            if (likes == null)
            {
                return NotFound("Likes not found.");
            }

            return Ok(likes);
        }

        /// <summary>
        /// Creates a new likes entity with the provided data.
        /// </summary>
        /// <param name="newLikes">The Likes object containing likes data to create.</param>
        /// <returns>Returns 400 Bad Request if the data is invalid, or 201 Created with the created likes information if successful. Returns 500 Internal Server Error if an exception occurs.</returns>
        [HttpPost]
        public async Task<IActionResult> CreateLikes([FromBody] Likes newLikes)
        {
            if (newLikes == null || !ModelState.IsValid)
            {
                return BadRequest("Invalid data.");
            }

            try
            {
                var createdLikes = await _repository.Add(newLikes);
                return CreatedAtAction(nameof(GetLikes), new { id = createdLikes.Id }, createdLikes);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating likes.");
                return StatusCode(500, "Internal Server Error: " + ex.Message);
            }
        }

        /// <summary>
        /// Deletes likes by their ID.
        /// </summary>
        /// <param name="id">The ID of the likes to delete.</param>
        /// <returns>Returns 204 No Content if the likes are successfully deleted. Returns 500 Internal Server Error if an exception occurs.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLikes(int id)
        {
            try
            {
                var existingLikes = await _repository.Get(id, default);
                if (existingLikes == null)
                {
                    return NotFound("Likes not found.");
                }

                await _repository.Delete(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting likes.");
                return StatusCode(500, "Internal Server Error: " + ex.Message);
            }
        }

        /// <summary>
        /// Updates an existing likes entity with the provided data.
        /// </summary>
        /// <param name="id">The ID of the likes to update.</param>
        /// <param name="updatedLikes">The Likes object containing updated likes data.</param>
        /// <returns>Returns 400 Bad Request if the data is invalid, 404 Not Found if the likes do not exist, or 200 OK with the updated likes information if successful. Returns 500 Internal Server Error if an exception occurs.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateLikes(int id, [FromBody] Likes updatedLikes)
        {
            if (updatedLikes == null || !ModelState.IsValid)
            {
                return BadRequest("Invalid data.");
            }

            var existingLikes = await _repository.Get(id, default);
            if (existingLikes == null)
            {
                return NotFound("Likes not found.");
            }

            existingLikes.UserId = updatedLikes.UserId;
            existingLikes.SightingId = updatedLikes.SightingId;

            try
            {
                _repository.Update(existingLikes);
                return Ok(existingLikes);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating likes.");
                return StatusCode(500, "Internal Server Error: " + ex.Message);
            }
        }
    }
}
