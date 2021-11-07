using System;

namespace OzonEdu.MerchandiseService.Domain.Exceptions
{
    public class UnableToFormMerchRequestException : Exception
    {
        public UnableToFormMerchRequestException(string message) : base(message)
        {
        }
        
        public UnableToFormMerchRequestException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}