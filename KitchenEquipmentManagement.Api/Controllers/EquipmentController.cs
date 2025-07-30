using AutoMapper;
using KitchenEquipmentManagement.Application.DTOs;
using KitchenEquipmentManagement.Application.Interfaces.Persistence;
using KitchenEquipmentManagement.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KitchenEquipmentManagement.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class EquipmentController : ControllerBase
    {
        private readonly IEquipmentService _equipmentService;
        private readonly IMapper _mapper;

        public EquipmentController(IEquipmentService equipmentService, IMapper mapper)
        {
            _equipmentService = equipmentService;
            _mapper = mapper;
        }

        // GET: api/equipment
        [HttpGet]
        public async Task<IActionResult> GetAllEquipment()
        {
            var userId = int.Parse(User.FindFirst("id").Value);
            var equipmentList = await _equipmentService.GetAllEquipmentByUserIdAsync(userId);
            var equipmentDtos = _mapper.Map<IEnumerable<EquipmentDto>>(equipmentList);
            return Ok(equipmentDtos);
        }


        // GET: api/equipment/unregistered
        [HttpGet("unregistered")]
        public async Task<IActionResult> GetUnregisteredEquipments()
        {
            var userId = int.Parse(User.FindFirst("id").Value);
            var unregisteredEquipments = await _equipmentService.GetUnregisteredEquipmentsAsync(userId);
            var equipmentDtos = _mapper.Map<IEnumerable<EquipmentDto>>(unregisteredEquipments);
            return Ok(equipmentDtos);
        }

        // GET: api/equipment/registered
        [HttpGet("registered/{siteId}")]
        public async Task<IActionResult> GetRegisteredEquipments(int siteId)
        {
            var userId = int.Parse(User.FindFirst("id").Value);
            var unregisteredEquipments = await _equipmentService.GetRegisteredEquipmentsAsync(userId, siteId);
            var equipmentDtos = _mapper.Map<IEnumerable<EquipmentDto>>(unregisteredEquipments);
            return Ok(equipmentDtos);
        }


        // GET: api/equipment/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEquipment(int id)
        {
            var equipment = await _equipmentService.GetEquipmentAsync(id);
            if (equipment == null)
                return NotFound();

            var equipmentDto = _mapper.Map<EquipmentDto>(equipment);
            return Ok(equipmentDto);
        }

        // POST: api/equipment
        [HttpPost]
        public async Task<IActionResult> AddEquipment([FromBody] EquipmentRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var userId = int.Parse(User.FindFirst("id").Value);
            var equipment = _mapper.Map<Equipment>(request);
            equipment.UserId = userId;
            var result = await _equipmentService.AddEquipmentAsync(equipment);

            
            if(!result)
                return BadRequest("Adding Equipment failed");

            return Ok($"Equipment {equipment.Description} created successfully.");
        }

        // PUT: api/equipment/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEquipment(int id, [FromBody] EquipmentRequest equipmentDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingEquipment = await _equipmentService.GetEquipmentAsync(id);
            if (existingEquipment == null)
                return NotFound();

            var updatedEquipment = _mapper.Map(equipmentDto, existingEquipment);
            await _equipmentService.UpdateEquipmentAsync(updatedEquipment);

            return Ok($"Equipment {updatedEquipment.Description} is updated.");
        }

        // DELETE: api/equipment/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEquipment(int id)
        {
            var existingEquipment = await _equipmentService.GetEquipmentAsync(id);
            if (existingEquipment == null)
                return NotFound();

            await _equipmentService.DeleteEquipmentAsync(id);
            return Ok($"Equipment {existingEquipment.Description} is deleted.");
        }
    }
}
