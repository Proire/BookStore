using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStoreML;

namespace BookStoreRL.Entity
{
    public class Order
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int BookId { get; set; }

        public string BookTitle {  get; set; }

        public int Quantity { get; set; }   

        public decimal TotalPrice { get; set; } 

        public int UserId {  get; set; }    
        public User User { get; set; }  
    }
}
