﻿using Data.Entities;
using Data.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interface
{
    public interface IUserAddressRepository : IRepository<UserAddress>
    {
        Task<List<UserAddressDto>> GetByUser(Guid userId);
        Task<UserAddress> GetDefault(Guid userId);

    }
}
