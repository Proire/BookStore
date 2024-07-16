namespace BookStoreRL.Models
{
    public class BookDetailModel
    {
        public int BookId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int QuantityToPurchase { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
