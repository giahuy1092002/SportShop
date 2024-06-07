using Data.Entities;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.ViewModel
{
    public class ProductColorDetail
    {
        public string Name { get; set; }
        public int Price { get; set; }
        public string Color { get; set; }
        public List<Color> Colors { get; set; }
        public List<ProductSKUOptionDto> Options { get; set; }
        public List<ImageColorDto> ImagesColors { get; set; }
        public List<ImageDto> Images { get; set; }
    }
}
