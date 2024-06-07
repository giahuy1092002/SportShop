using Data.Entities.OrderAggregate;
using Data.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Interface;

namespace SportShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> CreateOrder(bool isDefault, ShippingAddress? shippingAddress)
        {
            var buyerId = User.Identity.Name;
            var order = await _orderService.Create(buyerId, isDefault, shippingAddress);
            return Ok(order);
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetByUser()
        {
            var buyerId = User.Identity.Name;
            return Ok(await _orderService.GetByUser(buyerId));
        }
    }
}
