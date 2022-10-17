using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NuGet.Common;
using VL.Shared.Interfaces;
using VL.Shared.Model;

namespace API.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly ILogger<AccountController> _logger;
        private readonly IAccountService _accountService;
        private readonly ITokenService _tokenService;

        public AccountController(
            ILogger<AccountController> logger,
            IAccountService accountService,
            ITokenService tokenService)
        {
            _logger = logger;
            _accountService = accountService;
            _tokenService = tokenService;
        }

        [HttpPost]
        [Route("register")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Register([FromBody] UserDto userDto)
        {
            var result = await _accountService.RegisterAsync(userDto);

            if (!result.Succeeded)
            {
                return BadRequest();
            }
            
            var jwt = await _tokenService.GenerateTokenAsync(userDto);

            return Ok(jwt);
        }

        [HttpPost]
        [Route("login")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Login([FromBody] LoginDto userDto)
        {
            var login = await _accountService.LoginAsync(userDto);

            if (!login.Succeeded)
            {
                return Unauthorized();
            }

            var jwt = _tokenService.GenerateTokenAsync(userDto);
            return Ok(jwt);
        }
    }
}