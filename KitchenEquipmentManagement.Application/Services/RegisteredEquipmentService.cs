using KitchenEquipmentManagement.Application.DTOs;
using KitchenEquipmentManagement.Application.Interfaces.Persistence;
using KitchenEquipmentManagement.Domain.Models;

namespace KitchenEquipmentManagement.Application.Services
{
    public class RegisteredEquipmentService : IRegisteredEquipmentService
    {
        public readonly IRegisteredEquipmentRepository _registeredEquipmentRepo;
        public readonly IEquipmentService _equipmentService;

        public RegisteredEquipmentService(IRegisteredEquipmentRepository registeredEquipmentRepo, IEquipmentService equipmentService)
        {
            _registeredEquipmentRepo = registeredEquipmentRepo;
            _equipmentService = equipmentService;
        }

        public async Task<bool> AddRegisteredEquipment(RegisteredEquipment regeq)
        {
            try
            {
                var equipment = await _equipmentService.GetEquipmentAsync(regeq.EquipmentId);
                if (equipment == null) return false;

                equipment.SiteId = regeq.SiteId;
                await _registeredEquipmentRepo.AddRegistrationAsync(regeq);
                await _equipmentService.UpdateEquipmentAsync(equipment);
                return true;
            }
            catch 
            {
                return false;
            }
        }

        public async Task<bool> UnregisterEquipmentAsync(int equipmentId)
        {
            try
            {
                var equipment = await _equipmentService.GetEquipmentAsync(equipmentId);
                if (equipment == null) return false;

                await _registeredEquipmentRepo.DeleteByEquipmentAsync(equipmentId);

                equipment.SiteId = null;
                await _equipmentService.UpdateEquipmentAsync(equipment);

                return true;
            }
            catch
            {
                return false;
            }
        }

    }
}
