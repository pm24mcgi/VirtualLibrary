using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using VL.Shared.Data;
using VL.Shared.Interfaces;
using VL.Shared.Model;

namespace VL.Shared.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _applicationDbContext;


        public AccountService(UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            IConfiguration configuration,
            ApplicationDbContext applicationDbContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _applicationDbContext = applicationDbContext;
        }

        public async Task<IdentityResult> Register(UserDTO userDTO)
        {
            var user = new IdentityUser
            {
                UserName = userDTO.Email,
                Email = userDTO.Email
            };

            var userResult = await _userManager.CreateAsync(user, userDTO.Password);

            if (userResult.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, userDTO.Role);
            }
            return userResult;
        }

        public async Task<string> Login(LoginDTO userDTO)
        {
            var result = await _signInManager.PasswordSignInAsync(userDTO.Email,
                    userDTO.Password,
                    false,
                    false);

            var user = await _userManager.FindByEmailAsync(userDTO.Email);
            //var userObj = await _userManager.FindByIdAsync(user.Id);
            //var role = await _userManager.GetRolesAsync(userObj.Id).FirstOrDefault();
            //var role = await _userManager.GetRolesAsync(user);
            //var role = await _roleManager.GetClaimsAsync();

            string jwt = CreateToken(user);
            //string jwt = CreateToken(user, role);
            return jwt;
        }

        private string CreateToken(IdentityUser user)
        //private string CreateToken(IdentityUser user, IdentityRole role)
        {

            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserName)
                //new Claim(ClaimTypes.Role, role.Name)
            };

            var secret = "This is my secret key that should be in app settings";
            //var secret = _configuration.GetSection("JWT").Value;

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(secret));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: credentials);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
    }
}
