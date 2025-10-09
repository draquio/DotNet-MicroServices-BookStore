using Microsoft.EntityFrameworkCore;
using ShoppingCartService.Models;

namespace ShoppingCartService.Persistence
{
    public class CartContext : DbContext
    {
        public CartContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartDetail> CartDetails { get; set; }
    }
}
