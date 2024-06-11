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
using Data;

namespace Service
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public OrderService(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<OrderDto> Create(string buyerId, bool isDefault, ShippingAddress? shippingAddress)
        {
           var cart = await _unitOfWork.Carts.Retrieve(buyerId);
           var user = await _unitOfWork.Accounts.GetUserByName(buyerId);
           var items = new List<OrderItem>();
            foreach (var item in cart.Items)
            {
                var productItem = await _unitOfWork.ProductSKUs.GetById(item.ProductSKUId);
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
            await _unitOfWork.Orders.Add(order);
            await _unitOfWork.Carts.Delete(cart);
            return _mapper.Map<OrderDto>(order);
        }

        public async Task<List<OrderDto>> GetByUser(string buyerId)
        {
            return await _unitOfWork.Orders.GetByUser(buyerId);
        }
    }
}
