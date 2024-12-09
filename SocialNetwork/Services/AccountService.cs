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
        private readonly ITokenService _tokenService;

        public AccountService(UserManager<AppUser> userManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
        }

        public async Task<NewUserDto> RegisterUser(RegisterDto registerDto)
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
                    return
                        new NewUserDto
                        {
                            Username = appUser.UserName,
                            Email = appUser.Email,
                            Token = _tokenService.CreateToken(appUser)
                        };
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