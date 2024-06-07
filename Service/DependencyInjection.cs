using Data.AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddService(this IServiceCollection services)
        {
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<IProductSKUService, ProductSKUService>();
            services.AddTransient<IImageService, ImageService>();
            services.AddTransient<ICartService, CartService>();
            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<IOrderService, OrderService>();   
            services.AddTransient<IUserAddressService, UserAddressService>();
            return services;
        }
    }
}
