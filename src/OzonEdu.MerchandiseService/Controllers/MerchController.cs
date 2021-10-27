using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OzonEdu.MerchandiseService.HttpModels;
using OzonEdu.MerchandiseService.Models;
using OzonEdu.MerchandiseService.Services.Interfaces;
using MerchItem = OzonEdu.MerchandiseService.Models.MerchItem;

namespace OzonEdu.MerchandiseService.Controllers
{
    [ApiController]
    [Route("v1/api/merch")]
    [Produces("application/json")]
    public class MerchController : ControllerBase
    {
        private readonly IMerchandiseService _merchandiseService;

        public MerchController(IMerchandiseService service)
        {
            _merchandiseService = service;
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
        public async Task<ActionResult> IssueMerchToEmployee(long employeeId, string merchPackName, CancellationToken token)
        {
            var status = await _merchandiseService.IssueMerchToEmployee(employeeId, merchPackName, token);
            switch (status)
            {
                case MerchIssueRequestStatus.EmployeeAlreadyHasSuchMerch:
                case MerchIssueRequestStatus.NoSuchMerchExists:
                    return Problem($"Failed to issue {merchPackName} to {employeeId}: {status}");
                case MerchIssueRequestStatus.RequestCreated:
                    return Ok();
            }

            return Problem($"Unknown status {status}");
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