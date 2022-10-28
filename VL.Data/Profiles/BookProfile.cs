using AutoMapper;
using VL.Shared.Model;

namespace VL.Shared.Profiles
{
    public class BookProfile : Profile
    {
        public BookProfile()
        {
            CreateMap<BookDto, Book>()
                .ForMember(
                    dest => dest.Id,
                    from
                        => from.MapFrom(x => x.Id)
                )
                .ForMember(
                    dest => dest.Author,
                    from
                        => from.MapFrom(x => x.Author)
                )
                .ForMember(dest => dest.Title,
                    from
                        => from.MapFrom(x => x.Title)
                )
                .ForMember(dest => dest.Description,
                    from
                        => from.MapFrom(x => x.Description)
                )
                .ReverseMap();
        }
    }
}
