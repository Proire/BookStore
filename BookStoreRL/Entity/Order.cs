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

        public int CartId {  get; set; }    


        public decimal TotalCartPrice { get; set; } 

        public DateTime OrderPlacedDateTime { get; set; } = DateTime.UtcNow;

        public int CustomerDetailId {  get; set; }    
        public CustomerDetail CustomerDetail { get; set; }  
    }
}
