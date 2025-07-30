using KitchenEquipmentManagement.Frontend.WPF.Helpers;
using KitchenEquipmentManagement.Frontend.WPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KitchenEquipmentManagement.Frontend.WPF.ApiServices
{
    public class RegisteredEquipmentService : ApiService
    {
        public RegisteredEquipmentService() 
        {
            if (!string.IsNullOrEmpty(TokenStorage.Token))
            {
                SetToken(TokenStorage.Token);
            }
        }

        public async Task<string> RegisterEquipment(RegisterEquipmentRequest request)
        {
            var response = await PostAsync<dynamic, string>("api/RegisteredEquipment/register", request);
            return response;
        }

        public async Task<bool> UnregisterEquipment(int equipmentId)
        {
            var response = await DeleteAsync($"api/RegisteredEquipment/unregister/{equipmentId}");
            return response;
        }
    }
}
