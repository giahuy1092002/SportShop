﻿using Data.Entities.OrderAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class UserAddress : Address
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public bool IsDefault { get; set; }
    }
}
