using System;

namespace OzonEdu.MerchandiseService.Infrastructure.Exceptions
{
    public class MerchPackNotPreparedException : Exception
    {
        public MerchPackNotPreparedException(string message) : base(message)
        {
        }

        public MerchPackNotPreparedException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}