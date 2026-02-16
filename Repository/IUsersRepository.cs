using DTOs;
using Entities;

namespace Repository
{
    public interface IUsersRepository
    {
        Task DeleteUser(int id);
        Task<IEnumerable<User>> GetAllUsers();
        Task<User> GetUserById(int id);
        Task<User> LoginUser(UserLoginDTO userToLog);
        Task<User> RegisterUser(User user);
        Task<User> UpdateUser(User userToUpdate, int id);
    }
}