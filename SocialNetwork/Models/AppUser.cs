using Microsoft.AspNetCore.Identity;

namespace SocialNetwork.Models
{
    public class AppUser : IdentityUser
    {
        public List<Post> Posts { get; set; }
    }
}