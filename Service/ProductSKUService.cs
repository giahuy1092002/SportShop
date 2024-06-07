using Data.Entities;
using Data.Interface;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class ProductSKUService : IProductSKUService
    {
        private readonly IProductSKURepository _productSKURepository;

        public ProductSKUService(IProductSKURepository productSKURepository)
        {
            _productSKURepository = productSKURepository;
        }

        public async Task Add(ProductSKU productSKU)
        {
            await _productSKURepository.Add(productSKU);
        }

        public async Task<List<ProductSKU>> GetByColor(int productId, int colorId)
        {
            return await _productSKURepository.GetByColor(productId,colorId);
        }

        public async Task<ProductSKU> GetProductSKU(int productSKUId)
        {
            return await _productSKURepository.GetProductSKU(productSKUId);
        }
    }
}
