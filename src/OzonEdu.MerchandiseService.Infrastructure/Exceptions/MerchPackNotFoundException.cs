using System;

namespace OzonEdu.MerchandiseService.Infrastructure.Exceptions
{
    public class MerchPackNotFoundException : Exception
    {
        public MerchPackNotFoundException(string message) : base(message)
        {
        }

        public MerchPackNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}