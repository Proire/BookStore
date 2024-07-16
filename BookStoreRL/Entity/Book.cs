using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreRL.Entity
{
    public class Book
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public string ISBN { get; set; } = string.Empty;
        public string Publisher { get; set; } = string.Empty;
        public DateTime PublishedDate { get; set; } = DateTime.MinValue;
        public string Genre { get; set; } = string.Empty;
        public string Language { get; set; } = string.Empty;
        public int Pages { get; set; } 
        public decimal Price { get; set; }
        public string Description { get; set; } = string.Empty ;
        public string CoverImageUrl { get; set; } = string.Empty;   
        public int StockQuantity { get; set; }
        public double Rating { get; set; }
    }

}
