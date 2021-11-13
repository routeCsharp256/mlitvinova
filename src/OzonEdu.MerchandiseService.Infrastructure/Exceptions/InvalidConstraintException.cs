using System;

namespace OzonEdu.MerchandiseService.Infrastructure.Exceptions
{
    public class InvalidConstraintException : Exception
    {
        public InvalidConstraintException(string message) : base(message)
        {
        }

        public InvalidConstraintException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}