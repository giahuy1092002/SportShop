﻿using Data.Entities;
using Data.Model;
using Data.RequestHelper;
using Data.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IProductService
    {
        Task<List<ProductListDto>> GetProducts(ProductParams productParams);
        Task<ProductColorDetail> GetProductColorDetail(int productId, int colorId);
        Task<List<Product>> GetProducts();
        Task<bool> AddColor(AddProductColorModel createProduct, int productId);
        Task<bool> Create(CreateProductModel createProduct);
    }
}
