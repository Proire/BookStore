using BookStoreRL.Entity;
using MediatR;
using System;

namespace BookStoreRL.CQRS.Commands.BookCommands
{
    public class CreateBookCommand : IRequest<Book>
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string ISBN { get; set; }
        public string Publisher { get; set; }
        public DateTime PublishedDate { get; set; }
        public string Genre { get; set; }
        public string Language { get; set; }
        public int Pages { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string CoverImageUrl { get; set; }
        public int StockQuantity { get; set; }
        public double Rating { get; set; }

        public CreateBookCommand(
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
