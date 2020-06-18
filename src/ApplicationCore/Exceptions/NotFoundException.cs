using System;

namespace ApplicationCore.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string what)
            : base($"{what} not found!")
        {
        }
    }
}