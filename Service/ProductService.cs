using Service.Interface;
using Data.Entities;
using Data.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.ViewModel;
using Data.RequestHelper;

namespace Service
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<ProductColorDetail> GetProductColorDetail(int productId,int colorId)
        {
            return await _productRepository.GetProductColorDetail(productId,colorId);
        }

        public async Task<List<ProductListDto>> GetProducts(ProductParams productParams)
        {
            var products = await _productRepository.GetProducts(productParams);
            return products;
        }

        public async Task<List<Product>> GetProducts()
        {
            return await _productRepository.GetProducts();
        }
    }
}
