using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TMS.Application.Interfaces;
using TMS.Application.Models;

namespace TMS.Infrastructure.Identity
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly JwtOptions _jwtOptions;

        public IdentityService(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, IOptions<JwtOptions> options)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtOptions = options.Value;
        }

        public async Task<Result> AddPermissionToUserAync(string userName, Permissions value)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user is null)
            {
                return new Result
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Message = $"The user {user} not found!",
                    Succeded = false
                };
            }

            var result = await _userManager.AddClaimAsync(user, new Claim(nameof(CustomClaimTypes.Permission), value.ToString()));
            if (!result.Succeeded)
            {
                Result responseModel = new()
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                    Succeded = false
                };
                foreach (var item in result.Errors)
                {
                    responseModel.Message += $" {item.Description}";
                }
                return responseModel;
            }

            return new Result
            {
                StatusCode = (int)HttpStatusCode.OK,
                Message = "The user permission added successfully!",
                Succeded = true
            };
        }

        public async Task<Result> AssignUserToRoleAsync(string userName, string roleName)
        {
            if (string.IsNullOrEmpty(roleName) || string.IsNullOrEmpty(userName))
            {
                return new Result
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Message = "User or role name missing",
                    Succeded = false
                };
            }

            var roleExists = await _roleManager.RoleExistsAsync(roleName);
            if (!roleExists)
            {
                return new Result { 
                    StatusCode = (int)HttpStatusCode.BadRequest, 
                    Message = $"The role {roleName} does not exist. You should create the role explicitly!", 
                    Succeded = false 
                };
            }

            var user = await _userManager.FindByNameAsync(userName);
            if (user is null)
            {
                return new Result { 
                    StatusCode = (int)HttpStatusCode.BadRequest, 
                    Message = $"The user {userName} does not exist. You should create the user explicitly!", 
                    Succeded = false 
                };
            }

            if (await _userManager.IsInRoleAsync(user, roleName))
            {
                return new Result { 
                    StatusCode = (int)HttpStatusCode.Conflict, 
                    Message = $"The user already is in the role", 
                    Succeded = false 
                };
            }

            var result = await _userManager.AddToRoleAsync(user, roleName);
            if (!result.Succeeded)
            {
                Result responseModel = new() { 
                    StatusCode = (int)HttpStatusCode.InternalServerError, 
                    Succeded = false 
                };
                foreach (var item in result.Errors)
                {
                    responseModel.Message += $" {item.Description}";
                }
                return responseModel;
            }

            return new Result { 
                StatusCode = (int)HttpStatusCode.OK, 
                Message = "The role was added to the user successfully!", 
                Succeded = true 
            };
        }

        public async Task<Result> CreateUserAsync(UserModel userModel)
        {
            var user = await _userManager.FindByNameAsync(userModel.Username);
            if (user is not null)
            {
                return new Result
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Message = "User already exists!",
                    Succeded = false
                };
            }

            AppUser appUser = new()
            {
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = userModel.Username
            };

            var result = await _userManager.CreateAsync(appUser, userModel.Password);

            if (!result.Succeeded)
            {
                Result responseModel = new()
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                    Succeded = false
                };
                foreach (var item in result.Errors)
                {
                    responseModel.Message += $" {item.Description}";
                }
                return responseModel;
            }

            return new Result
            {
                StatusCode = (int)HttpStatusCode.Created,
                Message = "User created successfully!",
                Succeded = true
            };
        }

        public async Task<Result> CreateRoleAsync(string roleName)
        {
            var roleExists = await _roleManager.RoleExistsAsync(roleName);
            if (roleExists)
            {
                return new Result
                {
                    StatusCode = (int)HttpStatusCode.Conflict,
                    Message = "Role already exists!",
                    Succeded = false
                };
            }
            var result = await _roleManager.CreateAsync(new IdentityRole(roleName));

            if (!result.Succeeded)
            {
                Result responseModel = new()
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                    Succeded = false
                };
                foreach (var item in result.Errors)
                {
                    responseModel.Message += $" {item.Description}";
                }
                return responseModel;
            }

            return new Result
            {
                StatusCode = (int)HttpStatusCode.Created,
                Message = "Role created successfully!",
                Succeded = true
            };
        }

        public async Task<Result> DeleteUserAsync(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user is null)
            {
                return new Result
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Message = "User not found!",
                    Succeded = false
                };
            }
            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                Result responseModel = new()
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                    Succeded = false
                };
                foreach (var item in result.Errors)
                {
                    responseModel.Message += $" {item.Description}";
                }
                return responseModel;
            }

            return new Result
            {
                StatusCode = (int)HttpStatusCode.NoContent,
                Message = "User deleted successfully!",
                Succeded = true
            };
        }

        public async Task<Result> DeleteRoleAsync(string roleName)
        {
            var userRole = await _roleManager.FindByNameAsync(roleName);
            if (userRole is null)
            {
                return new Result
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Message = "Role not found!",
                    Succeded = false
                };
            }

            var result = await _roleManager.DeleteAsync(userRole);
            if (!result.Succeeded)
            {
                Result responseModel = new()
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                    Succeded = false
                };
                foreach (var item in result.Errors)
                {
                    responseModel.Message += $" {item.Description}";
                }
                return responseModel;
            }

            return new Result
            {
                StatusCode = (int)HttpStatusCode.NoContent,
                Message = "Role deleted successfully!",
                Succeded = true
            };
        }

        public IEnumerable<string> GetAllRoles()
        {
            return _roleManager.Roles.Select(x => x.Name).ToList();
        }

        public IEnumerable<string> GetAllUsers()
        {
            return _userManager.Users.Select(u => u.UserName).ToList();
        }

        public async Task<(JwtResult jwtResult, bool authorized)> LoginAsync(UserModel userModel)
        {
            var user = await _userManager.FindByNameAsync(userModel.Username);
            var passwordIsValid = await _userManager.CheckPasswordAsync(user, userModel.Password);
            if (user is null || !passwordIsValid)
            {
                return (null, false);
            }

            var userRoles = await _userManager.GetRolesAsync(user);

            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            var userClaims = await _userManager.GetClaimsAsync(user);

            foreach (var userClaim in userClaims)
            {
                authClaims.Add(new Claim(userClaim.Type, userClaim.Value));
            }

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Secret));

            var token = new JwtSecurityToken(
                issuer: _jwtOptions.ValidIssuer,
                audience: _jwtOptions.ValidAudience,
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return (
                    new JwtResult { Token = new JwtSecurityTokenHandler().WriteToken(token), Expiration = token.ValidTo },
                    true
                    );
        }

        public async Task<Result> RemoveUserFromRoleAsync(string userName, string roleName)
        {
            if (string.IsNullOrEmpty(roleName) || string.IsNullOrEmpty(userName))
            {
                return new Result
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Message = "User or role name missing",
                    Succeded = false
                };
            }

            var user = await _userManager.FindByNameAsync(userName);
            if (user is null)
            {
                return new Result
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Message = $"The user {userName} does not exist.",
                    Succeded = false
                };
            }

            if (!await _userManager.IsInRoleAsync(user, roleName))
            {
                return new Result
                {
                    StatusCode = (int)HttpStatusCode.Conflict,
                    Message = $"The user {userName} is not in the role",
                    Succeded = false
                };
            }

            var result = await _userManager.RemoveFromRoleAsync(user, roleName);
            if (!result.Succeeded)
            {
                Result responseModel = new()
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                    Succeded = false
                };
                foreach (var item in result.Errors)
                {
                    responseModel.Message += $" {item.Description}";
                }
                return responseModel;
            }

            return new Result
            {
                StatusCode = (int)HttpStatusCode.OK,
                Message = "The role was removed from the user successfully!",
                Succeded = true
            };
        }
    }
}
