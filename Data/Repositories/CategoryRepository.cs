using AutoMapper;
using Data.DataContext;
using Data.Entities;
using Data.Interface;
using Data.ViewModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly IMapper _mapper;

        public CategoryRepository(SportStoreContext context,IMapper mapper):base(context)
        {
            _mapper = mapper;
        }
        public async Task<List<CategoryDto>> GetByGender(int genderId)
        {
            var categories = await Entities
                .Include(c=>c.SubCategories)
                .Where(c=>c.GenderId==genderId)
                .ToListAsync();
            return _mapper.Map<List<CategoryDto>>(categories);
        }
    }
}
