using Data.Entities;
using Data.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interface
{
    public interface ISubCategoryRepository : IRepository<SubCategory>
    {
        Task<List<SubCategoryDto>> GetByGender(int genderId);
        Task<SubCategory> GetSubCategory(int subcategoryId);
    }
}
