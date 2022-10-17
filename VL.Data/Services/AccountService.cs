using Microsoft.AspNetCore.Identity;
using VL.Shared.Interfaces;
using VL.Shared.Model;

namespace VL.Shared.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AccountService(UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<IdentityResult> RegisterAsync(UserDto userDto)
        {
            var user = new IdentityUser
            {
                UserName = userDto.Email,
                Email = userDto.Email
            };

            var userResult = await _userManager.CreateAsync(user, userDto.Password);

            if (userResult.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, userDto.Role);
            }
            return userResult;
        }

        public async Task<SignInResult> LoginAsync(LoginDto userDto)
        {
            var result = await _signInManager.PasswordSignInAsync(userDto.Email,
                userDto.Password,
                false,
                false);

            return result;
        }
    }
}
