using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.RequestHelper
{
    public class PaginationParams
    {
        private const int MaxSize = 50;
        public int PageNumber { get; set; } = 1;
        private int _pageSize = 6;
        public int PageSize
        {
            get=> _pageSize;
            set => _pageSize = value > MaxSize ? MaxSize : value;
        }
    }
}
