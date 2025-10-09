using AuthorService.Models;
using Microsoft.EntityFrameworkCore;

namespace AuthorService.Persistence
{
    public class AuthorContext : DbContext
    {
        public AuthorContext(DbContextOptions<AuthorContext> options) : base(options)
        {
        }
        public DbSet<Author> Authors { get; set; }
        public DbSet<AcademicDegree> AcademicDegrees { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>()
                .HasMany(b => b.AcademicDegrees)
                .WithOne(a => a.Author)
                .HasForeignKey(a => a.AuthorId);
            base.OnModelCreating(modelBuilder);
        }
    }
}
