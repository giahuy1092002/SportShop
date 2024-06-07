using Data.Entities.OrderAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.ViewModel
{
    public class OrderDto
    {
        public int Id { get; set; }
        public string BuyerId { get; set; }

        public DateTime OrderDate { get; set; }
        public List<OrderItemDto> OrderItems { get; set; } = new List<OrderItemDto>();
        public string Status { get; set; }
        public int DeliveryFee { get; set; }
        public int SubTotal { get; set; }
    }
}
