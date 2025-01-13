﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManager.Common.Exceptions
{
    public class DatabaseException : Exception
    {
        public DatabaseException() { }

        public DatabaseException(string message) : base(message) { }

        public DatabaseException(string message, Exception ex) : base(message, ex) { }
    }
}
