using ShoppingCartService.Application.DTO.CartDetails;
using ShoppingCartService.Models;

namespace ShoppingCartService.Application.DTO.Carts
{
    public class CartDTO
    {
        public int Id { get; set; }
        public string CreatedAt { get; set; }
        public ICollection<CartDetailsDTO> CartDetails { get; set; } = new List<CartDetailsDTO>();
    }
}
