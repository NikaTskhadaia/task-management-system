using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TMS.Application.Models;
using TMS.Application.Interfaces;

namespace TMS.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(Roles = "Admin")]
    public class RoleController : ControllerBase
    {
        private readonly IIdentityService _identityService;

        public RoleController(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        [HttpGet("all")]
        public IActionResult GetAll()
        {
            var result = _identityService.GetAllRoles();
            return Ok(result);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(string roleName)
        {
            var result = await _identityService.CreateRoleAsync(roleName);

            if (!result.Succeded)
            {
                return StatusCode(result.StatusCode, result.Message);
            }

            return Ok(result);
        }

        [HttpPost("delete")]
        public async Task<IActionResult> Delete(string roleName)
        {
            var result = await _identityService.DeleteRoleAsync(roleName);

            if (!result.Succeded)
            {
                return StatusCode(result.StatusCode, result.Message);
            }

            return Ok(result);
        }

        [HttpPost("add-role-permission")]
        public async Task<IActionResult> AddPermission(string roleName, Permissions value)
        {
            var result = await _identityService.AddPermissionToRoleAync(roleName, value);

            if (!result.Succeded)
            {
                return StatusCode(result.StatusCode, result.Message);
            }

            return Ok(result);
        }
    }
}
