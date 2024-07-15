using BookStoreML;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreRL.CQRS.Commands.UserCommands
{
    public class InsertUserCommand : IRequest<User>
    {
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string Phonenumber { get; set; }
        public DateTime DateOfBirth { get; set; }

        public InsertUserCommand(string fullName, string userName, string email, string password, string role, string phonenumber, DateTime dateOfBirth)
        {
            FullName = fullName;
            UserName = userName;
            Email = email;
            Password = password;
            Role = role;
            Phonenumber = phonenumber;
            DateOfBirth = dateOfBirth;
        }
    }
}