using AutoMapper;
using Data.Entities;
using Data.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Interface;
using System.Security.Claims;

namespace SportShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;
        private readonly IProductSKUService _productSKUService;
        private readonly IMapper _mapper;

        public CartController(
            ICartService cartService,
            IProductSKUService productSKUService,
            IMapper mapper
            )
        {
            _cartService = cartService;
            _productSKUService = productSKUService;
            _mapper = mapper;
        }
        [HttpGet(Name = "GetCart")]
        public async Task<ActionResult<CartDto>> GetCart()
        {
            var cart = await _cartService.Retrieve(GetBuyerId());
            if (cart == null) return NotFound();
            var cartDto = _mapper.Map<CartDto>(cart);
            return Ok(cartDto);
        }
        [HttpPost]
        public async Task<ActionResult> AddItem(int productSKUId, int quantity = 1)
        {
            var cart = await _cartService.Retrieve(GetBuyerId());
            if (cart == null) cart = await CreateCart();
            var productSKU = await _productSKUService.GetProductSKU(productSKUId);
            if (productSKU == null) return BadRequest(new ProblemDetails { Title = "Product Not Found" });
            cart = await _cartService.AddItem(productSKU, quantity, cart);
            var cartDto = _mapper.Map<CartDto>(cart);
            if (cart!=null) return CreatedAtRoute("GetCart", cartDto);
            return BadRequest(new ProblemDetails { Title = "Problem saving item into cart" });
        }
        [HttpDelete]
        public async Task<ActionResult<bool>> DeleteItem(int productSKUId, int quantity = 1)
        {
            var cart = await _cartService.Retrieve(GetBuyerId());
            if (cart == null) return NotFound();
            var productSKU = await _productSKUService.GetProductSKU(productSKUId);
            if (productSKU == null) return NotFound();
            return await _cartService.RemoveItem(productSKUId, quantity, cart);
        }

        private async Task<Cart> CreateCart()
        {
            var buyerId = User.Identity.Name;
            if (string.IsNullOrEmpty(buyerId))
            {
                buyerId = Guid.NewGuid().ToString();
                var optionCookies = new CookieOptions
                {
                    IsEssential = true,
                    Secure = true,
                    SameSite = SameSiteMode.None,
                    Expires = DateTime.Now.AddDays(30),
                };
                Response.Cookies.Append("buyerId", buyerId, optionCookies);
            }
            var cart =  await _cartService.AddCart(buyerId);
            return cart;

        }
        private string GetBuyerId()
        {
            return User.FindFirst(ClaimTypes.Email)?.Value ?? Request.Cookies["buyerId"];
        }
    }
}
