using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Reports.Api.Services;

namespace Reports.Api.Controllers
{
    [ApiController]
    [Route("production-order")]
    public class ProductionOrderController : ControllerBase
    {
        private readonly IProductionOrderService _productionOrderService;
        public ProductionOrderController(IProductionOrderService productionOrderService)
        {
            _productionOrderService = productionOrderService;
        }

        [HttpGet("by-date")]
        public async Task<IActionResult> GetByExactDate([FromQuery] DateOnly date)
        {
            var result = await _productionOrderService.GetByStartDateAsync(date);
            return Ok(result);
        }
    }
}
