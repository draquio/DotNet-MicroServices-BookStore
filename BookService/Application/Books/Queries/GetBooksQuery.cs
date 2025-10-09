using AutoMapper;
using BookService.Application.DTO.Books;
using BookService.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BookService.Application.Books.Queries
{
    public record GetBooksQuery(int Page, int Pagesize) : IRequest<List<BookDTO>>;
    public class GetBooksQueryHandler : IRequestHandler<GetBooksQuery, List<BookDTO>>
    {
        private readonly BookContext _dbContext;
        private readonly IMapper _mapper;

        public GetBooksQueryHandler(BookContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<List<BookDTO>> Handle(GetBooksQuery request, CancellationToken cancellationToken)
        {
            var books = await _dbContext.Books.Skip((request.Page - 1) * request.Pagesize)
                .Take(request.Pagesize)
                .ToListAsync();

            if(books is null)
            {
                return new List<BookDTO>();
            }

            var booksDTO = _mapper.Map<List<BookDTO>>(books);
            return booksDTO;
        }
    }
}
