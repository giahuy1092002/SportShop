using Data.DataContext;
using Data.Entities;
using Data.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class ImageRepository : Repository<Image>,IImageRepository
    {
        public ImageRepository(SportStoreContext context):base(context)
        {
            
        }

        public async Task Add(int productId, int colorId, string type, string pictureUrl)
        {
            var image = new Image
            {
                PictureUrl = pictureUrl,
                Type = type,
                ProductId = productId,
                ColorId = colorId,
            };
            await Entities.AddAsync(image);
            //_uow.SaveChanges();
        }
    }
}
