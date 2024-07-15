using BookStoreRL.Entity;
using MediatR;
using System;

namespace BookStoreRL.CQRS.Commands.BookCommands
{
    public class UpdateBookCommand : IRequest<Book>
    {
        public int BookId { get; }
        public string Title { get; }
        public string Author { get; }
        public string ISBN { get; }
        public string Publisher { get; }
        public DateTime PublishedDate { get; }
        public string Genre { get; }
        public string Language { get; }
        public int Pages { get; }
        public decimal Price { get; }
        public string Description { get; }
        public string CoverImageUrl { get; }
        public int StockQuantity { get; }
        public double Rating { get; }

        public UpdateBookCommand(
            int bookId,
            string title,
            string author,
            string isbn,
            string publisher,
            DateTime publishedDate,
            string genre,
            string language,
            int pages,
            decimal price,
            string description,
            string coverImageUrl,
            int stockQuantity,
            double rating)
        {
            BookId = bookId;
            Title = title;
            Author = author;
            ISBN = isbn;
            Publisher = publisher;
            PublishedDate = publishedDate;
            Genre = genre;
            Language = language;
            Pages = pages;
            Price = price;
            Description = description;
            CoverImageUrl = coverImageUrl;
            StockQuantity = stockQuantity;
            Rating = rating;
        }
    }
}
