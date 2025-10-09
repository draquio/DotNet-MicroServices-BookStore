namespace BookService.Application.DTO.Books
{
    public class BookDTO
    {
        public string Title { get; set; }
        public string? PublishDate { get; set; }
        public Guid? BookGuid { get; set; }
        public Guid? AuthorGuid { get; set; }
    }
}
