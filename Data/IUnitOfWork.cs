using Data.Entities;
using Data.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{

    public interface IUnitOfWork : IDisposable
    {
        IProductRepository Products { get; }
        IAccountRepository Accounts { get; }
        IOrderRepository Orders { get; }
        IProductSKURepository ProductSKUs { get; }
        IUserAddressRepository UserAddress { get; }
        ICartRepository Carts { get; }
        IImageRepository Images { get; }
        ISubCategoryRepository SubCategory { get; }
        ICategoryRepository Category { get; }
        void CommitTransaction();
        void RollbackTransaction();
        int SaveChanges();
        void BeginTransaction();

        Task<int> SaveChangesAsync();

    }

}
