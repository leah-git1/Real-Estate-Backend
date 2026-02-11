using AutoMapper;
using DTOs;
using Entities;
using Repository;

namespace Services
{
    public class UsersServices : IUsersServices
    {
        IUsersRepository _iUsersRepository;
        IPasswordService _iPasswordService;
        IMapper _mapper;

        public UsersServices(IUsersRepository iusersRepository, IPasswordService passwordService, IMapper mapper)
        {
            this._iUsersRepository = iusersRepository;
            this._iPasswordService = passwordService;
            this._mapper = mapper;
        }

        public async Task<IEnumerable<UserProfileDTO>> getAllUsers()
        {
            IEnumerable<User> users = await _iUsersRepository.getAllUsers();
            return _mapper.Map<IEnumerable<User>, IEnumerable<UserProfileDTO>>(users);
        }

        public async Task<UserProfileDTO> getUserById(int id)
        {
            User user = await _iUsersRepository.getUserById(id);
            if (user == null)
                return null;
            return _mapper.Map<User, UserProfileDTO>(user);
        }

        public async Task<UserProfileDTO> registerUser(UserRegisterDTO userToRegister)
        {
            var checkPassword = _iPasswordService.checkStrengthPassword(userToRegister.Password);
            if (checkPassword.strength < 2)
            {
                return null;
            }
            User user = _mapper.Map<UserRegisterDTO, User>(userToRegister);
            user = await _iUsersRepository.registerUser(user);
            return _mapper.Map<User, UserProfileDTO>(user);
        }

        public async Task<UserProfileDTO> loginUser(UserLoginDTO userToLog)
        {
           
            User user = await _iUsersRepository.loginUser(userToLog);
            if (user == null)
                return null;
            return _mapper.Map<User, UserProfileDTO>(user);
        }

        public async Task<UserProfileDTO> updateUser(UserRegisterDTO userToUpdate, int id)
        {
            var checkPassword = _iPasswordService.checkStrengthPassword(userToUpdate.Password);
            if (checkPassword.strength < 2)
            {
                return null;
            }
            User user = _mapper.Map<UserRegisterDTO, User>(userToUpdate);
            user.UserId = id;
            user = await _iUsersRepository.updateUser(user, id);
            return _mapper.Map<User, UserProfileDTO>(user);
        }

        public async Task deleteUser(int id)
        {
            await _iUsersRepository.deleteUser(id);
        }
    }
}