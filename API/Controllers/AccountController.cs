using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using VL.Shared.Model;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<AccountController> _logger;
        private readonly IMapper _mapper;

        public AccountController(UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            ILogger<AccountController> logger,
            IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("register")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Register([FromBody] UserDTO userDTO)
        {
            _logger.LogInformation($"Registration Attempt for {userDTO.Email}");
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var user = _mapper.Map<IdentityUser>(userDTO);
                var result = await _userManager.CreateAsync(user);

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
                var result = await _signInManager.PasswordSignInAsync(userDTO.Email,
                    userDTO.Password,
                    false,
                    false);

                if (!result.Succeeded)
                {
                    return Unauthorized();
                }

                return Accepted();
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Login Error");
                return StatusCode(500, "Login error, please try again");
            }
        }
    }
}
