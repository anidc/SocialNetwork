using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetwork.Dtos.Post
{
    public class UpdatePostDto
    {
        public string Content { get; set; } = string.Empty;
    }
}