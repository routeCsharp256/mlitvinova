using System;

namespace OzonEdu.MerchandiseService.Infrastructure.Exceptions
{
    public class MerchAlreadyIssuedException : Exception
    {
        public MerchAlreadyIssuedException(string message) : base(message)
        {
        }

        public MerchAlreadyIssuedException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}