using BookService.Models;
using Microsoft.EntityFrameworkCore;

namespace BookService.Persistence
{
    public class BookContext : DbContext
    {
        public BookContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Book> Books { get; set; }
    }
}
