using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetwork.Dtos.Post
{
    public class UpdatePostDto
    {
        [Required]
        [MaxLength(2560, ErrorMessage = "Maximum length for the content is 2560 characters.")]
        public string Content { get; set; }
    }
}