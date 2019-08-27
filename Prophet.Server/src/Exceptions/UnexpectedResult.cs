using System;

namespace Prophet.Exceptions
{
    public class UnexpectedResult : Exception
    {
        public UnexpectedResult(string operationName)
            : base($"Unexpected result of: {operationName}") { }
    }
}
