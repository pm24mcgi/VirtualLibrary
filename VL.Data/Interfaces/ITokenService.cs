using Microsoft.AspNetCore.Identity;
using VL.Shared.Model;

namespace VL.Shared.Interfaces
{
    public interface ITokenService
    {
        Task<string> GenerateTokenAsync(LoginDto userDto);
    }
}
