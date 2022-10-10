using Microsoft.AspNetCore.Identity;
using AutoMapper;
using VL.Shared.Model;

namespace API.Config
{
    public class MapInitializer : Profile
    {
        public MapInitializer()
        {
            CreateMap<IdentityUser, UserDTO>().ReverseMap();
        }
    }
}
