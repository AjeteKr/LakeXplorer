using LakeXplorer.Models;
using LakeXplorer.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

namespace LakeXplorer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LakeSightingsController : ControllerBase
    {
        private readonly ILogger<LakeSightingsController> _logger;
        private readonly IRepository<LakeSightings> _repository;
        private Cloudinary cloudinary;

        public LakeSightingsController(ILogger<LakeSightingsController> logger, IRepository<LakeSightings> repository)
        {
            _logger = logger;
            _repository = repository;

            Account account = new Account(
                "djiicjy1v",
                "417365291149721",
                "g32YBH42nhxvoL4654d9sqBEpKk"
            );

            cloudinary = new Cloudinary(account);
        }

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
        [HttpGet("image/{cloudinaryAssetId}")]
        public async Task<IActionResult> GetImageByCloudinaryAssetId(string cloudinaryAssetId)
        {
            try
            {
                var result = await cloudinary.GetResourceAsync(cloudinaryAssetId);

                if (result != null)
                {
                    return Ok(result);
                }
                else
                {
                    return NotFound("Image not found.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting image by Cloudinary Asset ID.");
                return StatusCode(500, "Internal Server Error: " + ex.Message);
            }
        }


        [HttpPost]
        public async Task<IActionResult> CreateLakeSighting([FromBody] LakeSightings newSighting)
        {
            if (newSighting == null || !ModelState.IsValid)
            {
                return BadRequest("Invalid data.");
            }

            try
            {
                var uploadParams = new ImageUploadParams()
                {
                    Folder = "newSighting.Image"
                };

                var uploadResult = await cloudinary.UploadAsync(uploadParams);
                newSighting.CloudinaryAssetId = uploadResult.PublicId;

                var createdSighting = await _repository.Add(newSighting);
                return CreatedAtAction(nameof(GetLakeSighting), new { id = createdSighting.Id }, createdSighting);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating lake sighting.");
                return StatusCode(500, "Internal Server Error: " + ex.Message);
            }
        }

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
