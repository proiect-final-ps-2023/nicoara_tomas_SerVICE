using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using SerVICE.Services.ServiceForCategory;
using SerVICE.Services.ServiceForUser;
using System.Threading.Tasks;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace SerVICE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userController)
        {

            _userService = userController;
        }

        [HttpGet]
        public async Task<ActionResult<List<User>>> GetAllUsers()
        {
            return await _userService.GetAllUsers();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUserById(int id)
        {
            try
            {
                var uaser = await _userService.GetUserById(id);
                return Ok(uaser);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<List<User>>> AddUser(User user)
        {
            var new_user = await _userService.AddUser(user);
            return Ok(new_user);
        }

        [HttpGet("name/{name}")]
        public async Task<ActionResult<User>> GetUserByUsername(string name)
        {
            try
            {
                var user = await _userService.GetUserByUsername(name);
                return Ok(user);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<User>>> DeleteUser(int id)
        {
            try
            {
                var result = await _userService.DeleteUser(id);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<List<User>>> UpdateUser(int id, User category)
        {
            try
            {
                var result = await _userService.UpdateUser(id, category);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
