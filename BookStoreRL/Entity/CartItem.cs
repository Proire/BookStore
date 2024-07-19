using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStoreRL.Entity
{
    public class CartItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int CartId { get; set; } // Foreign key for Cart
        [ForeignKey("CartId")]
        public Cart Cart { get; set; } // Navigation property for cart

        public int BookId { get; set; } // ID of the book

        public Book Book { get; set; }
        public int QuantityToPurchase { get; set; }  // Quantity to purchase
    }
}
