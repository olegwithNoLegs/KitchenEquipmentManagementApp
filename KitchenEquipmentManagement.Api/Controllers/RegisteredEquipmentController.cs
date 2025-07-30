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
    [Authorize]
    public class RegisteredEquipmentController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IRegisteredEquipmentService _service;

        public RegisteredEquipmentController(IMapper mapper, IRegisteredEquipmentService service)
        {
            _mapper = mapper;
            _service = service;
        }

        // POST: api/RegisterEquipment/register
        [HttpPost("register")]
        public async Task<IActionResult> AddRegisteredEquipment([FromBody] RegisterEquipmentRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var registeredEquipment = _mapper.Map<RegisteredEquipment>(request);
            var result = await _service.AddRegisteredEquipment(registeredEquipment);

            if (!result)
                return BadRequest("Registering equipment failed.");

            return Ok("Equipment registered successfully.");
        }

        // DELETE: api/RegisterEquipment/unregister/{equipmentId}
        [HttpDelete("unregister/{equipmentId}")]
        public async Task<IActionResult> UnregisterEquipment(int equipmentId)
        {
            var result = await _service.UnregisterEquipmentAsync(equipmentId);

            if (!result)
                return BadRequest("Unregistering equipment failed.");

            return Ok("Equipment unregistered successfully.");
        }


    }
}
