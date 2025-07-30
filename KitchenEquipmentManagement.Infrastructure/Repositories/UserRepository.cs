using Azure.Core;
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
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<User?> GetUserByUserNameAsync(string username)
        {
            return await _context.Users.SingleOrDefaultAsync(u => u.UserName == username);
        }

        public async Task<User?> GetUserByIdAsync(int userId)
        {
            return await _context.Users.SingleOrDefaultAsync(u => u.UserId == userId);
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task AddUserAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateUserAsync(User user)
        {
            var existingUser = await _context.Users.FindAsync(user.UserId);
            if (existingUser == null)
                throw new Exception("User not found");

            existingUser.FirstName = user.FirstName;
            existingUser.LastName = user.LastName;
            existingUser.EmailAddress = user.EmailAddress;
            existingUser.UserName = user.UserName;
            existingUser.UserType = user.UserType;

            if (!string.IsNullOrWhiteSpace(user.Password))
            {
                existingUser.Password = user.Password;
            }

            await _context.SaveChangesAsync();
        }


        public async Task DeleteUserAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> CheckIfUserExists(string username) 
        {
            return await _context.Users.AnyAsync(u => u.UserName == username);
        }
    }

}
