using KitchenEquipmentManagement.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KitchenEquipmentManagement.Application.Interfaces.Persistence
{
    public interface IEquipmentRepository
    {
        Task<Equipment?> GetEquipmentAsync(int id);
        Task<IEnumerable<Equipment>> GetAllEquipmentAsync();
        Task AddEquipmentAsync(Equipment equipment);
        Task UpdateEquipmentAsync(Equipment equipment);
        Task DeleteEquipmentAsync(int id);
    }
}
