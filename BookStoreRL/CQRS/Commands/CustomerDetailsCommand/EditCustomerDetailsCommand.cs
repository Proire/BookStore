using MediatR;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BookStoreRL.Commands
{
    public class EditCustomerDetailsCommand : IRequest
    {
        public string AddressType { get; set; } = string.Empty;

        public string FullAddress { get; set; } = string.Empty;

        public string City { get; set; } = string.Empty;

        public string Country { get; set; } = string.Empty;

        public string Zipcode { get; set; } = string.Empty;

        public string State { get; set; } = string.Empty;

        public int UserId { get; set; }    

        public EditCustomerDetailsCommand(string addressType, string fullAddress, string city, string country, string zipcode, string state, int userId)
        {
            AddressType = addressType;
            FullAddress = fullAddress;
            City = city;
            Country = country;
            Zipcode = zipcode;
            State = state;
            UserId = userId;
        }
    }
}
