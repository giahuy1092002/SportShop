﻿using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.ViewModel
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<SubCategoryDto> SubCategories { get; set; }
    }
}
