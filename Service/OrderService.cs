using Data.Entities.OrderAggregate;
using Data.Interface;
using Service.Interface;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Entities;
using Data.ViewModel;
using AutoMapper;

namespace Service
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductSKURepository _productSKURepository;
        private readonly ICartRepository _cartRepository;
        private readonly IMapper _mapper;
        private readonly IAccountRepository _accountRepository;

        public OrderService(IOrderRepository orderRepository, 
            IProductSKURepository productSKURepository,
            ICartRepository cartRepository,
            IMapper mapper,
            IAccountRepository accountRepository
            )
        {
            _orderRepository = orderRepository;
            _productSKURepository = productSKURepository;
            _cartRepository = cartRepository;
            _mapper = mapper;
            _accountRepository = accountRepository;
        }

        public async Task<OrderDto> Create(string buyerId, bool isDefault, ShippingAddress? shippingAddress)
        {
           var cart = await _cartRepository.Retrieve(buyerId);
           var user = await _accountRepository.GetUserByName(buyerId);
           var items = new List<OrderItem>();
            foreach (var item in cart.Items)
            {
                var productItem = await _productSKURepository.GetById(item.ProductSKUId);
                var orderItem = new OrderItem
                {
                    ProductSKU = productItem,
                    Price = productItem.Price,
                    Quantity = item.Quantity
                };
                items.Add(orderItem);
            }
            var subtotal = items.Sum(item => item.Price * item.Quantity);
            var deliveryFee = subtotal > 10000 ? 0 : 500;
            var order = new Order
            {
                OrderItems = items,
                BuyerId = buyerId,
                SubTotal = subtotal,
                DeliveryFee = deliveryFee
            };
            if(isDefault)
            {
                var userAddress = user.AddressBook.FirstOrDefault(a => a.IsDefault == true);
                var address = new ShippingAddress
                {
                    FirstName = userAddress.FirstName,
                    LastName = userAddress.LastName,
                    City = userAddress.City,
                    Commune = userAddress.Commune,
                    District = userAddress.District,
                    PhoneNumber = userAddress.PhoneNumber,
                    AddressLine = userAddress.AddressLine,
                };
                order.ShippingAddress = address;
            }    
            else
            {
                order.ShippingAddress = shippingAddress;
            }    
            await _orderRepository.Add(order);
            await _cartRepository.Delete(cart);
            return _mapper.Map<OrderDto>(order);
        }

        public async Task<List<OrderDto>> GetByUser(string buyerId)
        {
            return await _orderRepository.GetByUser(buyerId);
        }
    }
}
