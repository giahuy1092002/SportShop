using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Detail { get; set; }
        public string Form {  get; set; }
        public string Outstanding {  get; set; }
        public string Sports { get; set; }
        public List<ProductSKU> Skus { get; set; }
        public List<Image> Images { get; set; } = new List<Image>();
        public int SubCategoryId { get; set; }
        public SubCategory SubCategory { get; set; }

    }
}
