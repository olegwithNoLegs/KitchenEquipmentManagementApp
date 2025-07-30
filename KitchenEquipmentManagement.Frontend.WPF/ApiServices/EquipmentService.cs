using KitchenEquipmentManagement.Frontend.WPF.Helpers;
using KitchenEquipmentManagement.Frontend.WPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KitchenEquipmentManagement.Frontend.WPF.ApiServices
{
    public class EquipmentService : ApiService
    {
        public EquipmentService() 
        {
            if (!string.IsNullOrEmpty(TokenStorage.Token)) 
            {
                SetToken(TokenStorage.Token);
            }
        }

        public async Task<List<EquipmentDto>?> GetAllEquipmentAsync()
        {
            return await GetAsync<List<EquipmentDto>>("api/equipment");
        }

        public async Task<List<EquipmentDto>?> GetUnregisteredEquipmentsAsync()
        {
            return await GetAsync<List<EquipmentDto>>("api/equipment/unregistered");
        }

        public async Task<List<EquipmentDto>?> GetRegisteredEquipmentsAsync(int siteId)
        {
            return await GetAsync<List<EquipmentDto>>($"api/equipment/registered/{siteId}");
        }

        public async Task<EquipmentDto?> GetEquipmentAsync(int id)
        {
            return await GetAsync<EquipmentDto>($"api/equipment/{id}");
        }

        public async Task<string?> UpdateEquipmentAsync(EquipmentDto equipmentDto)
        {
            return await PutAsync<EquipmentDto, string>($"api/equipment/{equipmentDto.EquipmentId}", equipmentDto);
        }

        public async Task<bool> DeleteEquipmentAsync(int id)
        {
            return await DeleteAsync($"api/equipment/{id}");
        }

        public async Task<string> AddEquipmentAsync(EquipmentDto equipmentDto)
        {

            dynamic equipmentToSend = new
            {
                EquipmentId = equipmentDto.EquipmentId,
                Description = equipmentDto.Description,
                Condition = (int)equipmentDto.Condition
            };



            var response = await PostAsync<dynamic, string>("api/equipment", equipmentDto);
            return response;
        }
    }
}
