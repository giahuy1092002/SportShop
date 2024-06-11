using Data;
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
        private readonly IUnitOfWork _unitOfWork;

        public CartService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Cart> AddCart(string buyerId)
        {
            return await _unitOfWork.Carts.AddCart(buyerId);
        }

        public async Task<Cart> AddItem(ProductSKU productSKU, int quantity, Cart cart)
        {
            var result =  await _unitOfWork.Carts.AddItem(productSKU, quantity, cart);
            return result;
        }

        public async Task<bool> RemoveItem(int productSKUId, int quantity, Cart cart)
        {
            var result = await _unitOfWork.Carts.RemoveItem(productSKUId, quantity, cart);
            return result;
        }

        public async Task<Cart> Retrieve(string buyerId)
        {
            return await _unitOfWork.Carts.Retrieve(buyerId);
        }
        public async Task Remove(Cart cart)
        {
            await _unitOfWork.Carts.Delete(cart);
        }
        public async Task Update(Cart cart)
        {
            await _unitOfWork.Carts.Update(cart);
        }
    }
}
