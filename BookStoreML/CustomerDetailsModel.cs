using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreML
{
    public class CustomerDetailsModel
    {
        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Address Type must be between 3 and 50 characters.")]
        [DefaultValue("Residential")] // Default value for Swagger and documentation
        public string AddressType { get; set; } = "Residential";

        [Required]
        [StringLength(200, MinimumLength = 5, ErrorMessage = "Full Address must be between 5 and 200 characters.")]
        [DefaultValue("456 Maple Avenue, Suite 789")] // Random example value
        public string FullAddress { get; set; } = "456 Maple Avenue, Suite 789";

        [Required]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "City must be between 2 and 100 characters.")]
        [DefaultValue("Springfield")] // Random example value
        public string City { get; set; } = "Springfield";

        [Required]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Country must be between 2 and 100 characters.")]
        [DefaultValue("United States")] // Random example value
        public string Country { get; set; } = "United States";

        [Required]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "Zipcode must be between 5 and 20 characters.")]
        [DefaultValue("62704")] // Random example value
        public string Zipcode { get; set; } = "62704";

        [Required]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "State must be between 2 and 100 characters.")]
        [DefaultValue("IL")] // Random example value
        public string State { get; set; } = "IL";
    }
}
