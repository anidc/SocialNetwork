using System.Text;
using System.Web;
using FirstCast.Application.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SocialNetwork.Dtos.Account;
using SocialNetwork.Interfaces;
using SocialNetwork.Mappers;
using SocialNetwork.Models;

namespace SocialNetwork.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IEmailService _emailService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AccountService(UserManager<AppUser> userManager, ITokenService tokenService,
            SignInManager<AppUser> signInManager, IEmailService emailService, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _signInManager = signInManager;
            _emailService = emailService;
            _httpContextAccessor = httpContextAccessor;
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

        public async Task<NewUserDto> LoginUser(LoginDto loginDto)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == loginDto.Username.ToLower());

            if (user == null) throw ExceptionManager.BadRequest("Username or password is incorrect!");

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (!result.Succeeded) throw ExceptionManager.NotAuthorized();

            return new NewUserDto
            {
                Username = user.UserName,
                Email = user.Email,
                Token = _tokenService.CreateToken(user)
            };
        }

        public async Task<UserDto> GetUserByIdAsync(string userId)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == userId);

            if (user == null) throw ExceptionManager.BadRequest("Something went wrong.");

            return user.ToUserDto();
        }

        public async Task ResetPasswordAsync(ResetPasswordDto resetPasswordDto)
        {
            var user = await _userManager.FindByEmailAsync(resetPasswordDto.Email);

            if (user == null) throw ExceptionManager.NotAuthorized();

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            var encodedToken = Uri.EscapeDataString(token);
            var encodedEmail = Uri.EscapeDataString(resetPasswordDto.Email);

            var resetLink = $"http://localhost:4200/reset-password?token={encodedToken}&email={encodedEmail}";

            var path = Path.Combine(AppContext.BaseDirectory, "Templates", "PasswordResetEmail.html");

            var template = await File.ReadAllTextAsync(path);
            var stringBuilder = new StringBuilder(template);

            stringBuilder.Replace("{{reset_link}}", resetLink);
            stringBuilder.Replace("{{current_year}}", DateTime.Now.Year.ToString());

            await _emailService.SendEmailAsync(resetPasswordDto.Email, "Reset password", stringBuilder.ToString());
        }

        public async Task UpdatePassword(UpdatePasswordDto updatePasswordDto)
        {
            var user = await _userManager.FindByEmailAsync(updatePasswordDto.Email);

            if (user == null) throw ExceptionManager.BadRequest("");

            if (updatePasswordDto.Password != null &&
                !updatePasswordDto.Password.Equals(updatePasswordDto.ConfirmPassword))
                throw ExceptionManager.BadRequest("Passwords do not match!");

            if (updatePasswordDto.Token == null) throw ExceptionManager.AccessDenied();

            var resetResult =
                await _userManager.ResetPasswordAsync(user, updatePasswordDto.Token, updatePasswordDto.Password);

            if (!resetResult.Succeeded)
            {
                throw ExceptionManager.BadRequest("Something went wrong");
            }
        }

        public async Task ChangePasswordAsync(string userId, ChangePasswordDto changePasswordDto)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) throw ExceptionManager.NotAuthorized();

            var result =
                await _userManager.ChangePasswordAsync(user, changePasswordDto.CurrentPassword, changePasswordDto.NewPassword);

            if (!result.Succeeded) throw ExceptionManager.BadRequest("Something went wrong");
        }
    }
}