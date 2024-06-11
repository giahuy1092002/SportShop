using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.ViewModel
{
    public class ProductListDto
    { 
        public int Id { get; set; }
        public string Name { get; set; }
        public int ColorId { get; set; } // Màu cụ thể của sản phẩm này
        public string PictureUrl { get; set; } // Hình ảnh chính của màu cụ thể này
        public List<ImageColorDto> ImageColors { get; set; }
        public int Price { get; set; }

    }
}
