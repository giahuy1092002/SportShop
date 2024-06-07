using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IImageService
    {
        Task Add(int productId, int colorId, string type, string pictureUrl);
        Task Add(Image image);
    }
}
