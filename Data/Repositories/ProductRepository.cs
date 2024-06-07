using AutoMapper;
using Data.DataContext;
using Data.Entities;
using Data.Extensions;
using Data.Interface;
using Data.RequestHelper;
using Data.ViewModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly IMapper _mapper;

        public ProductRepository(SportStoreContext context,IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }
        public async Task<ProductColorDetail> GetProductColorDetail(int productId,int colorId)
        {
            var product = await Entities
                .Include(p => p.Skus)
                .ThenInclude(p => p.Color)
                .Include(p => p.Skus)
                .ThenInclude(p => p.Size)
                .Include(p => p.Images)
                .FirstOrDefaultAsync(p=>p.Id==productId);
            var images = product.Images
                .Where(i => i.ProductId == productId && i.ColorId == colorId)
                .ToList();
            var imageDto = _mapper.Map<List<ImageDto>>(images);
            var color = product.Skus
                .Select(sku => sku.Color)
                .FirstOrDefault(c => c.Id == colorId);
            var colors = product.Skus
                .Select(sku => sku.Color)
                .Distinct()
                .ToList();
            var sizes = product.Skus
                .Where(sku => sku.ColorId == colorId)
                .Select(sku => sku.Size)
                .Distinct()
                .ToList();
            var imagesColor = product.Images.Where(i => i.Type == "Anh chinh");
            var imagesColorDtos = _mapper.Map<List<ImageColorDto>>(imagesColor);
            var productSKu = product.Skus
               .Where(p => p.ColorId == colorId)
               .ToList();
            var options = _mapper.Map<List<ProductSKUOptionDto>>(productSKu);
            var productDetail = new ProductColorDetail
            {
                Name = product.Name,
                Price = product.Skus[0].Price,
                Color = color.Name,
                Images = imageDto,
                Colors = colors,
                ImagesColors = imagesColorDtos,
                Options = options,
            };
            return productDetail;
        }

        public async Task<List<ProductListDto>> GetProducts(ProductParams productParams)
        {
            var query = Entities
                .Include(p => p.Skus)
                    .ThenInclude(p => p.Color)
                .Include(p => p.Skus)
                    .ThenInclude(p => p.Size)
                .Include(p => p.Images)
                .AsQueryable();
            var products = await query.Sort(productParams.OrderBy).ToListAsync();
            var productDtos = products.Select(x => new ProductListDto
            {
                Id = x.Id,
                Name = x.Name,
                ImageColors = x.Skus.Select(p => new ImageColorDto
                {
                    ColorId = p.ColorId,
                    PictureUrl = p.Product.Images.FirstOrDefault(i => i.ColorId == p.ColorId && i.Type == "Anh chinh").PictureUrl
                }).Distinct(new ImageColorDtoCompare()).ToList(),
                Price = x.Skus[0].Price,
            }).ToList();
            return productDtos;

        }
        public async Task<List<Product>> GetProducts()
        {
            var products = await Entities
                .Include(p => p.Skus)
                    .ThenInclude(p => p.Color)
                .Include(p => p.Skus)
                    .ThenInclude(p => p.Size)
                .ToListAsync();
            return products;
        }
    }
}
