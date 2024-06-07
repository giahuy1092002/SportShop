using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interface
{
    public interface ICartRepository : IRepository<Cart>
    {
        Task<Cart> AddItem(ProductSKU productSKU, int quantity, Cart cart);
        Task<bool> RemoveItem(int productSKUId, int quantity, Cart cart);
        Task<Cart> AddCart(string buyerId);
        Task<Cart> Retrieve(string buyerId);
    }
}
