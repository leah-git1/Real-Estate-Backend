using DTOs;
using Entities;

namespace Repository
{
    public interface IUsersRepository
    {
        Task deleteUser(int id);
        Task<IEnumerable<User>> getAllUsers();
        Task<User> getUserById(int id);
        Task<User> loginUser(UserLoginDTO userToLog);
        Task<User> registerUser(User user);
        Task<User> updateUser(User userToUpdate, int id);
    }
}