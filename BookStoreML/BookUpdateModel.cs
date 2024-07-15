using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BookStoreML
{
    public class BookUpdateModel
    {
        [Required(ErrorMessage = "Title is required.")]
        [StringLength(200, ErrorMessage = "Title cannot be longer than 200 characters.")]
        [DefaultValue("Harry Potter and the Cursed Child")]
        public string Title { get; set; } = "Harry Potter and the Cursed Child";

        [Required(ErrorMessage = "Author is required.")]
        [StringLength(100, ErrorMessage = "Author cannot be longer than 100 characters.")]
        [DefaultValue("J.K. Rowling, John Tiffany, and Jack Thorne")]
        public string Author { get; set; } = "J.K. Rowling, John Tiffany, and Jack Thorne";

        [Required(ErrorMessage = "ISBN is required.")]
        [StringLength(13, ErrorMessage = "ISBN must be 13 characters long.", MinimumLength = 13)]
        [DefaultValue("9781338216660")]
        public string ISBN { get; set; } = "9781338216660";

        [Required(ErrorMessage = "Publisher is required.")]
        [StringLength(100, ErrorMessage = "Publisher cannot be longer than 100 characters.")]
        [DefaultValue("Arthur A. Levine Books")]
        public string Publisher { get; set; } = "Arthur A. Levine Books";

        [Required(ErrorMessage = "Published Date is required.")]
        [DataType(DataType.Date, ErrorMessage = "Published Date must be a valid date.")]
        [DefaultValue("2016-07-31")]
        public DateTime PublishedDate { get; set; } = new DateTime(2016, 7, 31);

        [Required(ErrorMessage = "Genre is required.")]
        [StringLength(50, ErrorMessage = "Genre cannot be longer than 50 characters.")]
        [DefaultValue("Fantasy")]
        public string Genre { get; set; } = "Fantasy";

        [Required(ErrorMessage = "Language is required.")]
        [StringLength(50, ErrorMessage = "Language cannot be longer than 50 characters.")]
        [DefaultValue("English")]
        public string Language { get; set; } = "English";

        [Range(1, int.MaxValue, ErrorMessage = "Pages must be a positive number.")]
        [DefaultValue(336)]
        public int Pages { get; set; } = 336;

        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than zero.")]
        [DefaultValue(29.99)]
        public decimal Price { get; set; } = 29.99m;

        [StringLength(1000, ErrorMessage = "Description cannot be longer than 1000 characters.")]
        [DefaultValue("The eighth story, nineteen years later. A new play by J.K. Rowling, John Tiffany, and Jack Thorne.")]
        public string Description { get; set; } = "The eighth story, nineteen years later. A new play by J.K. Rowling, John Tiffany, and Jack Thorne.";

        [Url(ErrorMessage = "Cover Image URL must be a valid URL.")]
        [DefaultValue("https://example.com/harry-potter-cursed-child.jpg")]
        public string CoverImageUrl { get; set; } = "https://example.com/harry-potter-cursed-child.jpg";

        [Range(0, int.MaxValue, ErrorMessage = "Stock Quantity must be a non-negative number.")]
        [DefaultValue(50)]
        public int StockQuantity { get; set; } = 50;

        [Range(0.0, 5.0, ErrorMessage = "Rating must be between 0 and 5.")]
        [DefaultValue(4.5)]
        public double Rating { get; set; } = 4.5;
    }
}
