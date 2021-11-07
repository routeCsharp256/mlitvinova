using System;

namespace OzonEdu.MerchandiseService.Domain.Exceptions
{
    public class InvalidConstraintType : Exception
    {
        public InvalidConstraintType(string message) : base(message)
        {
        }

        public InvalidConstraintType(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}