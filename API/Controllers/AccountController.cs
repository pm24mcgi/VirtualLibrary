﻿using Microsoft.AspNetCore.Identity;
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
        public async Task<IActionResult> Register([FromBody] UserDto userDto)
        {
            _logger.LogInformation($"Registration Attempt for {userDto.Email}");

            var result = await _accountService.Register(userDto);

            if (!result.Succeeded)
            {
                return BadRequest();
            }

            return Accepted();
            }

        [HttpPost]
        [Route("login")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Login([FromBody] LoginDto userDto)
        {
            _logger.LogInformation($"Login Attempt for {userDto.Email}");

            var result = await _accountService.Login(userDto);

            if (result.Contains("Login failed"))
            {
                return Unauthorized();
            }

            return Content(result);
        }
    }
}