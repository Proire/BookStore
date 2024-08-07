﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreRL.CustomExceptions
{
    public class BookException : Exception
    {
        public BookException()
        {
        }

        public BookException(string message)
            : base(message)
        {
        }

        public BookException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
