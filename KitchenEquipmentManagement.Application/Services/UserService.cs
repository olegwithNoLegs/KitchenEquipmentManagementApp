using KitchenEquipmentManagement.Application.DTOs;
using KitchenEquipmentManagement.Application.Interfaces;
using KitchenEquipmentManagement.Application.Interfaces.Persistence;
using KitchenEquipmentManagement.Domain.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KitchenEquipmentManagement.Application.Services
{

    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepo;
        private readonly ISiteService _siteService;
        private readonly IEquipmentService _equipService;
        private readonly IJwtTokenGenerator _jwt;
        private readonly IPasswordHasher<User> _passwordHasher;

        public UserService(IUserRepository userRepo, IEquipmentService equipService, ISiteService siteService, IJwtTokenGenerator jwt)
        {
            _userRepo = userRepo;
            _equipService = equipService;
            _siteService = siteService;
            _jwt = jwt;
            _passwordHasher = new PasswordHasher<User>();
        }

        public async Task<IEnumerable<UserDto>> GetAllUsersAsync(int userId)
        {
            var users = await _userRepo.GetAllUsersAsync();

            return users
                .Where(u => u.UserId != userId)
                .Select(u => new UserDto
                {
                    UserId = u.UserId,
                    UserName = u.UserName,
                    EmailAddress = u.EmailAddress,
                    UserType = u.UserType,
                    FullName = u.FirstName + " " + u.LastName,
                })
                .ToList();
        }

        public async Task<bool> AddUserAsync(User user)
        {
            try
            {
                if (await _userRepo.CheckIfUserExists(user.UserName))
                    return false;

                user.Password = _passwordHasher.HashPassword(user, user.Password);

                await _userRepo.AddUserAsync(user);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> UpdateUserAsync(User user)
        {
            try
            {
                await _userRepo.UpdateUserAsync(user);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            try
            {
                var equipments = await _equipService.GetAllEquipmentByUserIdAsync(id);

                foreach (var equipment in equipments) 
                {
                    await _equipService.DeleteEquipmentAsync(equipment.EquipmentId);
                }
                var sites = await _siteService.GetAllSitesByUserIdAsync(id);

                foreach (var site in sites) 
                {
                    await _siteService.DeleteSiteAsync(site.SiteId);
                }

                await _userRepo.DeleteUserAsync(id);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<LoginResponse> AuthenticateAsync(LoginRequest request)
        {
            var user = await _userRepo.GetUserByUserNameAsync(request.UserName);
            return user != null &&
                   _passwordHasher.VerifyHashedPassword(user, user.Password, request.Password) == PasswordVerificationResult.Success
                ? new LoginResponse() { Token = _jwt.GenerateToken(user.UserId.ToString(), user.UserName, user.UserType.ToString()), UserType = user.UserType.ToString() }
                : new LoginResponse() { Token = null, UserType = null };
        }

        public async Task<User?> GetUserByIdAsync(int userId)
        {
            return await _userRepo.GetUserByIdAsync(userId);
        }
    }

}
