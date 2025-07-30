using AutoMapper;
using KitchenEquipmentManagement.Application.DTOs;
using KitchenEquipmentManagement.Application.Interfaces;
using KitchenEquipmentManagement.Application.Interfaces.Persistence;
using KitchenEquipmentManagement.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KitchenEquipmentManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;

        public AuthenticationController(IUserService userService, IMapper mapper, ITokenService tokenService) 
        {
            _userService = userService;
            _mapper = mapper;
            _tokenService = tokenService;
        }


        [HttpPost("signup")]
        public async Task<IActionResult> Signup([FromBody] SignupRequest request)
        {
            if (request == null)
                return BadRequest("Invalid request payload.");

            var user = _mapper.Map<User>(request);

            var result = await _userService.AddUserAsync(user);

            if (!result)
                return BadRequest("User already exists.");

            return Ok("User created successfully.");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var response = await _userService.AuthenticateAsync(request);
            if (string.IsNullOrEmpty(response.Token))
                return Unauthorized("Invalid username or password.");

            return Ok(response);
        }

        [Authorize]
        [HttpPost("logout")]
        public IActionResult Logout()
        {
            var jti = User.FindFirst("jti")?.Value;
            if (!string.IsNullOrEmpty(jti))
            {
                _tokenService.RevokeToken(jti);
            }

            return Ok("Logged out successfully.");
        }
    }
}
