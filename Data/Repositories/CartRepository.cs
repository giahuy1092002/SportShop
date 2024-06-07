using Data.DataContext;
using Data.Entities;
using Data.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class CartRepository : Repository<Cart>, ICartRepository
    {
        private readonly IUnitOfWork _uow;

        public CartRepository(SportStoreContext context, IUnitOfWork uow) : base(context)
        {
            _uow = uow;
        }

        public async Task<Cart> AddCart(string buyerId)
        {
            var cart = new Cart
            {
                BuyerId = buyerId,
            };
            await Entities.AddAsync(cart);
            return cart;
        }

        public async Task<Cart> AddItem(ProductSKU productSKU, int quantity, Cart cart)
        {
            cart.AddItem(productSKU, quantity);
            await _uow.SaveChangesAsync();
            return await Retrieve(cart.BuyerId);
        }

        public async Task<bool> RemoveItem(int productSKUId, int quantity, Cart cart)
        {
            cart.RemoveItem(productSKUId, quantity);
            await _uow.SaveChangesAsync();
            return true;
        }

        public async Task<Cart> Retrieve(string buyerId)
        {
            return await Entities
                .Include(c => c.Items)
                    .ThenInclude(i => i.ProductSKU)
                        .ThenInclude(p => p.Product)
                            .ThenInclude(p => p.Images)
                .Include(c => c.Items)
                    .ThenInclude(i => i.ProductSKU)
                        .ThenInclude(p => p.Color)
                .Include(c => c.Items)
                    .ThenInclude(i => i.ProductSKU)
                        .ThenInclude(p => p.Size)
                .FirstOrDefaultAsync(c => c.BuyerId == buyerId);

        }
    }
}



