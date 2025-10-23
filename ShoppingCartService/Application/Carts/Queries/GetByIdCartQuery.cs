using MediatR;
using Microsoft.EntityFrameworkCore;
using ShoppingCartService.Application.DTO.CartDetails;
using ShoppingCartService.Application.DTO.Carts;
using ShoppingCartService.External.DTOs.Authors;
using ShoppingCartService.External.DTOs.Books;
using ShoppingCartService.External.Services.Author.Interfaces;
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
        private readonly IAuthorService _authorService;

        public GetByIdCartQueryHandler(CartContext dbContext, IBookService bookService, IAuthorService authorService)
        {
            _dbContext = dbContext;
            _bookService = bookService;
            _authorService = authorService;
        }

        public async Task<CartDTO> Handle(GetByIdCartQuery request, CancellationToken cancellationToken)
        {
            var cartDetailsDTOs = new List<CartDetailsDTO>();

            // Fetch the cart by ID
            var cart = await _dbContext.Carts.FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);
            if (cart is null) throw new KeyNotFoundException($"Cart with ID {request.Id} not found");

            // Fetch cart details associated with the cart
            var cartDetails = await _dbContext.CartDetails.Where(cd => cd.CartId == cart.Id).ToListAsync(cancellationToken);

            foreach (var item in cartDetails)
            {
                BookDTO? book = null;
                AuthorDTO? author = null;
                var response = await _bookService.GetBook(item.BookGuid);
                if (response.book?.AuthorGuid != null)
                {
                    var authorGuid = response.book.AuthorGuid ?? Guid.Empty;
                    var responseAuthor = await _authorService.GetAuthor(authorGuid);
                    author = responseAuthor.author ?? new AuthorDTO();
                }
                if (response.result)
                {
                    book = response.book;
                    var cartDetailsDto = new CartDetailsDTO
                    {
                        Id = item.Id,
                        TitleBook = book?.Title,
                        PublishDate = book?.PublishDate,
                        BookGuid = book?.BookGuid != null ? book.BookGuid.Value : Guid.Empty,
                        AuthorBook = $"{author?.Name ?? "" } {author?.Lastname ?? ""}".Trim(),

                    };
                    cartDetailsDTOs.Add(cartDetailsDto);
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
