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
    public class SubCategoryRepository : Repository<SubCategory>,ISubCategoryRepository
    {
        private readonly IMapper _mapper;

        public SubCategoryRepository(SportStoreContext context,IMapper mapper):base(context)
        {
            _mapper = mapper;
        }

        public async Task<List<SubCategoryDto>> GetByGender(int genderId)
        {
            var subCategorys = await Entities
                .Where(i=>i.Category.GenderId== genderId)
                .ToListAsync();
            var result = _mapper.Map<List<SubCategoryDto>>(subCategorys);
            return result;
        }

        public async Task<SubCategory> GetSubCategory(int subcategoryId)
        {
            return await Entities
                .Include(s=>s.Category)
                .ThenInclude(c=>c.Gender)
                .FirstOrDefaultAsync(s => s.Id == subcategoryId);
        }
    }
}
