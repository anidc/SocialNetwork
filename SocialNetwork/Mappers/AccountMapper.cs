using SocialNetwork.Dtos.Account;
using SocialNetwork.Models;

namespace SocialNetwork.Mappers
{
    public static class AccountMapper
    {
        public static UserDto ToUserDto(this AppUser appUser)
        {
            return new UserDto
            {
                Id = appUser.Id,
                Email = appUser.Email,
                Username = appUser.UserName
            };
        }
    }
}