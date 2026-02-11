using DTOs;

namespace Services
{
    public interface IUsersServices
    {
        Task deleteUser(int id);
        Task<IEnumerable<UserProfileDTO>> getAllUsers();
        Task<UserProfileDTO> getUserById(int id);
        Task<UserProfileDTO> loginUser(UserLoginDTO userToLog);
        Task<UserProfileDTO> registerUser(UserRegisterDTO userToRegister);
        Task<UserProfileDTO> updateUser(UserRegisterDTO userToUpdate, int id);
    }
}