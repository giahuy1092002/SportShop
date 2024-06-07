
using Data.Entities;
using Data.Interface;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class ImageService : IImageService
    {
        private readonly IImageRepository _imageRepository;

        public ImageService(IImageRepository imageRepository)
        {
            _imageRepository = imageRepository;
        }
        public async Task Add(int productId, int colorId, string type, string pictureUrl)
        {
           await _imageRepository.Add(productId,colorId,type,pictureUrl);
        }
        public async Task Add(Image image)
        {
            await _imageRepository.Add(image);
        }

    }
}
