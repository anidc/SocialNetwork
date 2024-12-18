using SocialNetwork.Dtos.Account;

namespace SocialNetwork.Interfaces
{
    public interface IAccountService
    {
        Task<NewUserDto> RegisterUser(RegisterDto registerDto);
        Task<NewUserDto> LoginUser(LoginDto loginDto);
        Task<UserDto> GetUserByIdAsync(string userId);
    }
}