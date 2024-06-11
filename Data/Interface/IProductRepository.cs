using Data.Entities;
using Data.RequestHelper;
using Data.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interface
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<List<ProductListDto>> GetProducts(ProductParams productParams);
        Task<ProductColorDetail> GetProductColorDetail(int productId,int colorId);
        Task<List<Product>> GetProducts();
        Task<Product> GetProduct(int productId);
    }
}
