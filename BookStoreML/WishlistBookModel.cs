
namespace BookStoreRL.Models
{
    public class WishlistBookModel
    {
        public int BookId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int QuantityToPurchase { get; set; }
    }
}
