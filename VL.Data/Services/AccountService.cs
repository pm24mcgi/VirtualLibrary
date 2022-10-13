using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Text;
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

            if (!result.Succeeded)
            {
                return "Login failed";
            }

            var user = await _userManager.FindByEmailAsync(userDTO.Email);
            var role = _userManager.GetRolesAsync(user).Result[0];

            var jwt = CreateToken(user, role);
            return jwt;

        }

        private string CreateToken(IdentityUser user, string role)
        {

            List<Claim> claims = new()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Role, role)
            };

            var secret = _configuration["Authentication:SecretKey"];

            var byteArray = Encoding.ASCII.GetBytes(secret);

            var signingKey = new SymmetricSecurityKey(byteArray);
            var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                issuer: _configuration["Authentication:Issuer"],
                audience: _configuration["Authentication:Audience"],
                expires: DateTime.Now.AddHours(1),
                signingCredentials: signingCredentials);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        //private string CreateToken(IdentityUser user)
        //{

        //    List<Claim> claims = new List<Claim>
        //    {
        //        new Claim(ClaimTypes.NameIdentifier, user.Id),
        //        new Claim(ClaimTypes.Name, user.UserName)
        //    };

        //    var secret = _configuration["Authentication:SecretKey"];

        //    var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secret));

        //    var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        //    var token = new JwtSecurityToken(
        //        claims: claims,
        //        expires: DateTime.Now.AddHours(1),
        //        signingCredentials: credentials);

        //    var jwt = new JwtSecurityTokenHandler().WriteToken(token);

        //    return jwt;
        //}
    }
}
