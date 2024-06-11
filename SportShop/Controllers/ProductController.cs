using Data.Interface;
using Data.Model;
using Data.RequestHelper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service;
using Service.Interface;

namespace SportShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetProducts([FromQuery] ProductParams productParams)
        {
            return Ok(await _productService.GetProducts(productParams));
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetProductColorDetail(int productId, int colorId)
        {
            return Ok(await _productService.GetProductColorDetail(productId,colorId));
        }
        [HttpGet("GetSizes")]
        public async Task<IActionResult> GetSizes()
        {
            var products = await _productService.GetProducts();
            var sizes = products.SelectMany(p => p.Skus).Select(p => p.Size.Value).Distinct().ToList();
            var colors = products.SelectMany(p => p.Skus).Select(p => p.Color.Value).Distinct().ToList();
            return Ok(new { sizes, colors });
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> Create([FromForm] CreateProductModel createProduct)
        {
            return Ok(await _productService.Create(createProduct));
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> AddColor([FromForm] AddProductColorModel createProduct,int productId)
        {
            return Ok(await _productService.AddColor(createProduct, productId));
        }
    }
}
