using KitchenEquipmentManagement.Application.DTOs;
using KitchenEquipmentManagement.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KitchenEquipmentManagement.Application.Interfaces.Persistence
{

    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetAllUsersAsync(int userId);
        Task<bool> AddUserAsync(User user);
        Task<bool> UpdateUserAsync(User user);
        Task<bool> DeleteUserAsync(int id);
        Task<LoginResponse> AuthenticateAsync(LoginRequest request);
        Task<User?> GetUserByIdAsync(int userId);
    }

}
