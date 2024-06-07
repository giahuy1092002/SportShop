using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interface
{
    public interface IImageRepository : IRepository<Image>
    {
        Task Add(int productId, int colorId, string type, string pictureUrl);
    }
}
