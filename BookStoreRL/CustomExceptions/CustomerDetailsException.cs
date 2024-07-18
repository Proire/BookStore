using System;

namespace BookStoreRL.Exceptions
{
    public class CustomerDetailsException : Exception
    {
        public CustomerDetailsException()
        {
        }

        public CustomerDetailsException(string message)
            : base(message)
        {
        }

        public CustomerDetailsException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
