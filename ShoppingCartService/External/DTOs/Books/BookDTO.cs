namespace ShoppingCartService.External.DTOs.Books
{
    public class BookDTO
    {
        public Guid? BookGuid { get; set; }
        public string Title { get; set; }
        public string? PublishDate { get; set; }
        public Guid? AuthorGuid { get; set; }
    }
}
