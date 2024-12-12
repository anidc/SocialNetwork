using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Dtos.Account;
using SocialNetwork.Interfaces;

namespace SocialNetwork.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
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

            return Ok("User registered succesfully");
        }
    }
}