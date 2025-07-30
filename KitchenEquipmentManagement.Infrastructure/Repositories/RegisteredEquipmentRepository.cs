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
    public class RegisteredEquipmentRepository : IRegisteredEquipmentRepository
    {
        private readonly AppDbContext _context;

        public RegisteredEquipmentRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<RegisteredEquipment>> GetBySiteIdAsync(int siteId)
        {
            return await _context.RegisteredEquipments
                .Where(r => r.SiteId == siteId)
                .ToListAsync();
        }

        public async Task<IEnumerable<RegisteredEquipment>> GetByEquipmentIdAsync(int equipmentId)
        {
            return await _context.RegisteredEquipments
                .Where(r => r.EquipmentId == equipmentId)
                .ToListAsync();
        }

        public async Task AddRegistrationAsync(RegisteredEquipment registration)
        {
            _context.RegisteredEquipments.Add(registration);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteBySiteIdAsync(int siteId)
        {
            var registrations = await _context.RegisteredEquipments
                .Where(r => r.SiteId == siteId)
                .ToListAsync();

            _context.RegisteredEquipments.RemoveRange(registrations);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteByEquipmentAsync(int equipmentId)
        {
            var registrations = await _context.RegisteredEquipments
                .Where(r => r.EquipmentId == equipmentId)
                .ToListAsync();

            _context.RegisteredEquipments.RemoveRange(registrations);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<RegisteredEquipment>> GetAllRegisteredEquipmentsAsync()
        {
            return await _context.RegisteredEquipments
            .ToListAsync();
        }
    }

}
