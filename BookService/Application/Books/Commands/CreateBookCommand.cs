using BookService.Models;
using BookService.Persistence;
using FluentValidation;
using MediatR;

namespace BookService.Application.Books.Commands
{
    public record CreateBookCommand(string Title, DateOnly? PublishDate, Guid? AuthorGuid) : IRequest<Book>;

    public sealed class CreateBookCommandValidator : AbstractValidator<CreateBookCommand>
    {
        public CreateBookCommandValidator()
        {
            RuleFor(x => x.Title).NotEmpty().MaximumLength(200);
            RuleFor(x => x.PublishDate).LessThanOrEqualTo(DateOnly.FromDateTime(DateTime.Now)).When(x => x.PublishDate.HasValue);
            RuleFor(x => x.AuthorGuid).NotEmpty().When(x => x.AuthorGuid.HasValue);
        }
    }
    public class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, Book>
    {
        private readonly BookContext _dbContext;

        public CreateBookCommandHandler(BookContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Book> Handle(CreateBookCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var book = new Book
                {
                    Title = request.Title,
                    PublishDate = request.PublishDate,
                    AuthorGuid = request.AuthorGuid,
                    BookGuid = Guid.NewGuid()
                };
                _dbContext.Books.Add(book);
                await _dbContext.SaveChangesAsync();
                return book;
            }
            catch (Exception)
            {
                throw;
            }

        }
    }
}
