using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.ViewModel
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Detail { get; set; }
        public string Form { get; set; }
        public string Outstanding { get; set; }
        public string Sports { get; set; }
        public List<ProductSKUDto> Skus { get; set; }
        public int SubCategoryId { get; set; }
    }
}
