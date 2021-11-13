using System;

namespace OzonEdu.MerchandiseService.Domain.Exceptions
{
    public class MerchRequestHistoryEntryStatusException : Exception
    {
        public MerchRequestHistoryEntryStatusException(string message) : base(message)
        {
        }
        
        public MerchRequestHistoryEntryStatusException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}