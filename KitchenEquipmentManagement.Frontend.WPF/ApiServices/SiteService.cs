using KitchenEquipmentManagement.Frontend.WPF.Helpers;
using KitchenEquipmentManagement.Frontend.WPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KitchenEquipmentManagement.Frontend.WPF.ApiServices
{
    public class SiteService : ApiService
    {
        public SiteService() 
        {
            if (!string.IsNullOrEmpty(TokenStorage.Token))
            {
                SetToken(TokenStorage.Token);
            }
        }

        public async Task<string> AddSiteAsync(SiteDto siteDto)
        {

            dynamic equipmentToSend = new
            {
                Description = siteDto.Description
            };



            var response = await PostAsync<dynamic, string>("api/site", siteDto);
            return response;
        }

        public async Task<List<SiteDto>?> GetAllSitesAsync()
        {
            return await GetAsync<List<SiteDto>>("api/site");
        }

        public async Task<SiteDto?> GetSiteByIdAsync(int id)
        {
            return await GetAsync<SiteDto>($"api/site/{id}");
        }

        public async Task<string?> UpdateSiteAsync(SiteDto site)
        {
            return await PutAsync<SiteDto, string>($"api/site/{site.SiteId}", site);
        }

        public async Task<bool> DeleteSiteAsync(int id)
        {
            return await DeleteAsync($"api/site/{id}");
        }
    }
}
