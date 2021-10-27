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
        public async Task<ActionResult<List<(string merchPackName, MerchPurchaseStatus status)>>> GetMerchIssuedToEmployee(
            long employeeId, CancellationToken token)
        {
            var merchItem = await _merchandiseService.GetIssuedMerchToEmployee(employeeId, token);
            
            return Ok(merchItem);
        }
        
        [HttpPost]
        public async Task<ActionResult> IssueMerchToEmployee(long employeeId, string merchPackName, CancellationToken token)
        {
            var success = await _merchandiseService.IssueMerchToEmployee(employeeId, merchPackName, token);
            if (!success)
            {
                return Problem($"Failed to issue {merchPackName} to {employeeId}");
            }
            
            return Ok();
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