using AutoMapper.Configuration.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Data.ViewModel
{
    public class ProductSKUDto
    {
        public int Id { get; set; }
        public int SizeId { get; set; }
        public string Size { get; set; }
        public string Color { get; set; }
        public string Sku { get; set; }
        public int QuantityInStock { get; set; }
        public int Price {  get; set; }
    }
    public class ProductSKUOptionDto
    {
        public int Id { get; set; }
        public string Size { get; set; }
    }
}
