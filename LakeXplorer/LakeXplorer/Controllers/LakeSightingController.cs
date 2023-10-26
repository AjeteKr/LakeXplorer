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
    public class LakeSightingsController : ControllerBase
    {
        private readonly ILogger<LakeSightingsController> _logger;
        private readonly IRepository<LakeSightings> _repository;

        public LakeSightingsController(ILogger<LakeSightingsController> logger, IRepository<LakeSightings> repository)
        {
            _logger = logger;
            _repository = repository;
        }


        /// <summary>
        /// Retrieves a single lake sighting by its ID.
        /// </summary>
        /// <param name="id">The ID of the lake sighting to retrieve.</param>
        /// <returns>Returns 404 Not Found if the lake sighting does not exist, or 200 OK with the lake sighting information if found.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetLakeSighting(int id)
        {
            var sighting = await _repository.Get(id, default);
            if (sighting == null)
            {
                return NotFound("Lake sighting not found.");
            }

            return Ok(sighting);
        }

        /// <summary>
        /// Creates a new lake sighting with the provided data.
        /// </summary>
        /// <param name="newSighting">The LakeSightings object containing lake sighting data to create.</param>
        /// <returns>Returns 400 Bad Request if the data is invalid, or 201 Created with the created lake sighting information if successful. Returns 500 Internal Server Error if an exception occurs.</returns>
        [HttpPost]
        public async Task<IActionResult> CreateLakeSighting([FromBody] LakeSightings newSighting)
        {
            if (newSighting == null || !ModelState.IsValid)
            {
                return BadRequest("Invalid data.");
            }

            try
            {
                var createdSighting = await _repository.Add(newSighting);
                return CreatedAtAction(nameof(GetLakeSighting), new { id = createdSighting.Id }, createdSighting);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating lake sighting.");
                return StatusCode(500, "Internal Server Error: " + ex.Message);
            }
        }

        /// <summary>
        /// Deletes a lake sighting by its ID.
        /// </summary>
        /// <param name="id">The ID of the lake sighting to delete.</param>
        /// <returns>Returns 204 No Content if the lake sighting is successfully deleted. Returns 500 Internal Server Error if an exception occurs.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLakeSighting(int id)
        {
            try
            {
                var existingSighting = await _repository.Get(id, default);
                if (existingSighting == null)
                {
                    return NotFound("Lake sighting not found.");
                }

                await _repository.Delete(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting lake sighting.");
                return StatusCode(500, "Internal Server Error: " + ex.Message);
            }
        }

        /// <summary>
        /// Updates an existing lake sighting with the provided data.
        /// </summary>
        /// <param name="id">The ID of the lake sighting to update.</param>
        /// <param name="updatedSighting">The LakeSightings object containing updated lake sighting data.</param>
        /// <returns>Returns 400 Bad Request if the data is invalid, 404 Not Found if the lake sighting does not exist, or 200 OK with the updated lake sighting information if successful. Returns 500 Internal Server Error if an exception occurs.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateLakeSighting(int id, [FromBody] LakeSightings updatedSighting)
        {
            if (updatedSighting == null || !ModelState.IsValid)
            {
                return BadRequest("Invalid data.");
            }

            var existingSighting = await _repository.Get(id, default);
            if (existingSighting == null)
            {
                return NotFound("Lake sighting not found.");
            }

            existingSighting.Longitude = updatedSighting.Longitude;
            existingSighting.Latitude = updatedSighting.Latitude;
            existingSighting.UserId = updatedSighting.UserId;
            existingSighting.LakeId = updatedSighting.LakeId;
            existingSighting.Image = updatedSighting.Image;
            existingSighting.FunFact = updatedSighting.FunFact;

            try
            {
                _repository.Update(existingSighting);
                return Ok(existingSighting);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating lake sighting.");
                return StatusCode(500, "Internal Server Error: " + ex.Message);
            }
        }
    }
}
