using System.Collections.Generic;
using MediatR;

namespace OzonEdu.MerchandiseService.Infrastructure.Commands.CreateMerchRequest
{
    public class CreateMerchRequestCommand : IRequest
    {
        public long EmployeeId { get; init; }
        
        public string MerchPackName { get; init; }
        
        public Dictionary<string, string> Restrainments { get; init; }
    }
}