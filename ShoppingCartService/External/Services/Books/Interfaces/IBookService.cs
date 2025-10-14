using ShoppingCartService.External.DTOs.Books;

namespace ShoppingCartService.External.Services.Books.Interfaces
{
    public interface IBookService
    {
        Task<(bool result, BookDTO? book, string? error)> GetBook(Guid guid);
    }
}
