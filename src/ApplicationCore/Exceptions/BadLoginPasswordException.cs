using System;

namespace ApplicationCore.Exceptions
{
    public class BadLoginPasswordException : Exception
    {
        public BadLoginPasswordException()
            : base($"Account with such login and password combination found!")
        {
        }
    }
}