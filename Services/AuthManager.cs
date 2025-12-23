using AutoMapper;
using Entities.Dtos;
using Microsoft.AspNetCore.Identity;
using Services.Contracts;

namespace Services
{
    public class AuthManager : IAuthService
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IMapper _autoMapper;

        public AuthManager(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager, IMapper autoMapper)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _autoMapper = autoMapper;
        }

        public IEnumerable<IdentityRole> Roles => _roleManager.Roles;

        public async Task<IdentityResult> CreateUser(UserDtoForCreation userDto)
        {
            var user = _autoMapper.Map<IdentityUser>(userDto);
            var result = await _userManager.CreateAsync(user, userDto.Password);
            if (!result.Succeeded)
            {
                throw new Exception("User could not be created");
            }

            if (userDto.Roles.Count>0)
            {
                var roleResult = await _userManager.AddToRolesAsync(user, userDto.Roles);
                if (!roleResult.Succeeded)
                {
                    throw new Exception("System have problems with roles");
                }
            }
            return result;
        }

        public async Task<IdentityResult> DeleteOneUser(string userName)
        {
            var user = await GetOneUser(userName);
            return await _userManager.DeleteAsync(user);
        }

        public IEnumerable<IdentityUser> GetAllUsers()
        {
            return _userManager.Users.ToList();
        }

        public async Task<IdentityUser> GetOneUser(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user is not null)
                return user;
            throw new Exception("User could not be found");
        }

        public async Task<UserDtoForUpdate> GetOneUserForUpdate(string userName)
        {
            var user = await GetOneUser(userName);
            if (user is not null)
            {
                var userDto =  _autoMapper.Map<UserDtoForUpdate>(user);
                userDto.Roles = new HashSet<string>(Roles.Select(r => r.Name));
                userDto.UserRoles = new HashSet<string>(await _userManager.GetRolesAsync(user));
                return userDto;
            }
            throw new Exception("Could not retrieve a user for update");
        }

        public async Task<IdentityResult> ResetPassword(ResetPasswordDto resetPasswordDto)
        {
            var user = await GetOneUser(resetPasswordDto.UserName);
            if (user is not null)
            {
                var resultPasswordRemove = await _userManager.RemovePasswordAsync(user);
                if (resultPasswordRemove.Succeeded)
                {
                    var result = await _userManager.AddPasswordAsync(user, resetPasswordDto.Password);
                    return result;
                }
            }
            throw new Exception("User could not be found");
        }

        public async Task Update(UserDtoForUpdate userDto)
        {
            var user = await GetOneUser(userDto.UserName);
            user.Email = userDto.Email;

            if (user is not null)
            {
                var result = await _userManager.UpdateAsync(user);
                if (userDto.Roles.Count > 0)
                {
                    var userRoles = await _userManager.GetRolesAsync(user);
                    var resultRoleRemove = await _userManager.RemoveFromRolesAsync(user, userRoles);
                    var resultRoleAdding = await _userManager.AddToRolesAsync(user, userDto.Roles);
                }
                return;
            }
            throw new Exception("User update is failed");
        }
    }
}
