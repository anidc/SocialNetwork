using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Dtos.Account;
using SocialNetwork.Interfaces;

namespace SocialNetwork.Controllers
{
    [Route("api/controller")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
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