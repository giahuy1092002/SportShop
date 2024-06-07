using AutoMapper.Configuration.Conventions;
using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interface
{
    public interface IProductSKURepository : IRepository<ProductSKU>
    {
        Task<ProductSKU> GetProductSKU(int productId);
        Task<List<ProductSKU>> GetByColor(int productId, int colorId);
    }
}
