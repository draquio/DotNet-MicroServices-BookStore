using AuthorService.Models;

namespace AuthorService.Application.DTO.Author
{
    public class AuthorDTO
    {
        public string Name { get; set; } = null!;
        public string Lastname { get; set; } = null!;
        public string? Birthdate { get; set; }
        public Guid? AuthorGuid { get; set; }
    }
}
