namespace ShoppingCartService.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public ICollection<CartDetail> CartDetails { get; set; } = new List<CartDetail>();
    }
}
