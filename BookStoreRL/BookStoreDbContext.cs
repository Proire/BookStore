using BookStore.Models;
using BookStoreML;
using BookStoreRL.Entity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreRL
{
    public class UserDbContext: DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Wishlist> Wishlists { get; set; }

        public DbSet<CustomerDetails> CustomerDetails { get; set; }

        public DbSet<Order> Orders { get; set; }

        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
        {
        }

        public async Task<Book> GetBookByIdAsync(int bookId)
        {
            var bookIdParam = new SqlParameter("@BookId", bookId);
            var books = await Books.FromSqlRaw("EXEC GetBookById @BookId", bookIdParam).ToListAsync();
            return books.FirstOrDefault();
        }

        public async Task<List<Book>> GetAllBooksAsync()
        {
            var books = await Books.FromSqlRaw("EXEC GetAllBooks").ToListAsync();
            return books;
        }
        public async Task AddOrderAsync(Order order)
        {
            // Define the parameters for the stored procedure
            var bookIdParam = new SqlParameter("@BookId", order.BookId);
            var bookTitleParam = new SqlParameter("@BookTitle", order.BookTitle);
            var quantityParam = new SqlParameter("@Quantity", order.Quantity);
            var totalPriceParam = new SqlParameter("@TotalPrice", order.TotalPrice);
            var userIdParam = new SqlParameter("@UserId", order.UserId);

            // Execute the stored procedure
            await Database.ExecuteSqlRawAsync(
                "EXEC AddOrder @BookId, @BookTitle, @Quantity, @TotalPrice, @UserId",
                bookIdParam, bookTitleParam, quantityParam, totalPriceParam, userIdParam
            );
        }

        public async Task<List<Wishlist>> GetWishlistItemsByUserIdAsync(int userId)
        {
            var userIdParam = new SqlParameter("@UserId", userId);
            return await Wishlists
                .FromSqlRaw("EXEC GetWishlistItemsByUserId @UserId", userIdParam)
                .ToListAsync();
        }

        // Method to get books by a list of IDs using stored procedure
        public async Task<List<Book>> GetBooksByIdsAsync(string bookIds)
        {
            var bookIdsParam = new SqlParameter("@BookIds", bookIds);
            return await Books
                .FromSqlRaw("EXEC GetBooksByIds @BookIds", bookIdsParam)
                .ToListAsync();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasIndex(u => u.UserName)
                .IsUnique();

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Phonenumber)
                .IsUnique();

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<Book>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Title).IsRequired().HasMaxLength(255);
                entity.Property(e => e.Author).IsRequired().HasMaxLength(255);
                entity.Property(e => e.ISBN).IsRequired().HasMaxLength(13);
                entity.Property(e => e.Publisher).HasMaxLength(255);
                entity.Property(e => e.PublishedDate).HasColumnType("date");
                entity.Property(e => e.Genre).HasMaxLength(100);
                entity.Property(e => e.Language).HasMaxLength(100);
                entity.Property(e => e.Pages).IsRequired();
                entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");
                entity.Property(e => e.Description).HasColumnType("text");
                entity.Property(e => e.CoverImageUrl).HasMaxLength(255);
                entity.Property(e => e.StockQuantity).IsRequired();
                entity.Property(e => e.Rating).HasColumnType("float");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.TotalPrice).HasColumnType("decimal(18, 2)");
            });

            // Configure one-to-one relationship between User and Cart
            modelBuilder.Entity<User>()
                .HasOne(u => u.Cart)
                .WithOne(c => c.User)
                .HasForeignKey<Cart>(c => c.UserId)
                .IsRequired();

            // Configure one-to-many relationship between Cart and CartItem
            modelBuilder.Entity<Cart>()
                .HasMany(c => c.CartItems)
                .WithOne(ci => ci.Cart)
                .HasForeignKey(ci => ci.CartId)
                .IsRequired();

            modelBuilder.Entity<User>()
               .HasMany(u => u.CustomerDetails)
               .WithOne(cd => cd.User)
               .HasForeignKey(cd => cd.UserId)
               .IsRequired();

            // Configure one-to-many relationship between User and Order
            modelBuilder.Entity<User>()
                .HasMany(u => u.Orders)
                .WithOne(o => o.User)
                .HasForeignKey(o => o.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
