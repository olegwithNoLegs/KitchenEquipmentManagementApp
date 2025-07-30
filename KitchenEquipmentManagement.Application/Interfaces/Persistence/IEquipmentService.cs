using KitchenEquipmentManagement.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KitchenEquipmentManagement.Application.Interfaces.Persistence
{
    public interface IEquipmentService
    {
        Task<Equipment?> GetEquipmentAsync(int id);
        Task<IEnumerable<Equipment>> GetAllEquipmentByUserIdAsync(int userId);
        Task<IEnumerable<Equipment>> GetAllEquipmentsBySiteIdAsync(int siteId);
        Task<bool> AddEquipmentAsync(Equipment equipment);
        Task UpdateEquipmentAsync(Equipment equipment);
        Task DeleteEquipmentAsync(int id);
        Task<IEnumerable<Equipment>> GetUnregisteredEquipmentsAsync(int userId);
        Task<IEnumerable<Equipment>> GetRegisteredEquipmentsAsync(int userId, int siteId);
    }
}
