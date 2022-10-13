using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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

        public AccountController(
            ILogger<AccountController> logger,
            IAccountService accountService)
        {
            _logger = logger;
            _accountService = accountService;
        }

        public static IdentityUser user = new IdentityUser();

        [HttpPost]
        [Route("register")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Register([FromBody] UserDTO userDTO)
        {
            _logger.LogInformation($"Registration Attempt for {userDTO.Email}");

            try
            {
                var result = await _accountService.Register(userDTO);

                if (!result.Succeeded)
                {
                    return BadRequest();
                }

                return Accepted();
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Registration Error");
                return StatusCode(500, "Registration error, please try again");
            }
        }

        [HttpPost]
        [Route("login")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Login([FromBody] LoginDTO userDTO)
        {
            _logger.LogInformation($"Login Attempt for {userDTO.Email}");
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = await _accountService.Login(userDTO);

                if (result.Contains("Login failed"))
                {
                    return Unauthorized();
                }

                return Content(result);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Login Error");
                return StatusCode(500, "Login error, please try again");
            }
        }
    }
}