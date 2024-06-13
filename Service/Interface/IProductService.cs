using Data.Entities;
using Data.Model;
using Data.RequestHelper;
using Data.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IProductService
    {
        Task<PagedList<ProductListDto>> GetProducts(ProductParams productParams, int subCategoryId);
        Task<ProductColorDetail> GetProductColorDetail(int productId, int colorId);
        Task<List<Product>> GetAll(ProductParams productParams, int subCategoryId);
        Task<bool> AddColor(AddProductColorModel createProduct, int productId);
        Task<bool> Create(CreateProductModel createProduct);
    }
}
