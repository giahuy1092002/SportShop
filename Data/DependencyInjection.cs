using Data.AutoMapper;
using Data.Interface;
using Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Data
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddRepository(this IServiceCollection services)
        {
            services.AddTransient(typeof(IUnitOfWork), typeof(UnitOfWork));
            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddAutoMapper(typeof(AutoMapperProfile));
            services.AddTransient<IProductSKURepository, ProductSKURepository>();
            services.AddTransient<IImageRepository, ImageRepository>();
            services.AddTransient<ICartRepository, CartRepository>();
            services.AddTransient<IAccountRepository, AccountRepository>();
            services.AddTransient<IOrderRepository, OrderRepository>();
            services.AddTransient<IUserAddressRepository, UserAddressRepository>();
            services.AddTransient<ISubCategoryRepository,SubCategoryRepository>();
            services.AddTransient<ICategoryRepository, CategoryRepository>();
            return services;
        }
    }

}
