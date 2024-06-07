using Data.DataContext;
using Data.Entities;
using Data.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class ProductSKURepository: Repository<ProductSKU>, IProductSKURepository
    {
        public ProductSKURepository(SportStoreContext context):base(context)
        {
      
        }

        public async Task<ProductSKU> GetProductSKU(int productSKUId)
        {
            return await Entities
                .Include(p=>p.Color)
                .Include(p=>p.Size)
                .FirstOrDefaultAsync(p => p.Id == productSKUId);
        }

        public async Task<List<ProductSKU>> GetByColor(int productId, int colorId)
        {
            return await Entities
                .Include(p => p.Color)
                .Include(p => p.Size)
                .Where(p => p.ProductId == productId && p.ColorId == colorId).ToListAsync();
        }
    }
}
