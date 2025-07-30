using KitchenEquipmentManagement.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KitchenEquipmentManagement.Application.Interfaces.Persistence
{
    public interface IRegisteredEquipmentRepository
    {
        Task<IEnumerable<RegisteredEquipment>> GetBySiteIdAsync(int siteId);
        Task AddRegistrationAsync(RegisteredEquipment registration);
        Task DeleteBySiteIdAsync(int siteId);
        Task DeleteByEquipmentAsync(int equipmentId);
        Task<IEnumerable<RegisteredEquipment>> GetByEquipmentIdAsync(int equipmentId);
        Task<IEnumerable<RegisteredEquipment>> GetAllRegisteredEquipmentsAsync();
    }
}
