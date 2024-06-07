using Data.Entities.OrderAggregate;
using Data.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interface
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task<List<OrderDto>> GetByUser(string buyerId);
    }
}
