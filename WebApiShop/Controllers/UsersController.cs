using DTOs;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApiShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersServices _iUsersServices;
        private readonly ILogger<UsersController> _logger;

        public UsersController(IUsersServices iUsersServices, ILogger<UsersController> logger)
        {
            _iUsersServices = iUsersServices;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserProfileDTO>>> GetAllUsers()
        {
            IEnumerable<UserProfileDTO> users = await _iUsersServices.GetAllUsers();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserProfileDTO>> GetUserById(int id)
        {
            UserProfileDTO user = await _iUsersServices.GetUserById(id);
            if (user == null)
            {
                _logger.LogWarning("User with ID {id} was not found", id);
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult<UserProfileDTO>> RegisterUser(UserRegisterDTO user)
        {
            try
            {
                UserProfileDTO result = await _iUsersServices.RegisterUser(user);
                _logger.LogInformation("User registered successfully: ID: {Id}, Email: {Email}", result.UserId, user.Email);
                return CreatedAtAction(nameof(GetAllUsers), new { id = result.UserId }, result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Registration failed for user email: {Email}", user.Email);
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserProfileDTO>> LoginUser([FromBody] UserLoginDTO userToLog)
        {
            UserProfileDTO user = await _iUsersServices.LoginUser(userToLog);
            if (user == null)
            {
                _logger.LogInformation("Login failed for email: {Email}", userToLog.Email);
                return BadRequest();
            }
            _logger.LogInformation("User login successfully: Name: {FullName}, Email: {Email}", user.FullName, userToLog.Email);
            return Ok(user);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateUser(int id, [FromBody] UserRegisterDTO userToUpdate)
        {
            UserProfileDTO user = await _iUsersServices.UpdateUser(userToUpdate, id);
            if (user == null)
            {
                _logger.LogWarning("Update failed: User with ID {id} not found or invalid data", id);
                return BadRequest();
            }
            _logger.LogInformation("User with ID {id} updated successfully", id);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser(int id)
        {
            UserProfileDTO user = await _iUsersServices.GetUserById(id);

            if (user == null)
            {
                _logger.LogWarning("Delete failed: Attempted to delete non-existent user with ID {id}", id);
                return NotFound();
            }

            await _iUsersServices.DeleteUser(id);
            _logger.LogInformation("User with ID {id} was deleted from the system", id);
            return NoContent();
        }
    }
}