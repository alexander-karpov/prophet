using System;

namespace Prophet.Exceptions
{
    public class AlreadyExistsException : Exception
    {
        public AlreadyExistsException(string msg) : base(msg) { }
    }
}
