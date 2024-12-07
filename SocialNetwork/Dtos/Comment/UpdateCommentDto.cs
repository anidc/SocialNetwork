using System.ComponentModel.DataAnnotations;

namespace SocialNetwork.Dtos.Comment
{
    public class UpdateCommentDto
    {
        [Required]
        [MaxLength(2560, ErrorMessage = "Maximum length for the content is 2560 characters.")]
        public string Text { get; set; }
    }
}
