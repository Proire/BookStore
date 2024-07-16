using BookStoreML;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStoreRL.Entity
{
    public class Cart
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("UserId")]
        public int UserId { get; set; } // Foreign key for User
        
        public User User { get; set; } // Navigation property for user

        public List<CartItem> CartItems { get; set; } = new List<CartItem>(); // Navigation property for cart items

        public bool IsPurchased { get; set; } = false;
    }
}
