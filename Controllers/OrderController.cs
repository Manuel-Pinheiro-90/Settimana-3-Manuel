using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Settimana_3_Manuel.Service;
using System.Security.Claims;

namespace Settimana_3_Manuel.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IProductService _productService;

        public OrderController(IOrderService orderService, IProductService productService)
        {
            _orderService = orderService;
            _productService = productService;
        }
        // ///////////////////////////////////////////////////////////////////////////////////


        public async Task<IActionResult> Index()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var orders = await _orderService.GetUserOrders(userId);
            return View(orders);
        }

        // ///////////////////////////////////////////////////////////////////////////////////
        
        [HttpPost]
        public async Task<IActionResult> AddToOrder(int productId, int quantity, string address, string extraNote)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            await _orderService.AddToOrder(userId, productId, quantity, address, extraNote);
            return Ok(); 
        }

        // ///////////////////////////////////////////////////////////////////////////////////
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AllOrders()
        {
            var orders = await _orderService.GetAllOrders();
            return View(orders);
        }

        // ///////////////////////////////////////////////////////////////////////////////////
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ProcessOrder(int orderId)
        {
            await _orderService.ProcessOrder(orderId);
            return RedirectToAction("AllOrders");
        }

        // /////////////////////////////////////////////////////////////////////////////////////////
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> DeleteOrder(int orderId)
        {
            await _orderService.DeleteOrder(orderId);
            return RedirectToAction("AllOrders");
        }




    }
}
