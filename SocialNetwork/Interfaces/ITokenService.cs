using SocialNetwork.Models;

namespace SocialNetwork.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }
}