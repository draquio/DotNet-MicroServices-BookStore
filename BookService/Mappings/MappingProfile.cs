using AutoMapper;
using BookService.Application.DTO.Books;
using BookService.Models;

namespace BookService.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Book, BookDTO>()
                .ForMember(dto => dto.PublishDate, options => options.MapFrom(b =>
                    b.PublishDate.HasValue ? b.PublishDate.Value.ToString("dd/MM/yyyy") : null));
        }
    }
}
