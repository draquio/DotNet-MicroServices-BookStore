using AuthorService.Models;
using AuthorService.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AuthorService.Application.Author.Queries
{
    public record GetAuthorByIdQuery(int Id) : IRequest<Models.Author>;

    public class GetAuthorByIdQueryHandler : IRequestHandler<GetAuthorByIdQuery, Models.Author>
    {
        private readonly AuthorContext _dbContext;

        public GetAuthorByIdQueryHandler(AuthorContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Models.Author> Handle(GetAuthorByIdQuery request, CancellationToken cancellationToken)
        {
            var author = await _dbContext.Authors.FirstOrDefaultAsync(a => a.Id == request.Id);
            if (author is null)
            {
                throw new Exception($"Author with Id {request.Id} not found.");
            }
            return author;
        }
    }
}
