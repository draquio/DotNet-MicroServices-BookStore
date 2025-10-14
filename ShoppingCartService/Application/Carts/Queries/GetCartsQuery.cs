using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ShoppingCartService.Application.DTO.Carts;
using ShoppingCartService.External.Services.Books.Interfaces;
using ShoppingCartService.Persistence;

namespace ShoppingCartService.Application.Carts.Queries
{
    public record GetCartsQuery(int Page, int Pagesize) : IRequest<List<CartDTO>>;
    public class  GetCartsQueryHandler : IRequestHandler<GetCartsQuery, List<CartDTO>>
    {
        private readonly CartContext _dbContext;
        private readonly IMapper _mapper;

        public GetCartsQueryHandler(CartContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<List<CartDTO>> Handle(GetCartsQuery request, CancellationToken cancellationToken)
        {
            var carts = await _dbContext.Carts
                .Skip((request.Page - 1) * request.Pagesize)
                .Take(request.Pagesize)
                .ToListAsync();
            if (carts is null)
            {
                return new List<CartDTO>();
            }
            var cartDTOs = _mapper.Map<List<CartDTO>>(carts);
            return cartDTOs;
        }
    }
}
