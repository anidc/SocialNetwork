using SocialNetwork.Dtos.Account;

namespace SocialNetwork.Interfaces
{
    public interface IAccountService
    {
        Task<NewUserDto> RegisterUser(RegisterDto registerDto);
        Task<NewUserDto> LoginUser(LoginDto loginDto);
        Task<UserDto> GetUserByIdAsync(string userId);
        Task ResetPasswordAsync(ResetPasswordDto resetPasswordDto);
        Task UpdatePassword(UpdatePasswordDto updatePasswordDto);
        Task ChangePasswordAsync(string userId, ChangePasswordDto changePasswordDto);
    }
}