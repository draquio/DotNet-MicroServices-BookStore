using System.Text.Json;
using ShoppingCartService.External.DTOs.Books;
using ShoppingCartService.External.Services.Books.Interfaces;

namespace ShoppingCartService.External.Services.Books
{
    public class BookService : IBookService
    {
        private readonly HttpClient _http;
        private readonly ILogger<BookService> _logger;

        public BookService(HttpClient http, ILogger<BookService> logger)
        {
            _http = http;
            _logger = logger;
        }

        public async Task<(bool result, BookDTO? book, string? error)> GetBook(Guid guid)
        {
            try
            {
                var response = await _http.GetAsync($"api/Book/{guid}");
                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogWarning("Error with {BookId}. Status: {StatusCode}", guid, response.StatusCode);
                    return (false, null, response.ReasonPhrase);
                }
                var bookDTO = await response.Content.ReadFromJsonAsync<BookDTO>(
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                if (bookDTO is null)
                {
                    _logger.LogWarning("Book {BookId} retrieves empty data", guid);
                    return (false, null, "Book not found");
                }

                return (true, bookDTO, null);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving Book {BookId}", guid);
                return (false, null, ex.Message);
            }
        }
    }
}
