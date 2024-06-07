using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.ViewModel
{
    public class CartItemDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public string Size { get; set; }
        public string PictureUrl { get; set; }
        public int ProductSKUId { get; set; }
        public int ColorId { get; set; }
        public string Color {  get; set; }
        public int Quantity { get; set; }
    }
}
