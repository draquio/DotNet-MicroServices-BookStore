using AuthorService.Application.DTO.Author;
using AuthorService.Models;
using AuthorService.Persistence;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AuthorService.Application.Author.Queries
{
    public record GetAuthorsQuery(int Page, int Pagesize) : IRequest<List<AuthorDTO>>;
    public class GetAuthorsQueryHandler : IRequestHandler<GetAuthorsQuery, List<AuthorDTO>>
    {
        private readonly AuthorContext _dbContext;
        private readonly IMapper _mapper;

        public GetAuthorsQueryHandler(AuthorContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<List<AuthorDTO>> Handle(GetAuthorsQuery request, CancellationToken cancellationToken)
        {
            var authors = await _dbContext.Authors.Skip((request.Page - 1) * request.Pagesize)
                .Take(request.Pagesize)
                .ToListAsync();
            if (authors is null)
            {
                return new List<AuthorDTO>();
            }
            var authorsDTO = _mapper.Map<List<AuthorDTO>>(authors);
            return authorsDTO;
        }
    }

}
