using Azure;
using Data.Entities.OrderAggregate;
using Data.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service;
using Service.Interface;

namespace SportShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IVnPayService _vnPayService;
        private readonly IOrderService _orderService;

        public PaymentController(IVnPayService vnPayService,IOrderService orderService)
        {
            _vnPayService = vnPayService;
            _orderService = orderService;
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> PaymentCallback()
        {
            var response = _vnPayService.PaymentExecute(Request.Query);
            string orderId = Request.Query["vnp_TxnRef"];
            var order = await _orderService.GetOrder(int.Parse(orderId));
            order.OrderStatus = OrderStatus.PaymentReceived;
            order.SubTotal = 0;
            await _orderService.Update(order);
            return Redirect($"http://localhost:3000/callback?status={response.Success}");
        }
    }
}
