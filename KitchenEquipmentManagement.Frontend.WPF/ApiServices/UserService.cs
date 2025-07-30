using KitchenEquipmentManagement.Frontend.WPF.Helpers;
using KitchenEquipmentManagement.Frontend.WPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KitchenEquipmentManagement.Frontend.WPF.ApiServices
{



    public class UserService : ApiService
    {
        public UserService()
        {
            if (!string.IsNullOrEmpty(TokenStorage.Token))
            {
                SetToken(TokenStorage.Token);
            }
        }

        public async Task<List<UserDto>?> GetAllUsersAsync()
        {
            return await GetAsync<List<UserDto>>("api/user");
        }

        public async Task<UserDto?> GetUserByIdAsync(int id)
        {
            return await GetAsync<UserDto>($"api/user/{id}");
        }

        public async Task<string?> UpdateUserAsync(UserDto user)
        {
            return await PutAsync<UserDto, string>($"api/user/{user.UserId}", user);
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            return await DeleteAsync($"api/user/{id}");
        }
    }

}
