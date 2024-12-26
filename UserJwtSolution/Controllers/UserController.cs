using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserJwtSolution.Models;
using UserJwtSolution.Services;

namespace UserJwtSolution.Controllers
{  
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly TokenService _tokenService;

        public UserController(UserService userService, TokenService tokenService) 
        {
            _userService = userService;
            _tokenService = tokenService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserDto userDto)
        {
            var user = new User
            {
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                Email = userDto.Email,
                Password = userDto.Password
            };

            var result = await _userService.RegisterUser(user);
            if (!result) return BadRequest("Email already exists.");

            return Ok("User registered successfully.");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var user = await _userService.AuthenticateUser(loginDto.Email, loginDto.Password);
            if (user == null) return Unauthorized("Invalid email or password.");

            var token = _tokenService.GenerateToken(user);

            return Ok(new { Token = token });
        }

        [Authorize]
        [HttpGet("users")]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userService.GetAllUsers();
            if (users == null || users.Count == 0)
                return NotFound("No users found.");

            return Ok(users);
        }

        [Authorize]
        [HttpGet("users/{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _userService.GetUserById(id);
            if (user == null)
                return NotFound($"No user found with ID {id}.");

            return Ok(user);
        }
    }
}
