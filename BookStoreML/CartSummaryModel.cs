using BookStoreRL.Models;

public class CartSummaryModel
{
    public IEnumerable<BookDetailModel> Books { get; set; } = Enumerable.Empty<BookDetailModel>();
    public decimal TotalCartPrice { get; set; }
}