using AutoMapper;
using BookService.Application.DTO.Books;
using BookService.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BookService.Application.Books.Queries
{
    public record GetByIdBookQuery(int Id) : IRequest<BookDTO>;
    public class GetByIdBookQueryHandler : IRequestHandler<GetByIdBookQuery, BookDTO>
    {
        private readonly BookContext _dbContext;
        private readonly IMapper _mapper;

        public GetByIdBookQueryHandler(BookContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<BookDTO> Handle(GetByIdBookQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var book = await _dbContext.Books.FirstOrDefaultAsync(b => b.Id == request.Id);
                if (book is null)
                {
                    throw new Exception($"Book with Id {request.Id} not found.");
                }
                var bookDTO = _mapper.Map<BookDTO>(book);
                return bookDTO;
            }
            catch (Exception)
            {
                throw;
            }

        }
    }
}
