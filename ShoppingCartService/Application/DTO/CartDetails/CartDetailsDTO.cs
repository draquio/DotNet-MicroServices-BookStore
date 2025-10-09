namespace ShoppingCartService.Application.DTO.CartDetails
{
    public class CartDetailsDTO
    {
        public int Id { get; set; }
        public string CreatedAt { get; set; }
        public Guid BookGuid { get; set; }
    }
}
