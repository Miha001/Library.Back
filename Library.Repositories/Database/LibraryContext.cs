using Library.Repositories.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace Library.Repositories.Database
{
    public class LibraryContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<FavoriteBook> FavoriteBooks { get; set; }
        public DbSet<AuthorizationToken> AuthorizationTokens { get; set; }

        public LibraryContext(DbContextOptions<LibraryContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(u => u.FavoriteBooks)
                .WithOne(fb => fb.User)
                .HasForeignKey(fb => fb.UserId);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Comments)
                .WithOne(c => c.User)
                .HasForeignKey(c => c.UserId);

            modelBuilder.Entity<Book>()
                .HasMany(b => b.FavoriteBooks)
                .WithOne(fb => fb.Book)
                .HasForeignKey(fb => fb.BookId);

            modelBuilder.Entity<Book>()
                .HasMany(b => b.Comments)
                .WithOne(c => c.Book)
                .HasForeignKey(c => c.BookId);

            modelBuilder.Entity<FavoriteBook>()
                .HasKey(fb => new { fb.UserId, fb.BookId });

            base.OnModelCreating(modelBuilder);
        }
    }
}
