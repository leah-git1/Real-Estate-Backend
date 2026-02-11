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
        public async Task<ActionResult<IEnumerable<UserProfileDTO>>> Get()
        {
            IEnumerable<UserProfileDTO> users = await _iUsersServices.getAllUsers();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserProfileDTO>> Get(int id)
        {
            UserProfileDTO user = await _iUsersServices.getUserById(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult<UserProfileDTO>> Post([FromBody] UserRegisterDTO user)
        {
            UserProfileDTO postUser = await _iUsersServices.registerUser(user);
            if (postUser == null)
            {
                return BadRequest();
            }
            return CreatedAtAction(nameof(Get), new { id = postUser.UserId }, postUser);
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserProfileDTO>> Login([FromBody] UserLoginDTO userToLog)
        {
            UserProfileDTO user = await _iUsersServices.loginUser(userToLog);
            if (user == null)
            {
                _logger.LogInformation("Login failed for email: {Email}", userToLog.Email);
                return BadRequest();
            }
            _logger.LogInformation("User login successfully: Name: {FullName}, Email: {Email}", user.FullName, userToLog.Email);
            return Ok(user);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] UserRegisterDTO userToUpdate)
        {
            UserProfileDTO user = await _iUsersServices.updateUser(userToUpdate, id);
            if (user == null)
            {
                return BadRequest();
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _iUsersServices.deleteUser(id);
            return NoContent();
        }
    }
}