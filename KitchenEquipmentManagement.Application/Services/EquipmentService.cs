using KitchenEquipmentManagement.Application.Interfaces.Persistence;
using KitchenEquipmentManagement.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KitchenEquipmentManagement.Application.Services
{
    public class EquipmentService : IEquipmentService
    {
        private readonly IEquipmentRepository _equipmentRepo;
        private readonly IRegisteredEquipmentRepository _registeredEquipmentRepo;
        public EquipmentService(IEquipmentRepository equipmentRepo, IRegisteredEquipmentRepository registeredEquipmentRepository) 
        {
            _equipmentRepo = equipmentRepo;
            _registeredEquipmentRepo = registeredEquipmentRepository;
        }
        public async Task<bool> AddEquipmentAsync(Equipment equipment)
        {
            try
            {
                // Generate a random serial number
                equipment.SerialNumber = GenerateSerialNumber();

                await _equipmentRepo.AddEquipmentAsync(equipment);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private string GenerateSerialNumber()
        {
            var random = new Random();
            var prefix = "EQP"; 
            var uniquePart = Guid.NewGuid().ToString("N").Substring(0, 8).ToUpper(); 
            var randomDigits = random.Next(1000, 9999); 

            return $"{prefix}-{uniquePart}-{randomDigits}";
        }


        public async Task DeleteEquipmentAsync(int id)
        {
            var registeredEquipments = await _registeredEquipmentRepo.GetByEquipmentIdAsync(id);
            if (registeredEquipments?.Count() > 0)
                await _registeredEquipmentRepo.DeleteByEquipmentAsync(id);

            await _equipmentRepo.DeleteEquipmentAsync(id);

        }

        public async Task<IEnumerable<Equipment>> GetAllEquipmentByUserIdAsync(int userId)
        {
            var equipments = await _equipmentRepo.GetAllEquipmentAsync();

            return equipments.Where(e => e.UserId == userId).ToList();
        }

        public async Task<IEnumerable<Equipment>> GetAllEquipmentsBySiteIdAsync(int siteId)
        {
            var equipments = await _equipmentRepo.GetAllEquipmentAsync();

            return equipments.Where(e => e.SiteId == siteId).ToList();
        }

        public Task<Equipment?> GetEquipmentAsync(int id) => _equipmentRepo.GetEquipmentAsync(id);

        public Task UpdateEquipmentAsync(Equipment equipment)
        {
            return _equipmentRepo.UpdateEquipmentAsync(equipment);
        }

        public async Task<IEnumerable<Equipment>> GetUnregisteredEquipmentsAsync(int userId) 
        {
            var allEquipments = await _equipmentRepo.GetAllEquipmentAsync();
            var registeredEquipments = await _registeredEquipmentRepo.GetAllRegisteredEquipmentsAsync();

            var registeredIds = registeredEquipments.Select(re => re.EquipmentId).ToHashSet();

            var unregisteredEquipments = allEquipments
                .Where(e => e.UserId == userId && !registeredIds.Contains(e.EquipmentId))
                .ToList();

            return unregisteredEquipments;
        }

        public async Task<IEnumerable<Equipment>> GetRegisteredEquipmentsAsync(int userId, int siteId)
        {
            var allEquipments = await _equipmentRepo.GetAllEquipmentAsync();
            var registeredEquipments = await _registeredEquipmentRepo.GetAllRegisteredEquipmentsAsync();

            var registeredIds = registeredEquipments.Select(re => re.EquipmentId).ToHashSet();

            var result = allEquipments
                .Where(e => e.UserId == userId &&
                            e.SiteId == siteId &&
                            registeredIds.Contains(e.EquipmentId))
                .ToList();

            return result;
        }

    }
}
