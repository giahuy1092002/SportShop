using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities.OrderAggregate
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int ProductSKUId { get; set; }
        public ProductSKU ProductSKU { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }
        public int OrderID { get; set; }
        public Order Order { get; set; }

    }
}
