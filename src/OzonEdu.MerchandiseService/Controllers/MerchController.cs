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
        public async Task<ActionResult<List<(MerchPack, MerchPurchaseStatus)>>> GetMerchIssuedToEmployee(
            long employeeId, CancellationToken token)
        {
            var merchItem = await _merchandiseService.GetIssuedMerchToEmployee(employeeId, token);
            
            return merchItem;
        }
        
        [HttpPost]
        public async Task<ActionResult> IssueMerchToEmployee(long employeeId, MerchPack pack, CancellationToken token)
        {
            await _merchandiseService.IssueMerchToEmployee(employeeId, pack, token);
            return Ok();
        }
    }
}