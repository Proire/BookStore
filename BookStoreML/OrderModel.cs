﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreML
{
    public class OrderModel
    {

        public int BookId { get; set; }

        public string BookTitle { get; set; }

        public int Quantity { get; set; }

        public decimal TotalPrice { get; set; }
    }
}