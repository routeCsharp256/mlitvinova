using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OzonEdu.MerchandiseService.Domain.AggregationModels.EmployeeAggregate;
using OzonEdu.MerchandiseService.Domain.AggregationModels.MerchPackAggregate;
using OzonEdu.MerchandiseService.Domain.AggregationModels.MerchPackRequestAggregate;
using OzonEdu.MerchandiseService.Domain.Models;
using OzonEdu.MerchandiseService.Domain.Services.Interfaces;
using OzonEdu.MerchandiseService.HttpModels;

namespace OzonEdu.MerchandiseService.Controllers
{
    [ApiController]
    [Route("v1/api/merch")]
    [Produces("application/json")]
    public class MerchController : ControllerBase
    {
        private readonly IMerchandiseService _merchandiseService;
        private readonly IMediator _mediator;

        public MerchController(IMerchandiseService service, IMediator mediator)
        {
            _merchandiseService = service;
            _mediator = mediator;
        }

        [HttpGet("GetMerchIssuedToEmployee")]
        public async Task<ActionResult<GetMerchPackIssuedToEmployeeResponse>> GetMerchIssuedToEmployee(
            long employeeId, CancellationToken token)
        {
            var merchIssued = await _merchandiseService.GetIssuedMerchToEmployee(employeeId, token);

            var convertedMerchIssued = new GetMerchPackIssuedToEmployeeResponse()
            {
                MerchList = merchIssued.Select(x =>
                {
                    var status = x.Status switch
                    {
                        MerchPurchaseStatus.Issued => MerchPackStatus.Issued,
                        MerchPurchaseStatus.Issuing => MerchPackStatus.Issuing,
                        _ => throw new ArgumentOutOfRangeException()
                    };
                    
                    return new MerchandiseService.HttpModels.MerchPackInStatus()
                    {
                        MerchPackName = x.MerchPackName,
                        Status = status
                    };
                }).ToList()
            };
            
            return Ok(convertedMerchIssued);
        }
        
        [HttpPost("IssueMerchToEmployee")]
        public async Task<ActionResult<IssueMerchToEmployeeResponse>> IssueMerchToEmployee(
            long employeeId,
            string merchPackName, 
            CancellationToken token)
        {
            var status = await _merchandiseService.IssueMerchToEmployee(employeeId, merchPackName, token);
            var requestStatus = status switch
            {
                MerchIssueRequestStatus.EmployeeAlreadyHasSuchMerch => IssueMerchResponse.MerchAlreadyIssued,
                MerchIssueRequestStatus.NoSuchMerchExists => IssueMerchResponse.NoSuchMerch,
                MerchIssueRequestStatus.RequestCreated => IssueMerchResponse.Created,
                _ => IssueMerchResponse.Unknown
            };
            
            var result = new IssueMerchToEmployeeResponse()
            {
                IssueMerchResponse = requestStatus
            };
            
            return Ok(result);
        }

        [HttpGet("GetMerchPackContent")]
        public async Task<ActionResult<GetMerchPackDetailsResponse>> GetMerchPackContent(string merchPackName, CancellationToken token)
        {
            var merchPackDetails = await _merchandiseService.GetMerchPackContent(merchPackName, token);

            if (merchPackDetails is null)
            {
                return NotFound();
            }

            var convertedResult = new GetMerchPackDetailsResponse()
            {
                PackName = merchPackDetails.MerchPackName,
                Items = merchPackDetails.PackItems.Select(x => new MerchPackItem()
                {
                    Name = x.Name,
                    Properties = x.Properties
                }).ToList()
            };

            return Ok(convertedResult);
        }
    }
}