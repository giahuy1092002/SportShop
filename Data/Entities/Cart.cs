using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class Cart
    {
        public int Id { get; set; }
        public string BuyerId { get; set; }
        public List<CartItem> Items { get; set; } = new List<CartItem>();
        public void AddItem(ProductSKU productSKU, int quantity)
        {
            if(Items.All(i=>i.ProductSKUId!=productSKU.Id))
            {
                Items.Add(new CartItem { ProductSKU= productSKU, Quantity=quantity });
                return;
            }    
            var existItem = Items.FirstOrDefault(i=>i.ProductSKUId==productSKU.Id);
            if (existItem != null) existItem.Quantity += quantity;
        }    
        public void RemoveItem(int productSKUId, int quantity)
        {
            var existItem = Items.FirstOrDefault(i => i.ProductSKUId == productSKUId);
            if (existItem == null) return;
            existItem.Quantity -= quantity;
            if(existItem.Quantity==0) Items.Remove(existItem);
        }
    }
}
