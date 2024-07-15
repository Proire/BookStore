using System;
using System.ComponentModel.DataAnnotations;

namespace BookStoreML
{
    public class BookUpdateModel
    {
        [Required(ErrorMessage = "Title is required.")]
        [StringLength(200, ErrorMessage = "Title cannot be longer than 200 characters.")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Author is required.")]
        [StringLength(100, ErrorMessage = "Author cannot be longer than 100 characters.")]
        public string Author { get; set; } = string.Empty;

        [Required(ErrorMessage = "ISBN is required.")]
        [StringLength(13, ErrorMessage = "ISBN must be 13 characters long.", MinimumLength = 13)]
        public string ISBN { get; set; } = string.Empty;

        [Required(ErrorMessage = "Publisher is required.")]
        [StringLength(100, ErrorMessage = "Publisher cannot be longer than 100 characters.")]
        public string Publisher { get; set; } = string.Empty;

        [Required(ErrorMessage = "Published Date is required.")]
        [DataType(DataType.Date, ErrorMessage = "Published Date must be a valid date.")]
        public DateTime PublishedDate { get; set; } = DateTime.MinValue;

        [Required(ErrorMessage = "Genre is required.")]
        [StringLength(50, ErrorMessage = "Genre cannot be longer than 50 characters.")]
        public string Genre { get; set; } = string.Empty;

        [Required(ErrorMessage = "Language is required.")]
        [StringLength(50, ErrorMessage = "Language cannot be longer than 50 characters.")]
        public string Language { get; set; } = string.Empty;

        [Range(1, int.MaxValue, ErrorMessage = "Pages must be a positive number.")]
        public int Pages { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than zero.")]
        public decimal Price { get; set; }

        [StringLength(1000, ErrorMessage = "Description cannot be longer than 1000 characters.")]
        public string Description { get; set; } = string.Empty;

        [Url(ErrorMessage = "Cover Image URL must be a valid URL.")]
        public string CoverImageUrl { get; set; } = string.Empty;

        [Range(0, int.MaxValue, ErrorMessage = "Stock Quantity must be a non-negative number.")]
        public int StockQuantity { get; set; }

        [Range(0.0, 5.0, ErrorMessage = "Rating must be between 0 and 5.")]
        public double Rating { get; set; }
    }
}
