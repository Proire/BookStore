using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreRL.CQRS.Commands.UserCommands
{
    public class ResetPasswordCommand : IRequest
    {
        public int UserId { get; set; }
        public string NewPassword { get; set; }

        public ResetPasswordCommand(int userId, string newPassword)
        {
            UserId = userId;
            NewPassword = newPassword;
        }
    }
}
