using Data.Entities.OrderAggregate;
using Data.Model;
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
        private readonly IVnPayService _vnPayService;

        public OrderController(IOrderService orderService,IVnPayService vnPayService)
        {
            _orderService = orderService;
            _vnPayService = vnPayService;
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> CreateOrder(bool isDefault, ShippingAddress? shippingAddress, string payment)
        {
            var buyerId = User.Identity.Name;
            if (payment=="VNPAY")
            {
                var order = await _orderService.Create(buyerId,isDefault,shippingAddress);
                var model = new PaymentInformationModel
                {
                    OrderType = "Thanh toán online qua VNPay",
                    OrderDescription = "Thanh toán GH STORE",
                    Amount = order.SubTotal,
                    OrderId = order.Id,
                    Name = "Thanh toán GH Store"
                };
                var url = _vnPayService.CreatePaymentUrl(model, HttpContext);
                return Ok(url);
            }    
            var result = await _orderService.Create(buyerId, isDefault, shippingAddress);
            return Ok(result);
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetByUser(string? status)
        {
            var buyerId = User.Identity.Name;
            return Ok(await _orderService.GetByUser(buyerId,status));
        }
    }
}
