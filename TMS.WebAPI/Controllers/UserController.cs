using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TMS.Application.Models;
using TMS.Application.Interfaces;

namespace TMS.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IIdentityService _identityService;

        public UserController(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        [HttpGet("all")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetAll()
        {
            var result = _identityService.GetAllUsers();
            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserModel userModel)
        {
            (JwtResult jwtResult, bool authorized) = await _identityService.LoginAsync(userModel);

            if (!authorized)
            {
                return Unauthorized();
            }

            return Ok(jwtResult);
        }

        [HttpPost("create")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] UserModel userModel)
        {
            var result = await _identityService.CreateUserAsync(userModel);

            if (!result.Succeded)
            {
                return StatusCode(result.StatusCode, result.Message);
            }

            return Ok(result);
        }

        [HttpDelete("delete")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(string name)
        {
            var result = await _identityService.DeleteUserAsync(name);

            if (!result.Succeded)
            {
                return StatusCode(result.StatusCode, result.Message);
            }

            return Ok(result);
        }

        [HttpPost("assign-role")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AssignToRole(string userName, string roleName)
        {
            var result = await _identityService.AssignUserToRoleAsync(userName, roleName);

            if (!result.Succeded)
            {
                return StatusCode(result.StatusCode, result.Message);
            }

            return Ok(result);
        }

        [HttpPost("dismiss-role")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RemoveFromRole(string userName, string roleName)
        {
            var result = await _identityService.RemoveUserFromRoleAsync(userName, roleName);

            if (!result.Succeded)
            {
                return StatusCode(result.StatusCode, result.Message);
            }

            return Ok(result);
        }
    }
}
