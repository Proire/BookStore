using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Primitives;
using System.Reflection.Metadata;
using BookStoreML;

namespace BookStoreRL.Entity
{
    public class CustomerDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } 

        public string AddressType { get; set; } = string.Empty;

        public string FullAddress { get; set; } = string.Empty;

        public string City { get; set; } = string.Empty;

        public string Country { get; set; } = string.Empty;

        public string Zipcode { get; set; } = string.Empty;

        public string State { get; set; } = string.Empty;

        public int UserId { get; set; } 

        public User User { get; set; }
    }
}
