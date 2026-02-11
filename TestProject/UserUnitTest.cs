using Entities;
using Moq;
using Repository;
using Services;
using DTOs;
using AutoMapper;
using System.Threading.Tasks;
using System.Collections.Generic;
using Xunit;
using Org.BouncyCastle.Crypto;

namespace TestProject
{
    public class UserUnitTest
    {
        private readonly Mock<IUsersRepository> _mockRepository;
        private readonly Mock<IPasswordService> _mockPasswordService;
        private readonly Mock<IMapper> _mockMapper;
        private readonly UsersServices _usersService;

        public UserUnitTest()
        {
            _mockRepository = new Mock<IUsersRepository>();
            _mockPasswordService = new Mock<IPasswordService>();
            _mockMapper = new Mock<IMapper>();

            _usersService = new UsersServices(
                _mockRepository.Object,
                _mockPasswordService.Object,
                _mockMapper.Object
            );
        }

        [Fact]
        public async Task GetUserById_ReturnsDto_WhenUserExists()
        {
            var user = new User { UserId = 1, FirstName = "Test" };
            var userDto = new UserProfileDTO { UserId = 1, FullName = "Test" };

            _mockRepository.Setup(repo => repo.getUserById(1)).ReturnsAsync(user);
            _mockMapper.Setup(m => m.Map<User, UserProfileDTO>(user)).Returns(userDto);

            var result = await _usersService.getUserById(1);

            Assert.NotNull(result);
            Assert.Equal(1, result.UserId);
        }

        [Fact]
        public async Task RegisterUser_ReturnsNull_WhenPasswordIsWeak()
        {
            var userRegisterDto = new UserRegisterDTO { Password = "123" };

            _mockPasswordService.Setup(ps => ps.checkStrengthPassword(It.IsAny<string>()))
                .Returns((1, "Weak"));

            var result = await _usersService.registerUser(userRegisterDto);

            Assert.Null(result);
            _mockRepository.Verify(repo => repo.registerUser(It.IsAny<User>()), Times.Never);
        }

        [Fact]
        public async Task LoginUser_ReturnsNull_WhenCredentialsInvalid()
        {
            var loginDto = new UserLoginDTO { Email = "wrong@test.com", Password = "123" };
            _mockRepository.Setup(repo => repo.loginUser(loginDto)).ReturnsAsync((User)null);

            var result = await _usersService.loginUser(loginDto);

            Assert.Null(result);
        }

        [Fact]
        public async Task GetAllUsers_ReturnsListOfDtos()
        {
            var users = new List<User> { new User { UserId = 1 }, new User { UserId = 2 } };
            var userDtos = new List<UserProfileDTO> { new UserProfileDTO { UserId = 1 }, new UserProfileDTO { UserId = 2 } };

            _mockRepository.Setup(repo => repo.getAllUsers()).ReturnsAsync(users);
            _mockMapper.Setup(m => m.Map<IEnumerable<User>, IEnumerable<UserProfileDTO>>(users)).Returns(userDtos);

            var result = await _usersService.getAllUsers();

            Assert.NotNull(result);
            Assert.Equal(2, ((List<UserProfileDTO>)result).Count);
        }

        [Fact]
        public async Task DeleteUser_CallsRepository()
        {
            await _usersService.deleteUser(1);

            _mockRepository.Verify(repo => repo.deleteUser(1), Times.Once);
        }
    }
}