using Data.DataContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly SportStoreContext _dbContext;
        public DbSet<TEntity> Entities { get; }
        public Repository(SportStoreContext dbContext)
        {
            _dbContext = dbContext;
            Entities = _dbContext.Set<TEntity>();
        }

        public async Task<TEntity> GetById(int id)
        {
            return await Entities.FindAsync(id);
        }
        public async Task<TEntity> GetById(Guid id)
        {
            return await Entities.FindAsync(id);
        }

        public async Task<List<TEntity>> GetAll()
        {
            return await Entities.ToListAsync();
        }

        public async Task Add(TEntity entity)
        {
            Entities.Add(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(TEntity entity)
        {
            Entities.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Update(TEntity entity)
        {
            Entities.Update(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
