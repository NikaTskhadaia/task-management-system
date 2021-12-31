using System.Collections.Generic;
using System.Threading.Tasks;
using TMS.Application.Models;

namespace TMS.Application.Interfaces
{
    public interface IIdentityService
    {
        #region Roles
        IEnumerable<string> GetAllRoles();
        Task<Result> CreateRoleAsync(string roleName);
        Task<Result> DeleteRoleAsync(string roleName);
        Task<Result> AddPermissionToRoleAync(string roleName, Permissions value);       
        #endregion


        #region Users
        IEnumerable<string> GetAllUsers();
        Task<(JwtResult jwtResult, bool authorized)> LoginAsync(UserModel userModel);
        Task<Result> CreateUserAsync(UserModel userModel);
        Task<Result> DeleteUserAsync(string userName);
        Task<Result> AssignUserToRoleAsync(string userName, string roleName);
        Task<Result> RemoveUserFromRoleAsync(string userName, string roleName);
        #endregion
    }
}
