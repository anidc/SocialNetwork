using SocialNetwork.Dtos.Account;

namespace SocialNetwork.Interfaces
{
    public interface IAccountService
    {
        Task<bool> RegisterUser(RegisterDto registerDto);
    }
}