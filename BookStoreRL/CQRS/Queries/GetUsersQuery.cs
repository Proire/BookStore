using BookStoreML;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreRL.CQRS.Queries
{
    public class GetUsersQuery : IRequest<IEnumerable<User>>
    {
    }
}
