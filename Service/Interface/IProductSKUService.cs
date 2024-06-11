using Data.Entities;
using Data.ViewModel;
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
        //Task<List<ProductSKUListDto>> GetProductSKUs();
        Task<List<ProductSKU>> GetByColor(int productId, int colorId);
        Task Add(ProductSKU productSKU);
    }
}
