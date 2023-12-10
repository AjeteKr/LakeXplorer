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

        [HttpPost("{userId}/{sightingId}")]
        public async Task<IActionResult> CreateLikes(int userId, int sightingId)
        {
            try
            {
                Likes newLikes = new Likes { UserId = userId, SightingId = sightingId };
                var createdLikes = await _repository.Add(newLikes);
                return CreatedAtAction(nameof(GetLikes), new { id = createdLikes.Id }, createdLikes);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating likes.");
                return StatusCode(500, "Internal Server Error: " + ex.Message);
            }
        }
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetLikesForUser(int userId)
        {
            try
            {
                var likesForUser = await _repository.GetLikesForUser(userId);
                return Ok(likesForUser);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting likes for user.");
                return StatusCode(500, "Internal Server Error: " + ex.Message);
            }
        }

        //[HttpPut("update-like-status/{lakeId}")]
        //public async Task<IActionResult> UpdateLikeStatus(int lakeId, [FromBody] bool isLiked)
        //{
        //    try
        //    {
        //        var existingLike = await _repository.Get(lakeId, default);
        //        if (existingLike == null)
        //        {
        //            return NotFound("Like not found.");
        //        }
        //        existingLike.IsLiked = isLiked;
        //        _repository.Update(existingLike);
        //        return Ok(existingLike);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "Error updating like status.");
        //        return StatusCode(500, "Internal Server Error: " + ex.Message);
        //    }
        //}


    }
}
