using SocialNetwork.Dtos.Account;

namespace SocialNetwork.Interfaces
{
    public interface IAccountService
    {
        Task<NewUserDto> RegisterUser(RegisterDto registerDto);
    }
}