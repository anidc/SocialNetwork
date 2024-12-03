using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetwork.Models
{
    [Table("Comments")]
    public class Comment : BaseEntity
    {
        [Required]
        [MaxLength(2560)]
        public string Text { get; set; }
        public int PostId { get; set; }
        public Post Post { get; set; }
    }
}