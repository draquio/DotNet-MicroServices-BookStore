using AuthorService.Application.DTO.Author;
using AuthorService.Persistence;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AuthorService.Application.Author.Queries
{
    public record GetByGuidAuthorQuery(Guid Guid) : IRequest<AuthorDTO>;
    public class GetByGuidAuthorQueryHandler : IRequestHandler<GetByGuidAuthorQuery, AuthorDTO>
    {
        private readonly AuthorContext _dbContext;
        private readonly IMapper _mapper;

        public GetByGuidAuthorQueryHandler(AuthorContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<AuthorDTO> Handle(GetByGuidAuthorQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var author = await _dbContext.Authors.FirstOrDefaultAsync(a => a.AuthorGuid == request.Guid);
                if (author is null)
                {
                    throw new Exception($"Author with Guid {request.Guid} not found.");
                }
                var authorDTO = _mapper.Map<AuthorDTO>(author);
                return authorDTO;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
