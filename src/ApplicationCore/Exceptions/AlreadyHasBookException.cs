using System;

namespace ApplicationCore.Exceptions
{
    public class AlreadyHasBookException : Exception
    {
        public AlreadyHasBookException(string title)
            : base($"You already have book \"{title}\"")
        {
        }
    }
}