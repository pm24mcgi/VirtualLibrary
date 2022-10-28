using Microsoft.AspNetCore.Identity;
using VL.Shared.Model;

namespace VL.Shared.Interfaces
{
    public interface IAccountService
    {
        Task<IdentityResult> RegisterAsync(UserDto userDto);

        Task<SignInResult> LoginAsync(LoginDto userDto);
    }
}
