using KitchenEquipmentManagement.Application.Interfaces.Persistence;
using KitchenEquipmentManagement.Domain.Models;
using KitchenEquipmentManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KitchenEquipmentManagement.Infrastructure.Repositories
{
    public class EquipmentRepository : IEquipmentRepository
    {
        private readonly AppDbContext _context;

        public EquipmentRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Equipment?> GetEquipmentAsync(int Id)
        {
            return await _context.Equipments.FindAsync(Id);
        }

        public async Task<IEnumerable<Equipment>> GetAllEquipmentAsync()
        {
            return await _context.Equipments.ToListAsync();
        }

        public async Task AddEquipmentAsync(Equipment equipment)
        {
            _context.Equipments.Add(equipment);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateEquipmentAsync(Equipment equipment)
        {
            _context.Equipments.Update(equipment);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteEquipmentAsync(int id)
        {
            var equipment = await _context.Equipments.FindAsync(id);
            if (equipment != null)
            {
                _context.Equipments.Remove(equipment);
                await _context.SaveChangesAsync();
            }
        }
    }

}
