using AutoMapper;
using BookService.Application.DTO.Books;
using BookService.Persistence;
using MediatR;

namespace BookService.Application.Books.Queries
{
    public record GetByGuidBookQuery(Guid Guid) : IRequest<BookDTO>;
    public class GetByGuidBookQueryHandler : IRequestHandler<GetByGuidBookQuery, BookDTO>
    {
        private readonly BookContext _dbContext;
        private readonly IMapper _mapper;

        public GetByGuidBookQueryHandler(BookContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<BookDTO> Handle(GetByGuidBookQuery request, CancellationToken cancellationToken)
        {
            var book = _dbContext.Books.FirstOrDefault(b => b.BookGuid == request.Guid);
            if (book is null)
            {
                throw new Exception($"Book with Guid {request.Guid} not found.");
            }
            var bookDTO = _mapper.Map<BookDTO>(book);
            return bookDTO;
        }
    }
}
