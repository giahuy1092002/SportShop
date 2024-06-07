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
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public OrderRepository(SportStoreContext context,IUnitOfWork uow,IMapper mapper):base(context)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<List<OrderDto>> GetByUser(string buyerId)
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
            return _mapper.Map<List<OrderDto>>(orders);   
        }
    }
}
