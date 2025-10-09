using AutoMapper;
using ShoppingCartService.Application.DTO.CartDetails;
using ShoppingCartService.Application.DTO.Carts;
using ShoppingCartService.Models;

namespace ShoppingCartService.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CartDetail, CartDetailsDTO>()
                .ForMember(dto => dto.CreatedAt, options => options.MapFrom(m => m.CreatedAt.ToString("dd/MM/yyyy")));
            CreateMap<Cart, CartDTO>()
                .ForMember(dto => dto.CartDetails, options => options.MapFrom(m => m.CartDetails));
        }
    }
}
