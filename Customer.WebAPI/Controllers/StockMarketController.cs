using Customer.Core.Interfaces.IAppServices;
using Microsoft.AspNetCore.Mvc;

namespace Customer.WebAPI.Controllers
{
    [Route("api/" + nameof(Contexts.CustomerAppContext) + "/" + "[controller]")]
    public class StockMarketController : BaseController
    {
        private readonly IStockMarketService _service;
        public StockMarketController(IStockMarketService service)
        {
            _service = service;
        }

        [HttpGet("Getstock")]
        public async Task<IActionResult> GetStock()
        {
          
            if (!ModelState.IsValid)
            { return BadRequest(ModelState); }

            if (ModelState.IsValid)
            {
                return Ok(await _service.GetStock());
            }

            return BadRequest(ModelState);

        }
    }
}
