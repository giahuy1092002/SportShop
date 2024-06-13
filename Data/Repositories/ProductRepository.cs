using AutoMapper;
using Data.DataContext;
using Data.Entities;
using Data.Extensions;
using Data.Interface;
using Data.Model;
using Data.RequestHelper;
using Data.ViewModel;
using MailKit.Search;
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

        public ProductRepository(SportStoreContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }
        public async Task<ProductColorDetail> GetProductColorDetail(int productId, int colorId)
        {
            var product = await Entities
                .Include(p => p.Skus)
                .ThenInclude(p => p.Color)
                .Include(p => p.Skus)
                .ThenInclude(p => p.Size)
                .Include(p => p.Images)
                .FirstOrDefaultAsync(p => p.Id == productId);
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

        public async Task<List<ProductListDto>> GetProducts(ProductParams productParams,int subCategoryId)
        {
            var query = Entities
                .Include(p => p.Skus)
                    .ThenInclude(p => p.Color)
                .Include(p => p.Skus)
                    .ThenInclude(p => p.Size)
                .Include(p => p.Images)
                .Where(p=>p.SubCategoryId==subCategoryId)
                .AsQueryable();
            var products = await query
                .Filter(productParams.Colors, productParams.Sizes)
                .ToListAsync();
            var productDtos = products
           .SelectMany(p => p.Skus
               .GroupBy(s => s.ColorId)
               .Select(g => new ProductListDto
               {
                   Id = p.Id,
                   Name = p.Name,
                   ColorId = g.Key,
                   PictureUrl = p.Images.FirstOrDefault(i => i.ColorId == g.Key && i.Type == "Anh chinh")?.PictureUrl,
                   Price = g.FirstOrDefault().Price,
                   ImageColors = p.Skus
                       .GroupBy(s => s.ColorId)
                       .Select(gc => new ImageColorDto
                       {
                           ColorId = gc.Key,
                           PictureUrl = p.Images.FirstOrDefault(i => i.ColorId == gc.Key && i.Type == "Anh chinh")?.PictureUrl,
                           Price = p.Skus.FirstOrDefault(sku=>sku.ColorId==gc.Key).Price,
                       }).ToList()
               })
           )
           .ToList();
            productDtos = productParams.OrderBy switch
            {
                "$-$$$" => productDtos.OrderBy(p => p.Price).ToList(),
                "$$$-$" => productDtos.OrderByDescending(p => p.Price).ToList(),
                "A-Z" => productDtos.OrderBy(p => p.Name).ToList(),
                "Z-A" => productDtos.OrderByDescending(p => p.Name).ToList(),
            };
            return productDtos;

        }
        public async Task<List<Product>> GetAll(ProductParams productParams,int subCategoryId)
        {
            var query = Entities
                .Include(p => p.Skus)
                    .ThenInclude(p => p.Color)
                .Include(p => p.Skus)
                    .ThenInclude(p => p.Size)
                .Include(p => p.Images)
                .Where(p => p.SubCategoryId == subCategoryId)
                .AsQueryable();
            var products = await query
                .Filter(productParams.Colors, productParams.Sizes)
                .Sort(productParams.OrderBy).ToListAsync();
            return products;
        }
        public async Task<Product> GetProduct(int productId)
        {
            var product = await Entities
                .Include(p => p.Skus)
                .FirstOrDefaultAsync(p => p.Id == productId);
            return product;
        }

        public async Task<List<ProductSearchDto>> GetByName(string name)
        {
            if (name.Length < 3) return null;
            //var products = await Entities
            //    .Where(p => p.Name.ToLower().Contains(name.ToLower()))
            //    .Select(p => new ProductSearchDto
            //    {
            //        Id = p.Id,
            //        Name = p.Name,
            //        Price
            //    })
            //    .Take(5)
            //    .ToListAsync();
            var query = Entities
                .Include(p => p.Skus)
                    .ThenInclude(p => p.Color)
                .Include(p => p.Skus)
                    .ThenInclude(p => p.Size)
                .Include(p => p.Images)
                .Where(p => p.Name.ToLower().Contains(name.ToLower()))
                .AsQueryable();
            var productDtos = query
           .SelectMany(p => p.Skus
               .GroupBy(s => s.ColorId)
               .Select(g => new ProductSearchDto
               {
                   Id = p.Id,
                   Name = p.Name,
                   ColorId = g.Key,
                   PictureUrl = p.Images.FirstOrDefault(i=>i.ColorId==g.Key&&i.Type=="Anh chinh").PictureUrl,
                   Price = g.FirstOrDefault().Price,
               })
           ).Take(5).ToList();
            return productDtos;
        }
    }
}
