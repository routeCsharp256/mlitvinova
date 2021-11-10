using System.Collections.Generic;
using MediatR;
using OzonEdu.MerchandiseService.Domain.AggregationModels.MerchPackRequestAggregate;

namespace OzonEdu.MerchandiseService.Infrastructure.Commands.CreateMerchRequest
{
    public class CreateMerchRequestEntryCommand : IRequest
    {
        public MerchPackRequest MerchPackRequest { get; init; }
    }
}