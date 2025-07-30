using KitchenEquipmentManagement.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KitchenEquipmentManagement.Application.Interfaces.Persistence
{
    public interface ISiteRepository
    {
        Task<Site?> GetSiteAsync(int id);
        Task<IEnumerable<Site>> GetAllSitesAsync();
        Task AddSiteAsync(Site site);
        Task UpdateSiteAsync(Site site);
        Task DeleteSiteAsync(int id);
    }
}
