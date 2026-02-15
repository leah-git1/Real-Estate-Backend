using Entities;
using Microsoft.EntityFrameworkCore;
using DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;
using Entities;
using Repository;

namespace Repository
{
    public class UsersRepository : IUsersRepository
    {
        ShopContext _ShopContext;

        public UsersRepository(ShopContext shopContext)
        {
            this._ShopContext = shopContext;
        }

        public async Task<IEnumerable<User>> getAllUsers()
        {
            return await _ShopContext.Users.ToListAsync();
        }

        public async Task<User> getUserById(int id)
        {
            return await _ShopContext.Users.FirstOrDefaultAsync(x => x.UserId == id);
        }

        public async Task<User> registerUser(User user)
        {
            await _ShopContext.Users.AddAsync(user);
            await _ShopContext.SaveChangesAsync();
            return user;
        }

        public async Task<User> loginUser(UserLoginDTO userToLog)
        {
            return await _ShopContext.Users.FirstOrDefaultAsync(x =>
                x.Email == userToLog.Email && x.Password == userToLog.Password);
        }

        public async Task<User> updateUser(User userToUpdate, int id)
        {
            _ShopContext.Users.Update(userToUpdate);
            await _ShopContext.SaveChangesAsync();
            return userToUpdate;
        }

        public async Task deleteUser(int id)
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