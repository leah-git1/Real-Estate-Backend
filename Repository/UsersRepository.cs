using Entities;
using Microsoft.EntityFrameworkCore;
using DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository
{
    public class UsersRepository : IUsersRepository
    {
        private readonly ShopContext _ShopContext;

        public UsersRepository(ShopContext shopContext)
        {
            this._ShopContext = shopContext;
        }

        public async Task<List<User>> GetAllUsers()
        {
            return await _ShopContext.Users.ToListAsync();
        }

        public async Task<User> GetUserById(int id)
        {
            return await _ShopContext.Users.FirstOrDefaultAsync(x => x.UserId == id);
        }

        public async Task<User> RegisterUser(User user)
        {
            await _ShopContext.Users.AddAsync(user);
            await _ShopContext.SaveChangesAsync();
            return user;
        }

        public async Task<User> LoginUser(UserLoginDTO userToLog)
        {
            return await _ShopContext.Users.FirstOrDefaultAsync(x =>
                x.Email == userToLog.Email && x.Password == userToLog.Password);
        }

        public async Task<User> UpdateUser(User userToUpdate, int id)
        {
            User existingUser = await _ShopContext.Users
                .FirstOrDefaultAsync(u => u.UserId == id);

            if (existingUser == null)
                return null;

            if (!string.IsNullOrWhiteSpace(userToUpdate.FullName))
                existingUser.FullName = userToUpdate.FullName;

            if (!string.IsNullOrWhiteSpace(userToUpdate.Email))
                existingUser.Email = userToUpdate.Email;

            if (!string.IsNullOrWhiteSpace(userToUpdate.Password))
                existingUser.Password = userToUpdate.Password;

            if (!string.IsNullOrWhiteSpace(userToUpdate.Phone))
                existingUser.Phone = userToUpdate.Phone;

            if (!string.IsNullOrWhiteSpace(userToUpdate.Address))
                existingUser.Address = userToUpdate.Address;

            await _ShopContext.SaveChangesAsync();
            return existingUser;
        }


        public async Task DeleteUser(int id)
        {
            var user = await _ShopContext.Users.FindAsync(id);
            if (user != null)
            {
                _ShopContext.Users.Remove(user);
                await _ShopContext.SaveChangesAsync();
            }
        }
    }
}