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
    public class SiteRepository : ISiteRepository
    {
        private readonly AppDbContext _context;

        public SiteRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Site?> GetSiteAsync(int id)
        {
            return await _context.Sites.FindAsync(id);
        }

        public async Task<IEnumerable<Site>> GetAllSitesAsync()
        {
            return await _context.Sites.ToListAsync();
        }

        public async Task AddSiteAsync(Site site)
        {
            _context.Sites.Add(site);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateSiteAsync(Site site)
        {
            _context.Sites.Update(site);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteSiteAsync(int id)
        {
            var site = await _context.Sites.FindAsync(id);
            if (site != null)
            {
                _context.Sites.Remove(site);
                await _context.SaveChangesAsync();
            }
        }


        public async Task DeleteSitesByUserIdAsync(int userId)
        {
            var sites = await _context.Sites
                .Where(s => s.UserId == userId)
                .ToListAsync();

            foreach (var site in sites)
            {
                await DeleteSiteAsync(site.SiteId);
            }
        }

    }

}
