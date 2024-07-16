using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreRL.CustomExceptions
{ 
    namespace BookStoreRL.Exceptions
    {
        public class CartException : Exception
        {
            public CartException() { }

            public CartException(string message)
                : base(message) { }

            public CartException(string message, Exception innerException)
                : base(message, innerException) { }
        }
    }

}
