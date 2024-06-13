using Data.Entities.OrderAggregate;
using Data.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IOrderService
    {
        Task<OrderDto> Create(string buyerId,bool isDefault, ShippingAddress shippingAddress);
        Task<List<OrderDto>> GetByUser(string buyerId,string? status);
        Task<Order> GetOrder(int orderId);
        Task Update(Order order);
    }
}
