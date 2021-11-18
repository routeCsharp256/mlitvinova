using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OzonEdu.MerchandiseService.Domain.AggregationModels.MerchPackRequestAggregate;
using OzonEdu.MerchandiseService.HttpModels;
using OzonEdu.MerchandiseService.Infrastructure.DomainServices.Interfaces;
using OzonEdu.MerchandiseService.Infrastructure.Exceptions;

namespace OzonEdu.MerchandiseService.Controllers
{
    [ApiController]
    [Route("v1/api/merch")]
    [Produces("application/json")]
    public class MerchController : ControllerBase
    {
        private readonly IMerchRequestDomainService _merchRequestDomainService;

        public MerchController(IMerchRequestDomainService merchRequestDomainService)
        {
            _merchRequestDomainService = merchRequestDomainService;
        }

        [HttpGet("GetMerchPackContent")]
        public async Task<ActionResult<GetMerchPackDetailsResponse>> GetMerchPackContent(string merchPackName, CancellationToken token)
        {
            var merchPackDetails = await _merchRequestDomainService.GetMerchPackContent(merchPackName, token);
        
            if (merchPackDetails is null)
            {
                return NotFound();
            }
        
            var convertedResult = new GetMerchPackDetailsResponse()
            {
                PackName = merchPackDetails.Name.Value,
                Items = merchPackDetails.MerchPackItems.Select(x => new MerchPackItem()
                {
                    Name = x.ItemType.Name,
                    Properties = x.Constraints.Any() ?
                        x.Constraints.ToDictionary(p => p.Key(), p => p.Value())
                        : new Dictionary<string, string>()
                }).ToList()
            };
        
            return Ok(convertedResult);
        }
        
        [HttpPost("IssueMerchToEmployee")]
        public async Task<ActionResult<IssueMerchToEmployeeResponse>> IssueMerchToEmployee(
            int employeeId,
            string merchPackName, 
            Dictionary<string, string> constraints,
            CancellationToken token)
        {
            var response = IssueMerchResponse.Created;
            try
            {
                await _merchRequestDomainService.GiveOutMerch(employeeId, merchPackName, constraints, token);
            }
            catch (MerchPackNotFoundException)
            {
                response = IssueMerchResponse.NoSuchMerch;
            }
            catch (MerchAlreadyIssuedException)
            {
                response = IssueMerchResponse.MerchAlreadyIssued;
            }

            var result = new IssueMerchToEmployeeResponse()
            {
                IssueMerchResponse = response
            };
            
            return Ok(result);
        }
                
        [HttpGet("GetMerchIssuedToEmployee")]
        public async Task<ActionResult<GetMerchPackIssuedToEmployeeResponse>> GetMerchIssuedToEmployee(
            int employeeId, CancellationToken token)
        {
            var merchIssued = await _merchRequestDomainService.GetMerchIssuedToEmployee(employeeId, token);
            
            var convertedMerchIssued = new GetMerchPackIssuedToEmployeeResponse()
            {
                MerchList = merchIssued.Select(x =>
                {
                    var status = x.Status switch
                    {
                        var s when s.Equals(MerchPackRequestStatus.Completed) 
                            => MerchPackStatus.Completed,
                        var s when s.Equals(MerchPackRequestStatus.WaitingForEmployeeToTakeIt) 
                            => MerchPackStatus.WaitingForEmployeeToTakeIt,
                        var s when s.Equals(MerchPackRequestStatus.WaitingForSupplies) 
                            => MerchPackStatus.WaitingForSupplies,
                        _ => throw new ArgumentOutOfRangeException()
                    };
                    
                    return new MerchPackInStatus()
                    {
                        MerchPackName = x.Name.Value,
                        Status = status
                    };
                }).ToList()
            };
            
            return convertedMerchIssued;
        }
                        
        [HttpPost("GiveOutPreparedPack")]
        public async Task<ActionResult<GiveOutPreparedPackResponse>> GiveOutPreparedPack(
            int employeeId, string packName, CancellationToken token)
        {
            await _merchRequestDomainService.GiveOutPreparedPack(employeeId, packName, token);

            return Ok(new GiveOutPreparedPackResponse() {Status = GiveOutPreparedPackStatus.Ok});
        }

        [HttpPost("ProcessNewSupplyArrival")]
        public async Task<ActionResult<ProcessNewSupplyArrivalResponse>> ProcessNewSupportArrival(List<long> skuArrived, CancellationToken token)
        {
            await _merchRequestDomainService.ProcessNewSupplyArrival(skuArrived, token);
            
            return Ok(new ProcessNewSupplyArrivalResponse() {Status = ProcessNewSupplyArrivalStatus.Ok});
        }
    }
}