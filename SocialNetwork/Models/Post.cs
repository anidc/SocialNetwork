using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetwork.Models
{
    [Table("Posts")]
    public class Post : BaseEntity
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(2560)]
        public string Content { get; set; }
        public int Likes { get; set; } = 0;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; }
        public DateTime DeletedAt { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsDraft { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}