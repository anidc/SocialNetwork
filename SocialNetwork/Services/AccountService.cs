using FirstCast.Application.Services;
using Microsoft.AspNetCore.Identity;
using SocialNetwork.Dtos.Account;
using SocialNetwork.Interfaces;
using SocialNetwork.Models;

namespace SocialNetwork.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<AppUser> _userManager;

        public AccountService(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<bool> RegisterUser(RegisterDto registerDto)
        {
            var appUser = new AppUser
            {
                UserName = registerDto.Username,
                Email = registerDto.Email
            };

            var createdUser = await _userManager.CreateAsync(appUser, registerDto.Password);

            if (createdUser.Succeeded)
            {
                var roleResult = await _userManager.AddToRoleAsync(appUser, "User");
                if (roleResult.Succeeded)
                {
                    return true;
                }
                else
                {
                    var errors = createdUser.Errors.Select(e => e.Description).ToList();
                    throw ExceptionManager.BadRequest(string.Join(" ", errors));
                }
            }
            else
            {
                var errors = createdUser.Errors.Select(e => e.Description).ToList();
                throw ExceptionManager.BadRequest(string.Join(" ", errors));
            }
        }
    }
}