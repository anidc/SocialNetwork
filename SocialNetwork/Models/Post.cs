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
        [Required]
        [MaxLength(2560)]
        public string Content { get; set; }
        public int Likes { get; set; } = 0;
    }
}