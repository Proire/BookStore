using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreML
{
    public class OrderModel
    {
        public int CartId { get; set; } 
        public decimal TotalCartPrice { get; set; }
        public int CustomerDetailsId { get; set; }
    }
}
