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
    public class UserAddressRepository : Repository<UserAddress>,IUserAddressRepository
    {
        private readonly IMapper _mapper;

        public UserAddressRepository(SportStoreContext context,IMapper mapper):base(context)
        {
            _mapper = mapper;
        }

        public async Task<List<UserAddressDto>> GetByUser(Guid userId)
        {
            var listAddress =  await Entities.Where(a => a.UserId == userId).ToListAsync();
            var addressDto = _mapper.Map<List<UserAddressDto>>(listAddress);
            return addressDto;
        }

        public async Task<UserAddress> GetDefault(Guid userId)
        {
            var address = await Entities.FirstOrDefaultAsync(a => a.UserId == userId && a.IsDefault == true);
            return address;
        }
    }
}
