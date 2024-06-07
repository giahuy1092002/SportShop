using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IProductSKUService
    {
        Task<ProductSKU> GetProductSKU(int productSKUId);
        Task<List<ProductSKU>> GetByColor(int productId, int colorId);
        Task Add(ProductSKU productSKU);
    }
}
