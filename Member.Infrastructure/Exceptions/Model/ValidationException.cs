using System;

namespace Member.Infrastructure.Exceptions.Model
{
	public class ValidationException : Exception
    {
        public ValidationException(string message)
            : base(message)
        { }
    }
}

