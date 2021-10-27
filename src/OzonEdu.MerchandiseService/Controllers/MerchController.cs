using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MerchandiseService.Models;
using MerchandiseService.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MerchandiseService.Controllers
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

        [HttpGet("{employeeId:long}")]
        public async Task<ActionResult<List<MerchPackInStatus>>> GetMerchIssuedToEmployee(
            long employeeId, CancellationToken token)
        {
            var merchItem = await _merchandiseService.GetIssuedMerchToEmployee(employeeId, token);
            
            return Ok(merchItem);
        }
        
        [HttpPost]
        public async Task<ActionResult> IssueMerchToEmployee(long employeeId, string merchPackName, CancellationToken token)
        {
            var status = await _merchandiseService.IssueMerchToEmployee(employeeId, merchPackName, token);
            switch (status)
            {
                case MerchIssueRequestStatus.EmployeeAlreadyHasSuchMerch:
                case MerchIssueRequestStatus.NoSuchEmployeeExists:
                    return Problem($"Failed to issue {merchPackName} to {employeeId}: {status}");
                case MerchIssueRequestStatus.RequestCreated:
                    return Ok();
            }

            return Problem($"Unknown status {status}");
        }

        [HttpGet]
        public async Task<ActionResult<MerchPack>> GetMerchPackContent(string merchPackName, CancellationToken token)
        {
            var merchPackDetails = await _merchandiseService.GetMerchPackContent(merchPackName, token);

            if (merchPackDetails is null)
            {
                return NotFound();
            }

            return Ok(merchPackDetails);
        }
    }
}