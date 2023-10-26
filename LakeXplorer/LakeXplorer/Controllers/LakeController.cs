using LakeXplorer.DTOs;
using LakeXplorer.Models;
using LakeXplorer.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LakeXplorer.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/[controller]")]
    public class LakesController : ControllerBase
    {
        private readonly ILogger<LakesController> _logger;
        private readonly IRepository<Lakes> _repository;

        
        public LakesController(ILogger<LakesController> logger, IRepository<Lakes> repository)
        {
            _logger = logger;
            _repository = repository;
        }

        /// <summary>
        /// Retrieves a single lake by its ID.
        /// </summary>
        /// <param name="id">The ID of the lake to retrieve.</param>
        /// <returns>Returns 404 Not Found if the lake does not exist, or 200 OK with the lake information if found.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetLake(int id)
        {
            var lake = await _repository.Get(id, default);
            if (lake == null)
            {
                return NotFound();
            }

            return Ok(lake);
        }

        /// Retrieves a list of all lakes.
        /// </summary>
        /// <param name="token">A cancellation token for handling asynchronous requests.</param>
        /// <returns>Returns 404 Not Found if no lakes are found, or 200 OK with a list of lakes if available.</returns>
        [HttpGet]
        public async Task<IActionResult> GetLakes(CancellationToken token)
        {
            var lakes = await _repository.GetAll().ToListAsync(token);
            if (lakes == null)
            {
                return NotFound();
            }

            return Ok(lakes);
        }

        /// <summary>
        /// Creates a new lake with the provided data.
        /// </summary>
        /// <param name="lake">The LakeDto object containing lake data to create.</param>
        /// <returns>Returns 400 Bad Request if the data is invalid, or 200 OK with the created lake information if successful. Returns 500 Internal Server Error if an exception occurs.</returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> CreateLake([FromBody] LakeDto lake)
        {
            if (lake == null)
            {
                return BadRequest("Invalid data");
            }

            try
            {
                var newLake = new Lakes
                {
                    Name = lake.Name,
                    Image = lake.Image,
                    Description = lake.Description
                };

                var createdLake = await _repository.Add(newLake);
                return Ok(createdLake);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error: " + ex.Message);
            }
        }

        /// <summary>
        /// Deletes a lake by its ID.
        /// </summary>
        /// <param name="id">The ID of the lake to delete.</param>
        /// <returns>Returns 204 No Content if the lake is successfully deleted. Returns 500 Internal Server Error if an exception occurs.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLake(int id)
        {
            try
            {
                await _repository.Delete(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error: " + ex.Message);
            }
        }

        /// <summary>
        /// Updates an existing lake with the provided data.
        /// </summary>
        /// <param name="id">The ID of the lake to update.</param>
        /// <param name="updatedLake">The Lakes object containing updated lake data.</param>
        /// <returns>Returns 404 Not Found if the lake does not exist, or 200 OK with the updated lake information if successful.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateLake(int id, [FromBody] Lakes updatedLake)
        {
            var existingLake = await _repository.Get(id, default);
            if (existingLake == null)
            {
                return NotFound();
            }

            existingLake.Name = updatedLake.Name;
            existingLake.Image = updatedLake.Image;
            existingLake.Description = updatedLake.Description;

            _repository.Update(existingLake);

            return Ok(existingLake);
        }

    }
}
