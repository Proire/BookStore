using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BookStoreML
{
    public class BookRegistrationModel
    {
        [Required(ErrorMessage = "Title is required.")]
        [StringLength(200, ErrorMessage = "Title cannot be longer than 200 characters.")]
        [DefaultValue("The Old Man and the Sea")]
        public string Title { get; set; } = "The Old Man and the Sea";

        [Required(ErrorMessage = "Author is required.")]
        [StringLength(100, ErrorMessage = "Author cannot be longer than 100 characters.")]
        [DefaultValue("Ernest Hemingway")]
        public string Author { get; set; } = "Ernest Hemingway";

        [Required(ErrorMessage = "ISBN is required.")]
        [StringLength(13, ErrorMessage = "ISBN must be 13 characters long.", MinimumLength = 13)]
        [DefaultValue("9780684801223")]
        public string ISBN { get; set; } = "9780684801223";

        [Required(ErrorMessage = "Publisher is required.")]
        [StringLength(100, ErrorMessage = "Publisher cannot be longer than 100 characters.")]
        [DefaultValue("Charles Scribner's Sons")]
        public string Publisher { get; set; } = "Charles Scribner's Sons";

        [Required(ErrorMessage = "Published Date is required.")]
        [DataType(DataType.Date, ErrorMessage = "Published Date must be a valid date.")]
        [DefaultValue("1952-09-01")]
        public DateTime PublishedDate { get; set; } = new DateTime(1952, 9, 1);

        [Required(ErrorMessage = "Genre is required.")]
        [StringLength(50, ErrorMessage = "Genre cannot be longer than 50 characters.")]
        [DefaultValue("Fiction")]
        public string Genre { get; set; } = "Fiction";

        [Required(ErrorMessage = "Language is required.")]
        [StringLength(50, ErrorMessage = "Language cannot be longer than 50 characters.")]
        [DefaultValue("English")]
        public string Language { get; set; } = "English";

        [Range(1, int.MaxValue, ErrorMessage = "Pages must be a positive number.")]
        [DefaultValue(127)]
        public int Pages { get; set; } = 127;

        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than zero.")]
        [DefaultValue(14.99)]
        public decimal Price { get; set; } = 14.99m;

        [StringLength(1000, ErrorMessage = "Description cannot be longer than 1000 characters.")]
        [DefaultValue("A novel about an aging fisherman who struggles with a giant marlin.")]
        public string Description { get; set; } = "A novel about an aging fisherman who struggles with a giant marlin.";

        [Url(ErrorMessage = "Cover Image URL must be a valid URL.")]
        [DefaultValue("https://example.com/cover.jpg")]
        public string CoverImageUrl { get; set; } = "https://example.com/cover.jpg";

        [Range(0, int.MaxValue, ErrorMessage = "Stock Quantity must be a non-negative number.")]
        [DefaultValue(100)]
        public int StockQuantity { get; set; } = 100;

        [Range(0.0, 5.0, ErrorMessage = "Rating must be between 0 and 5.")]
        [DefaultValue(4.5)]
        public double Rating { get; set; } = 4.5;
    }
}
