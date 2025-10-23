using System.Text.Json;
using ShoppingCartService.External.DTOs.Authors;
using ShoppingCartService.External.Services.Author.Interfaces;

namespace ShoppingCartService.External.Services.Author
{
    public class AuthorService : IAuthorService
    {
        private readonly HttpClient _http;
        private readonly ILogger<AuthorService> _logger;

        public AuthorService(HttpClient http, ILogger<AuthorService> logger)
        {
            _http = http;
            _logger = logger;
        }

        public async Task<(bool result, AuthorDTO? author, string? error)> GetAuthor(Guid guid)
        {
            try
            {
                var response = await _http.GetAsync($"api/Author/{guid}");
                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogWarning("Error with {AuthirId}. Status: {StatusCode}", guid, response.StatusCode);
                    return (false, null, response.ReasonPhrase);
                }
                var authorDTO = await response.Content.ReadFromJsonAsync<AuthorDTO>(
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                if (authorDTO is null)
                {
                    _logger.LogWarning("Author {AuthorId} retrieves empty data", guid);
                    return (false, null, "Author not found");
                }

                return (true, authorDTO, null);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving Author {AuthorId}", guid);
                return (false, null, ex.Message);
            }
        }
    }
}
