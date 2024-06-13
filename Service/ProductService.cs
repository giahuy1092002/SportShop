using Service.Interface;
using Data.Entities;
using Data.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.ViewModel;
using Data.RequestHelper;
using Data;
using Data.Model;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Common.Exceptions;

namespace Service
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ImageService _imageService;

        public ProductService(IUnitOfWork unitOfWork,IMapper mapper,ImageService imageService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _imageService = imageService;
        }

        public async Task<bool> AddColor(AddProductColorModel createProduct, int productId)
        {
            var product = await _unitOfWork.Products.GetProduct(productId);
            var result = product.Skus.FirstOrDefault(s => s.ColorId == createProduct.ColorId);
            if (result != null) throw new DuplicateException("Color is exist");
            for (int i = 0; i < createProduct.PictureUrls.Count; i++)
            {
                if (createProduct.PictureUrls[i] != null)
                {
                    var imageResult = await _imageService.AddImageAsync(createProduct.PictureUrls[i]);
                    var image = new Image
                    {
                        Product = product,
                        ColorId = createProduct.ColorId,
                        PictureUrl = imageResult.SecureUrl.ToString(),
                        Type = i == 0 ? "Anh chinh" : "Anh phu",
                    };
                    await _unitOfWork.Images.Add(image);
                }
            }
            foreach (var item in createProduct.Sizes)
            {
                var productSKU = new ProductSKU
                {
                    Product = product,
                    ColorId = createProduct.ColorId,
                    SizeId = item,
                    Price = createProduct.Price,
                    QuantityInStock = 50,
                    Sku = "123456"
                };
                await _unitOfWork.ProductSKUs.Add(productSKU);
            }
            return true;
        }

        public async Task<bool> Create(CreateProductModel createProduct)
        {
            var product = _mapper.Map<Product>(createProduct);
            await _unitOfWork.Products.Add(product);
            for (int i=0;i<createProduct.PictureUrls.Count;i++)
            {
                if (createProduct.PictureUrls[i] != null)
                {
                    var imageResult = await _imageService.AddImageAsync(createProduct.PictureUrls[i]);
                    var image = new Image
                    {
                        Product = product,
                        ColorId = createProduct.ColorId,
                        PictureUrl = imageResult.SecureUrl.ToString(),
                        Type = i==0 ? "Anh chinh" : "Anh phu",
                    }; 
                    await _unitOfWork.Images.Add(image);
                }
            }
            foreach(var item in createProduct.Sizes)
            {
                var productSKU = new ProductSKU
                {
                    Product = product,
                    ColorId = createProduct.ColorId,
                    SizeId = item,
                    Price = createProduct.Price,
                    QuantityInStock = 50,
                    Sku = "123456"
                };
                await _unitOfWork.ProductSKUs.Add(productSKU);
            }
            return true;
        }

        public async Task<ProductColorDetail> GetProductColorDetail(int productId,int colorId)
        {
            return await _unitOfWork.Products.GetProductColorDetail(productId,colorId);
        }

        public async Task<PagedList<ProductListDto>> GetProducts(ProductParams productParams, int subCategoryId)
        {
            var products = await _unitOfWork.Products.GetProducts(productParams,subCategoryId);
            return products;
        }

        public async Task<List<Product>> GetAll(ProductParams productParams, int subCategoryId)
        {
            return await _unitOfWork.Products.GetAll(productParams,subCategoryId);
        }
    }
}
