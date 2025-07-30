using KitchenEquipmentManagement.Application.DTOs;
using KitchenEquipmentManagement.Domain.Models;


namespace KitchenEquipmentManagement.Application.Interfaces.Persistence
{
    public interface IRegisteredEquipmentService
    {
        Task<bool> AddRegisteredEquipment(RegisteredEquipment regeq);
        Task<bool> UnregisterEquipmentAsync(int equipmentId);
    }
}
