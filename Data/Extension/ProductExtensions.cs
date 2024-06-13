using Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Data.Extensions
{
    public static class ProductExtensions
    {
        public static IQueryable<Product> Sort(this IQueryable<Product> query, string orderBy)
        {
            if (string.IsNullOrEmpty(orderBy)) return query.OrderBy(p => p.Name);
            query = orderBy switch
            {
                "$-$$$" => query.Include(p => p.Skus).OrderBy(p => p.Skus.Min(sku => sku.Price)),
                "$$$-$" => query.Include(p => p.Skus).OrderByDescending(p => p.Skus.Min(sku => sku.Price)),
                "A-Z" => query.OrderBy(p => p.Name),
                "Z-A" => query.OrderByDescending(p => p.Name),
            };
            return query;
        }
        public static IQueryable<Product> Filter(this IQueryable<Product> query,string colors,string sizes)
        {
            var sizeList = new List<string>();
            var colorList = new List<string>();

            if (!string.IsNullOrEmpty(sizes))
            {
                sizeList.AddRange(sizes.ToLower().Split(",").ToList());
            }

            if (!string.IsNullOrEmpty(colors))
            {
                colorList.AddRange(colors.ToLower().Split(",").ToList());
            }
            if (sizeList.Any() && colorList.Any())
            {
                query = query.Where(p => p.Skus.Any(sku => colorList.Contains(sku.Color.Value) && sizeList.Contains(sku.Size.Value)));
            }
            else if (colorList.Any())
            {
                query = query.Where(p => p.Skus.Any(sku => colorList.Contains(sku.Color.Value)));
            }
            else if (sizeList.Any())
            {
                query = query.Where(p => p.Skus.Any(sku => sizeList.Contains(sku.Size.Value)));
            }

            return query;
        }
        //public static bool Compare(Product product,List<string> sizes,List<string> colors)
        //{
            
        //}
    }
}
