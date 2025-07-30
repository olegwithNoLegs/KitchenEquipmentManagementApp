using KitchenEquipmentManagement.Application.Interfaces.Persistence;
using KitchenEquipmentManagement.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KitchenEquipmentManagement.Application.Services
{
    public class SiteService : ISiteService
    {
        private readonly ISiteRepository _siteRepo;
        private readonly IEquipmentService _equipmentService;
        private readonly IRegisteredEquipmentRepository _regRepo;

        public SiteService(ISiteRepository siteRepo, IEquipmentService equipmentService, IRegisteredEquipmentRepository regRepo)
        {
            _siteRepo = siteRepo;
            _equipmentService = equipmentService;
            _regRepo = regRepo;
        }

        public async Task<IEnumerable<Site>> GetAllSitesByUserIdAsync(int UserId) 
        {
            var sites = await _siteRepo.GetAllSitesAsync();

            return sites.Where(s => s.UserId == UserId).ToList();
        }
        public Task<Site?> GetSiteAsync(int id) => _siteRepo.GetSiteAsync(id);

        public async Task<bool> AddSiteAsync(Site site)
        {
            try 
            {
                site.Active = true;
                await _siteRepo.AddSiteAsync(site);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteSiteAsync(int id)
        {
            try
            {
                var equipments = await _equipmentService.GetAllEquipmentsBySiteIdAsync(id);


                foreach (var equipment in equipments)
                {
                    equipment.SiteId = null;
                    await _equipmentService.UpdateEquipmentAsync(equipment);
                }


                await _regRepo.DeleteBySiteIdAsync(id);
                await _siteRepo.DeleteSiteAsync(id);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> UpdateSiteAsync(Site site)
        {
            try
            {
                await _siteRepo.UpdateSiteAsync(site);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
