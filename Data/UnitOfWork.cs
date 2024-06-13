using AutoMapper;
using Data.DataContext;
using Data.Entities;
using Data.Interface;
using Data.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly SportStoreContext _dbContext;
        private readonly IProductRepository _productRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IProductSKURepository _productSKURepository;
        private readonly ICartRepository _cartRepository;
        private readonly IUserAddressRepository _userAddressRepository;
        private readonly IImageRepository _imageRepository;
        private readonly ISubCategoryRepository _subCategoryRepository;
        private readonly ICategoryRepository _categoryRepository;
        private IDbContextTransaction? _transaction = null;
        private readonly UserManager<User> _userManager;
        private readonly IUrlHelper _urlHelper;
        public UnitOfWork(SportStoreContext dbContext,IMapper mapper,UserManager<User> userManager,IUrlHelper urlHelper)
        {
            _dbContext = dbContext;
            _urlHelper = urlHelper;
            _userManager = userManager;
            _productRepository = new ProductRepository(_dbContext, mapper,this);
            _accountRepository = new AccountRepository(_dbContext, _userManager, _urlHelper);
            _orderRepository = new OrderRepository(_dbContext, mapper);
            _productSKURepository = new ProductSKURepository(_dbContext);
            _cartRepository = new CartRepository(_dbContext,this);
            _userAddressRepository = new UserAddressRepository(_dbContext, mapper);
            _imageRepository = new ImageRepository(_dbContext);
            _subCategoryRepository = new SubCategoryRepository(_dbContext,mapper);
            _categoryRepository = new CategoryRepository(_dbContext, mapper);
        }
        public IProductRepository Products => _productRepository;
        public IAccountRepository Accounts => _accountRepository;
        public IOrderRepository Orders => _orderRepository;
        public IProductSKURepository ProductSKUs => _productSKURepository;
        public IUserAddressRepository UserAddress => _userAddressRepository;
        public ICartRepository Carts => _cartRepository;
        public IImageRepository Images => _imageRepository;
        public ISubCategoryRepository SubCategory => _subCategoryRepository;
        public ICategoryRepository Category => _categoryRepository;
        public int SaveChanges()
        {
            return _dbContext.SaveChanges();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
            GC.SuppressFinalize(this);
        }

        public void BeginTransaction()
        {
            _transaction = _dbContext.Database.BeginTransaction();
        }

        public void CommitTransaction()
        {
            if (_transaction != null)
            {
                _transaction.Commit();
                _transaction.Dispose();
            }
        }

        public void RollbackTransaction()
        {
            if (_transaction != null)
            {
                _transaction.Rollback();
                _transaction.Dispose();
            }
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }
    }
}
