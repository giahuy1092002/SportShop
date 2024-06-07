using Data.Entities;
using Data.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IUserAddressService
    {
        Task<UserAddressDto> Create(Address address, Guid userId);
        Task Update(Address address, int addressId);
        Task Delete(int addressId);
        Task<bool> ChangeDefault(int addressId, Guid userId);
        Task<List<UserAddressDto>> GetByUser(Guid userId);
    }
}
