using MediatR;
using Microsoft.EntityFrameworkCore;
using ShoppingCartService.Application.DTO.CartDetails;
using ShoppingCartService.Application.DTO.Carts;
using ShoppingCartService.External.Services.Books.Interfaces;
using ShoppingCartService.Models;
using ShoppingCartService.Persistence;

namespace ShoppingCartService.Application.Carts.Queries
{
    public record GetByIdCartQuery(int Id) : IRequest<CartDTO>;
    public class GetByIdCartQueryHandler : IRequestHandler<GetByIdCartQuery, CartDTO>
    {
        private readonly CartContext _dbContext;
        private readonly IBookService _bookService;

        public GetByIdCartQueryHandler(CartContext dbContext, IBookService bookService)
        {
            _dbContext = dbContext;
            _bookService = bookService;
        }

        public async Task<CartDTO> Handle(GetByIdCartQuery request, CancellationToken cancellationToken)
        {
            var cartDetailsDTOs = new List<CartDetailsDTO>();

            // Fetch the cart by ID
            var cart = await _dbContext.Carts.FirstOrDefaultAsync(c => c.Id == request.Id);
            if (cart is null) throw new KeyNotFoundException($"Cart with ID {request.Id} not found");

            // Fetch cart details associated with the cart
            var cartDetails = await _dbContext.CartDetails.Where(cd => cd.CartId == cart.Id).ToListAsync();

            if (cartDetails.Any())
            {
                foreach (var item in cartDetails)
                {
                    var response = await _bookService.GetBook(item.BookGuid);
                    if (response.result)
                    {
                        var book = response.book;
                        var cartDetailsDto = new CartDetailsDTO
                        {
                            TitleBook = book.Title,
                            PublishDate = book.PublishDate,
                            BookGuid = book.BookGuid.HasValue ? book.BookGuid.Value : Guid.Empty,
                        };
                        cartDetailsDTOs.Add(cartDetailsDto);
                    }
                }
            }

            // build and return the CartDTO
            var cartDto = new CartDTO
            {
                Id = cart.Id,
                CreatedAt = cart.CreatedAt.ToString("yyyy-MM-dd HH:mm:ss"),
                CartDetails = cartDetailsDTOs
            };

            return cartDto;
        }
    }
}
