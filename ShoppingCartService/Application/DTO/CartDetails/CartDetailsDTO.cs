namespace ShoppingCartService.Application.DTO.CartDetails
{
    public class CartDetailsDTO
    {
        public int Id { get; set; }
        public string CreatedAt { get; set; }
        public string? TitleBook { get; set; }
        public Guid BookGuid { get; set; }
        public string? AuthorBook { get; set; }
        public string? PublishDate { get; set; }
    }
}
