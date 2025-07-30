using AutoMapper;
using KitchenEquipmentManagement.Application.DTOs;
using KitchenEquipmentManagement.Application.Interfaces.Persistence;
using KitchenEquipmentManagement.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KitchenEquipmentManagement.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Policy = "SuperAdminOnly")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var userId = int.Parse(User.FindFirst("id").Value);
            var users = await _userService.GetAllUsersAsync(userId);
            return Ok(users);
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetById(int userId)
        {
            var user = await _userService.GetUserByIdAsync(userId);
            return user == null ? NotFound() : Ok(user);
        }


        [HttpPut("{userId}")]
        public async Task<IActionResult> Update(int userId, [FromBody] UserDto userDto)

        {
            var existingUser = await _userService.GetUserByIdAsync(userId);
            if (existingUser == null)
            {
                return NotFound("User not found");
            }

            var updatedUser = _mapper.Map<User>(userDto);

            updatedUser.Password = existingUser.Password;

            updatedUser.UserId = userId;

            var result = await _userService.UpdateUserAsync(updatedUser);
            return result ? Ok("User updated") : NotFound("User not found");
        }

        [HttpDelete("{userId}")]
        public async Task<IActionResult> Delete(int userId)
        {
            bool result = await _userService.DeleteUserAsync(userId);
            return result ? Ok("User deleted") : NotFound("User not found");
        }
    }
}
