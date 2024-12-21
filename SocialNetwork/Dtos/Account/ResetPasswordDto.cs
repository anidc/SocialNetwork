using System.ComponentModel.DataAnnotations;

namespace SocialNetwork.Dtos.Account;

public class ResetPasswordDto
{
    [Required] 
    [EmailAddress] 
    public string Email { get; set; }

}