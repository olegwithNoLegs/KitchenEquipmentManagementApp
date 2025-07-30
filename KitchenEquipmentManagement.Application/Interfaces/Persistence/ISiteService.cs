using KitchenEquipmentManagement.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KitchenEquipmentManagement.Application.Interfaces.Persistence
{
    public interface ISiteService
    {
        Task<IEnumerable<Site>> GetAllSitesByUserIdAsync(int UserId);
        Task<Site?> GetSiteAsync(int id);
        Task<bool> AddSiteAsync(Site site);
        Task<bool> DeleteSiteAsync(int id);
        Task<bool> UpdateSiteAsync(Site site);
    }
}
