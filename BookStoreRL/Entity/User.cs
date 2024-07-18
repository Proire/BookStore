using BookStoreRL.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStoreRL
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } 
        
        public string FullName { get; set; }  = string.Empty;

        public string UserName { get; set; } = string.Empty;

        public string Email { get; set; }   = string.Empty ;

        public string Password { get; set; } = string.Empty;

        public string Role { get; set; } = string.Empty;

        public string Phonenumber { get; set; } = string.Empty;

        public DateTime DateOfBirth { get; set; } = new DateTime();

        public Cart? Cart { get; set; }

        public ICollection<CustomerDetails>? CustomerDetails { get; set; }

        public ICollection<Order> Orders { get; set; }  
    }
}
