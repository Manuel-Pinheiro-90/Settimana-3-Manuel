using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Settimana_3_Manuel.Service;

namespace Settimana_3_Manuel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderApiController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderApiController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet("totalProcessedOrders")]
        public async Task<IActionResult> GetTotalProcessedOrders()
        {
            var totalProcessedOrders = await _orderService.GetTotalProcessedOrders();
            return Ok(totalProcessedOrders);
        }

        [HttpGet("totalEarningsForDay")]
        public async Task<IActionResult> GetTotalEarningsForDay()
        {
            var totalEarnings = await _orderService.GetTotalEarningsForDay();
            return Ok(totalEarnings);
        }

























    }
}
