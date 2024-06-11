using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Model
{
    public class CreateProductModel
    {
        public string Name { get; set; }
        public string Detail { get; set; }
        public string Form { get; set; }
        public string Outstanding { get; set; }
        public string Sports { get; set; }
        public int SubCategoryId { get; set; }
        public int ColorId { get; set; }
        public int Price { get; set; }
        public List<IFormFile> PictureUrls { get; set; }
        public List<int> Sizes { get; set; }
    }
    public class AddProductColorModel
    {
        public int ColorId { get; set; }
        public int Price { get; set; }
        public List<IFormFile> PictureUrls { get; set; }
        public List<int> Sizes { get; set; }
    }
}
