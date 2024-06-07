using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.RequestHelper
{
    public class ProductParams : PaginationParams
    {
        public string OrderBy { get; set; } = "";
        public string Colors { get; set; } = "";
        public string Sizes { get; set; } = "";
    }
}
