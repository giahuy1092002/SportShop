using Data;
using Data.Entities;
using Data.Interface;
using Data.ViewModel;
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
        private readonly IUnitOfWork _unitOfWork;

        public ProductSKUService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Add(ProductSKU productSKU)
        {
            await _unitOfWork.ProductSKUs.Add(productSKU);
        }

        public async Task<List<ProductSKU>> GetByColor(int productId, int colorId)
        {
            return await _unitOfWork.ProductSKUs.GetByColor(productId,colorId);
        }

        public async Task<ProductSKU> GetProductSKU(int productSKUId)
        {
            return await _unitOfWork.ProductSKUs.GetProductSKU(productSKUId);
        }

        //public async Task<List<ProductSKUListDto>> GetProductSKUs()
        //{
        //    var products = await _unitOfWork.Products.GetProducts();

        //}
    }
}
