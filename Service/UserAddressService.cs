using AutoMapper;
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
        private readonly IUserAddressRepository _userAddressRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IMapper _mapper;

        public UserAddressService(IUserAddressRepository userAddressRepository,
            IAccountRepository accountRepository,
            IMapper mapper)
        {
            _userAddressRepository = userAddressRepository;
            _accountRepository = accountRepository;
            _mapper = mapper;
        }

        public async Task<bool> ChangeDefault(int addressId, Guid userId)
        {
            var defaultAddress = await _userAddressRepository.GetDefault(userId);
            var address = await _userAddressRepository.GetById(addressId);
            defaultAddress.IsDefault = false;
            address.IsDefault = true;
            await _userAddressRepository.Update(address);
            await _userAddressRepository.Update(defaultAddress);
            return true;
        }

        public async Task<UserAddressDto> Create(Address address, Guid userId)
        {
            var user = await _accountRepository.GetUser(userId);
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
            await _userAddressRepository.Add(userAddress);
            return _mapper.Map<UserAddressDto>(userAddress);
        }

        public async Task Delete(int addressId)
        {
            var userAddress = await _userAddressRepository.GetById(addressId);
            await _userAddressRepository.Delete(userAddress);
        }

        public async Task<List<UserAddressDto>> GetByUser(Guid userId)
        {
            return await _userAddressRepository.GetByUser(userId);
        }

        public async Task Update(Address address, int addressId)
        {
            var userAddress = await _userAddressRepository.GetById(addressId);
            userAddress.FirstName = address.FirstName;
            userAddress.LastName = address.LastName;
            userAddress.PhoneNumber = address.PhoneNumber;
            userAddress.AddressLine = address.AddressLine;
            userAddress.City = address.City;
            userAddress.Commune= address.Commune;
            userAddress.District = address.District;
            await _userAddressRepository.Update(userAddress);
        }
    }
}
