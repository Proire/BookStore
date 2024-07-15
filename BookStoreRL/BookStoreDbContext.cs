using BookStoreML;
using BookStoreRL.Entity;
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

        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasIndex(u => new { u.UserName, u.Role })
                .IsUnique();

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Phonenumber)
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

        }
    }
}
