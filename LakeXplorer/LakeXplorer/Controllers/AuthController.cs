using LakeXplorer.DTOs;
using LakeXplorer.Models;
using LakeXplorer.Repository;
using LakeXplorer.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LakeXplorer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly TokenService _tokenService;
        private readonly IRepository<Users> _repository;

        public AuthController(TokenService tokenService, IRepository<Users> repository)
        {
            _tokenService = tokenService;
            _repository = repository;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserDto user) 
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (await _repository.GetAll().AnyAsync(f => f.Username == user.Username || f.Email == user.Email))
            {
                return BadRequest("User already exists.");
            }
            var usr = new Users
            {
                Email = user.Email,
                Password = user.Password,
                Username = user.Username
            };

            await _repository.Add(usr);

            return Ok("User registered successfully.");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto user) 
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (await _repository.GetAll().AnyAsync(f => f.Username == user.Username && f.Password == user.Password))
            {
                var token = _tokenService.GenerateToken(user.Username);
                return Ok(new { Token = token });
            }

            return BadRequest("Username or password invalid.");
        }
    }
}
