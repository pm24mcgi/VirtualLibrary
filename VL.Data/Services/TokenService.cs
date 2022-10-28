using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using VL.Shared.Interfaces;
using VL.Shared.Model;

namespace VL.Shared.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<IdentityUser> _userManager;

        public TokenService(IConfiguration configuration, UserManager<IdentityUser> userManager)
        {
            _configuration = configuration;
            _userManager = userManager;
        }

        public async Task<string> GenerateTokenAsync(LoginDto userDto)
        {
            var identityUser = await _userManager.FindByEmailAsync(userDto.Email);
            var roles = await _userManager.GetRolesAsync(identityUser);
            var role = roles.First();

            List<Claim> claims = new()
            {
                new Claim(ClaimTypes.NameIdentifier, identityUser.Id),
                new Claim(ClaimTypes.Name, identityUser.UserName),
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
    }
}
