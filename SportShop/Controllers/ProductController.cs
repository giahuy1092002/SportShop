using Data;
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
        private readonly IUnitOfWork _unitOfWork;

        public ProductController(IProductService productService,IUnitOfWork unitOfWork)
        {
            _productService = productService;
            _unitOfWork = unitOfWork;
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetProducts([FromQuery] ProductParams productParams, int subCategoryId)
        {
            var products = await _productService.GetProducts(productParams, subCategoryId);
            var subCategory = await _unitOfWork.SubCategory.GetSubCategory(subCategoryId);
            var name = subCategory.Name;
            var gender = subCategory.Category.Gender.Name;
            return Ok(new { gender,name,products});
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetProductColorDetail(int productId, int colorId)
        {
            return Ok(await _productService.GetProductColorDetail(productId,colorId));
        }
        [HttpGet("GetSizes")]
        public async Task<IActionResult> GetSizes([FromQuery] ProductParams productParams, int subCategoryId)
        {
            var products = await _productService.GetAll(productParams, subCategoryId);
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
        [HttpGet("[action]")]
        public async Task<IActionResult> GetByName(string name)
        {
            return Ok(await _unitOfWork.Products.GetByName(name));
        }
    }
}
