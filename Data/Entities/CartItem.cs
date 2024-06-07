using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class CartItem
    {
        public int Id { get; set; }
        public int ProductSKUId { get; set; }
        public ProductSKU ProductSKU { get; set; }
        public int Quantity { get; set; }
        public int CartId { get; set; }
        public Cart Cart { get; set; }
    }
}
