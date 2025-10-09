using AuthorService.Application.DTO.Author;
using AuthorService.Models;
using AutoMapper;

namespace AuthorService.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Author, AuthorDTO>()
                .ForMember(dto => dto.Birthdate, options => options.MapFrom(m => 
                    m.Birthdate.HasValue ? m.Birthdate.Value.ToString("dd/MM/yyyy") : null));

        }
    }
}
