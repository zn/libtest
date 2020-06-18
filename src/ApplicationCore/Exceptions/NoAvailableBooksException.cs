using System;

namespace ApplicationCore.Exceptions
{
    public class NoAvailableBooksException : Exception
    {
        public NoAvailableBooksException(string title)
            : base($"Book \"{title}\" is not available now.")
        {
        }
    }
}