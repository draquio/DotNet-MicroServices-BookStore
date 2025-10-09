using AuthorService.Models;
using AuthorService.Persistence;
using FluentValidation;
using MediatR;
namespace AuthorService.Application.Author.Commands
{
    public record CreateAuthorCommand(string Name, string Lastname, DateOnly? Birthdate) : IRequest<Models.Author>;
    public sealed class CreateAuthorCommandValidator : AbstractValidator<CreateAuthorCommand>
    {
        public CreateAuthorCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required").MaximumLength(100);
            RuleFor(x => x.Lastname).NotEmpty().WithMessage("Lastname is required").MaximumLength(100);
        }
    }
    public class CreateAuthorCommandHandler : IRequestHandler<CreateAuthorCommand, Models.Author>
    {
        private readonly AuthorContext _dbContext;

        public CreateAuthorCommandHandler(AuthorContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Models.Author> Handle(CreateAuthorCommand request, CancellationToken cancellationToken)
        {
            var author = new Models.Author()
            {
                Name = request.Name,
                Lastname = request.Lastname,
                Birthdate = request.Birthdate,
                AuthorGuid = Guid.NewGuid()
            };
            _dbContext.Authors.Add(author);
            await _dbContext.SaveChangesAsync();
            return author;
        }
    }
}
