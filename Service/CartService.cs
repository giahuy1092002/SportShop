using Data.Entities;
using Data.Interface;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;

        public CartService(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        public async Task<Cart> AddCart(string buyerId)
        {
            return await _cartRepository.AddCart(buyerId);
        }

        public async Task<Cart> AddItem(ProductSKU productSKU, int quantity, Cart cart)
        {
            return await _cartRepository.AddItem(productSKU, quantity, cart);
        }

        public async Task<bool> RemoveItem(int productSKUId, int quantity, Cart cart)
        {
            return await _cartRepository.RemoveItem(productSKUId, quantity, cart);
        }

        public async Task<Cart> Retrieve(string buyerId)
        {
            return await _cartRepository.Retrieve(buyerId);
        }
        public async Task Remove(Cart cart)
        {
            await _cartRepository.Delete(cart);
        }
        public async Task Update(Cart cart)
        {
            await _cartRepository.Update(cart);
        }
    }
}
