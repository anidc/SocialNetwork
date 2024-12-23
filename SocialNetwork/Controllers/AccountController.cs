using FirstCast.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Dtos.Account;
using SocialNetwork.Helpers;
using SocialNetwork.Interfaces;

namespace SocialNetwork.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IEmailService _emailService;

        public AccountController(IAccountService accountService, IEmailService emailService)
        {
            _accountService = accountService;
            _emailService = emailService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginUser(LoginDto loginDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _accountService.LoginUser(loginDto);

            return Ok(result);
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterDto registerDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            await _accountService.RegisterUser(registerDto);

            return Ok("User registered successfully");
        }

        [Authorize]
        [HttpGet("me")]
        public async Task<IActionResult> GetMe()
        {
            var userId = ClaimsHelper.GetUserId(User);

            if (userId == null) throw ExceptionManager.NotAuthorized();

            var result = await _accountService.GetUserByIdAsync(userId);

            return Ok(result);
        }

        [AllowAnonymous]
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto resetPasswordDto)
        {
            if (string.IsNullOrWhiteSpace(resetPasswordDto.Email))
            {
                return BadRequest("Invalid request.");
            }

            try
            {
                await _accountService.ResetPasswordAsync(resetPasswordDto);
                return Ok("Password reset email sent.");
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                // Handle other exceptions if necessary
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [AllowAnonymous]
        [HttpPost("update-password")]
        public async Task<IActionResult> UpdatePassword([FromBody] UpdatePasswordDto updatePasswordDto)
        {
            await _accountService.UpdatePassword(updatePasswordDto);

            return Ok();
        }

        [Authorize]
        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto changePasswordDto)
        {
            var userId = ClaimsHelper.GetUserId(User);

            await _accountService.ChangePasswordAsync(userId, changePasswordDto);

            return Ok();
        }
        
    }
}