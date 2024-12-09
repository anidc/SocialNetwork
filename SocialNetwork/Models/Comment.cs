using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialNetwork.Models
{
    [Table("Comments")]
    public class Comment : BaseEntity
    {
        [Required] [MaxLength(2560)] public string Text { get; set; }
        public int PostId { get; set; }
        public string UserId { get; set; }
        public Post Post { get; set; }
        public AppUser User { get; set; }
    }
}