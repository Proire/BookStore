using System;

namespace BookStoreRL.CustomExceptions
{
    public class WishlistException : Exception
    {
        public WishlistException()
        {
        }

        public WishlistException(string message)
            : base(message)
        {
        }

        public WishlistException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
