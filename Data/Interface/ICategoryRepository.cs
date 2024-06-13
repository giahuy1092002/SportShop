using Data.Entities;
using Data.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interface
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<List<CategoryDto>> GetByGender(int genderId);
    }
}
