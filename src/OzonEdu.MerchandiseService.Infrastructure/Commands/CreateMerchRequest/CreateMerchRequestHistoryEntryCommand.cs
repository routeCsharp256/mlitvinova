﻿using System;
using System.Collections.Generic;
using MediatR;

namespace OzonEdu.MerchandiseService.Infrastructure.Commands.CreateMerchRequest
{
    public class CreateMerchRequestHistoryEntryCommand : IRequest
    {
        public int EmployeeId { get; init; }
        
        public string MerchPackName { get; init; }
        
        public List<long> Sku { get; init; }
        
        public DateTime CompletedAt { get; init; }
    }
}