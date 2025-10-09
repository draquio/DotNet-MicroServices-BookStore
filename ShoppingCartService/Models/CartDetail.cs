namespace ShoppingCartService.Models
{
    public class CartDetail
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid BookGuid { get; set; }
        public int CartId { get; set; }
        public Cart Cart { get; set; }
    }
}
