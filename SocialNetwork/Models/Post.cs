using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialNetwork.Models
{
    [Table("Posts")]
    public class Post : BaseEntity
    {
        [Required] [MaxLength(2560)] public string Content { get; set; }
        public string UserId { get; set; }
        public AppUser User { get; set; }
        public List<Comment> Comments { get; set; }
        public List<Guid> Likes { get; set; }
    }
}