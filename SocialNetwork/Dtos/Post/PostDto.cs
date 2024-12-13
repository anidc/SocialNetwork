using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using SocialNetwork.Dtos.Account;
using SocialNetwork.Models;

namespace SocialNetwork.Dtos
{
    public class PostDto
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int Likes { get; set; }
        public UserDto? User { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}