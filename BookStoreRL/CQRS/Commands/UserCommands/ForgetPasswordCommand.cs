using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreRL.CQRS.Commands.UserCommands
{
    public class ForgetPasswordCommand : IRequest<string>
    {
        public string Email { get; set; }

        public ForgetPasswordCommand(string email)
        {
            Email = email;
        }
    }
}
