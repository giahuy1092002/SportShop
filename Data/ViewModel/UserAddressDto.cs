using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.ViewModel
{
    public class UserAddressDto : Address
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public bool IsDefault { get; set; }
    }
}
