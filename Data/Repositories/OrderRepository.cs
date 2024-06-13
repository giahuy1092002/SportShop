using AutoMapper;
using Data.DataContext;
using Data.Entities.OrderAggregate;
using Data.Interface;
using Data.ViewModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        private readonly IMapper _mapper;

        public OrderRepository(SportStoreContext context,IMapper mapper):base(context)
        {
 
            _mapper = mapper;
        }

        public async Task<List<OrderDto>> GetByUser(string buyerId,string? status)
        {
            var orders = await Entities
                .Include(o=>o.OrderItems)
                    .ThenInclude(o=>o.ProductSKU)
                        .ThenInclude(p=>p.Product)
                            .ThenInclude(p => p.Images)
                 .Include(o => o.OrderItems)
                    .ThenInclude(o => o.ProductSKU)
                        .ThenInclude(p => p.Color)
                 .Include(o => o.OrderItems)
                    .ThenInclude(o => o.ProductSKU)
                        .ThenInclude(p => p.Size)
                .Where(o => o.BuyerId == buyerId)
                .ToListAsync();
            if(!string.IsNullOrEmpty(status))
            {
                orders = orders.Where(o => o.OrderStatus.ToString() == status).ToList();
            }
            return _mapper.Map<List<OrderDto>>(orders);   
        }

        public async Task<Order> GetOrder(int orderId)
        {
            var order = await Entities
                .Include(o => o.OrderItems)
                    .ThenInclude(o => o.ProductSKU)
                        .ThenInclude(p => p.Product)
                            .ThenInclude(p => p.Images)
                 .Include(o => o.OrderItems)
                    .ThenInclude(o => o.ProductSKU)
                        .ThenInclude(p => p.Color)
                 .Include(o => o.OrderItems)
                    .ThenInclude(o => o.ProductSKU)
                        .ThenInclude(p => p.Size)
                .FirstOrDefaultAsync(o => o.Id == orderId);
            return order;
        }
    }
}
