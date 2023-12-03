using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using LakeXplorer.DTOs;
using LakeXplorer.Models;
using LakeXplorer.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace LakeXplorer.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/[controller]")]
    public class LakesController : ControllerBase
    {
        private readonly ILogger<LakesController> _logger;
        private readonly IRepository<Lakes> _repository;
        private Cloudinary cloudinary;

       


        public LakesController(ILogger<LakesController> logger, IRepository<Lakes> repository)
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
        public async Task<IActionResult> GetLake(int id)
        {
            var lake = await _repository.Get(id, default);
            if (lake == null)
            {
                return NotFound();
            }

            return Ok(lake);
        }

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
                    CloudinaryAssetId = lake.CloudinaryAssetId,
                    Description = lake.Description,
                    ImageUrl = $"https://res.cloudinary.com/djiicjy1v/image/upload/v1701027229/Lakes/{lake.CloudinaryAssetId}.jpg\")\""
                };
                var createdLake = await _repository.Add(newLake);
                _logger.LogInformation("New lake added to database successfully.");
                return Ok(createdLake);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating the lake.");
                return StatusCode(500, "Internal Server Error: " + ex.Message);
            }
        }



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

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateLake(int id, [FromBody] Lakes updatedLake)
        {
            var existingLake = await _repository.Get(id, default);
            if (existingLake == null)
            {
                return NotFound();
            }

            existingLake.Name = updatedLake.Name;
            existingLake.Description = updatedLake.Description;

            _repository.Update(existingLake);

            return Ok(existingLake);
        }


    }
}
