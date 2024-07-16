using BookStoreML;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreRL.CQRS.Commands.UserCommands
{
    public class LoginUserCommand : IRequest<string>
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        public LoginUserCommand(string userName, string password)
        {
            UserName = userName;
            Password = password;
        }
    }
}
