namespace BookService.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateOnly? PublishDate { get; set; }
        public Guid? BookGuid { get; set; }
        public Guid? AuthorGuid { get; set; }

    }
}
