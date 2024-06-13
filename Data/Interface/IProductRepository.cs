using Data.Entities;
using Data.Model;
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
        Task<List<ProductListDto>> GetProducts(ProductParams productParams,int subCategoryId);
        Task<ProductColorDetail> GetProductColorDetail(int productId,int colorId);
        Task<List<Product>> GetAll(ProductParams productParams, int subcategoryId);
        Task<Product> GetProduct(int productId);
        Task<List<ProductSearchDto>> GetByName(string name);
    }
}
