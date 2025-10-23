using ShoppingCartService.External.DTOs.Authors;

namespace ShoppingCartService.External.Services.Author.Interfaces
{
    public interface IAuthorService
    {
        Task<(bool result, AuthorDTO? author, string? error)> GetAuthor(Guid guid);
    }
}
