using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities.OrderAggregate
{
    public class Order
    {
        public int Id { get; set; }
        public string BuyerId { get; set; }

        public DateTime OrderDate { get; set; } = DateTime.UtcNow;
        public List<OrderItem> OrderItems { get; set;} = new List<OrderItem>();
        public OrderStatus OrderStatus { get; set; } = OrderStatus.Pending;
        public int DeliveryFee { get; set; }
        public int SubTotal { get; set; }
        public ShippingAddress ShippingAddress { get; set; }
        public int GetTotal()
        {
            return DeliveryFee + SubTotal;
        }
    }
}
