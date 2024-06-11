using AutoMapper;
using Data;
using Data.Entities;
using Data.Interface;
using Data.ViewModel;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class UserAddressService : IUserAddressService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserAddressService(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<bool> ChangeDefault(int addressId, Guid userId)
        {
            var defaultAddress = await _unitOfWork.UserAddress.GetDefault(userId);
            var address = await _unitOfWork.UserAddress.GetById(addressId);
            defaultAddress.IsDefault = false;
            address.IsDefault = true;
            await _unitOfWork.UserAddress.Update(address);
            await _unitOfWork.UserAddress.Update(defaultAddress);
            return true;
        }

        public async Task<UserAddressDto> Create(Address address, Guid userId)
        {
            var user = await _unitOfWork.Accounts.GetUser(userId);
            bool isDefault = false;
            if(user.AddressBook.Count==0) isDefault = true;
            var userAddress = new UserAddress
            {
                User = user,
                AddressLine = address.AddressLine,
                City = address.City,
                Commune = address.Commune,
                District = address.District,
                IsDefault = isDefault,
                FirstName = address.FirstName,
                LastName = address.LastName,
                PhoneNumber = address.PhoneNumber
            };
            await _unitOfWork.UserAddress.Add(userAddress);
            return _mapper.Map<UserAddressDto>(userAddress);
        }

        public async Task Delete(int addressId)
        {
            var userAddress = await _unitOfWork.UserAddress.GetById(addressId);
            await _unitOfWork.UserAddress.Delete(userAddress);
        }

        public async Task<List<UserAddressDto>> GetByUser(Guid userId)
        {
            return await _unitOfWork.UserAddress.GetByUser(userId);
        }

        public async Task Update(Address address, int addressId)
        {
            var userAddress = await _unitOfWork.UserAddress.GetById(addressId);
            userAddress.FirstName = address.FirstName;
            userAddress.LastName = address.LastName;
            userAddress.PhoneNumber = address.PhoneNumber;
            userAddress.AddressLine = address.AddressLine;
            userAddress.City = address.City;
            userAddress.Commune= address.Commune;
            userAddress.District = address.District;
            await _unitOfWork.UserAddress.Update(userAddress);
        }
    }
}
