using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VL.Shared.Model;

namespace VL.Shared.Interfaces
{
    public interface IAccountService
    {
        Task<IdentityResult> Register(UserDTO userDTO);
        Task<string> Login(LoginDTO userDTO);
    }
}
