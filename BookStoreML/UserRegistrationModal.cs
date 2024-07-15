using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace BookStoreML
{
    public class UserRegistrationModel
    {
        [Required(ErrorMessage = "Full Name is required.")]
        [RegularExpression(@"^[A-Z][a-zA-Z\s]*$", ErrorMessage = "Full Name must start with an uppercase letter and contain only letters and spaces.")]
        [DefaultValue("John Doe")]
        public string FullName { get; set; } = "John Doe";

        [Required(ErrorMessage = "Username is required.")]
        [RegularExpression(@"^[a-zA-Z0-9_]{5,20}$", ErrorMessage = "Username must be 5-20 characters long and contain only letters, numbers, or underscores.")]
        [DefaultValue("johndoe")]
        public string UserName { get; set; } = "johndoe";

        [Required(ErrorMessage = "Email is required.")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid email address format.")]
        [DefaultValue("john.doe@example.com")]
        public string Email { get; set; } = "john.doe@example.com";

        [Required(ErrorMessage = "Password is required.")]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$", ErrorMessage = "Password must be at least 8 characters long, contain at least one uppercase letter, one number, and one special character.")]
        [DefaultValue("Password123!")]
        public string Password { get; set; } = "Password123!";

        [Required(ErrorMessage = "Date of Birth is required.")]
        [DefaultValue("2000-01-01")]
        public DateTime DateOfBirth { get; set; } = new DateTime(2000, 1, 1);

        [RegularExpression(@"^\+\d{1,3}\s?\d{7,15}$", ErrorMessage = "Phone number must be in the format: +<country code> <number> (e.g., +1 1234567890).")]
        [DefaultValue("+123 1234567890")]
        public string Phonenumber { get; set; } = "+123 1234567890";
    }

}
