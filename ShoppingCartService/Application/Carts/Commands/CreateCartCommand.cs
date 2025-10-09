
using AutoMapper;
using MediatR;
using ShoppingCartService.Application.DTO.CartDetails;
using ShoppingCartService.Application.DTO.Carts;
using ShoppingCartService.Models;
using ShoppingCartService.Persistence;

namespace ShoppingCartService.Application.Carts.Commands
{
    public record CreateCartCommand(List<CartDetailsCreateDTO> Items) : IRequest<CartDTO>;
    public class CreateCartCommandHandler : IRequestHandler<CreateCartCommand, CartDTO>
    {
        private readonly CartContext _dbContext;
        private readonly IMapper _mapper;

        public CreateCartCommandHandler(CartContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<CartDTO> Handle(CreateCartCommand request, CancellationToken cancellationToken)
        {
            var cart = new Cart
            {
                CreatedAt = DateTime.UtcNow,
                CartDetails = request.Items.Select(cd => new CartDetail
                {
                    BookGuid = cd.BookGuid,
                    CreatedAt = DateTime.UtcNow
                }).ToList()
            };
            await _dbContext.Carts.AddAsync(cart, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            var cartDTO = _mapper.Map<CartDTO>(cart);
            return cartDTO;
        }
    }
}
