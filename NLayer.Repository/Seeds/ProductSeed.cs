using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NLayer.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Repository.Seeds
{
    public class ProductSeed : IEntityTypeConfiguration<ProductDto>
    {
        public void Configure(EntityTypeBuilder<ProductDto> builder)
        {
            builder.HasData(
                new ProductDto { Id = 1, CategoryId = 1, Name = "Kalem 1", Price = 200, Stock = 20, CreateDate = DateTime.Now },
                new ProductDto { Id = 2, CategoryId = 1, Name = "Kalem 2", Price = 1000, Stock = 60, CreateDate = DateTime.Now },
                new ProductDto { Id = 3, CategoryId = 2, Name = "Kitap 1", Price = 5200, Stock = 320, CreateDate = DateTime.Now },
                new ProductDto { Id = 4, CategoryId = 3, Name = "Defter 1", Price = 6200, Stock = 220, CreateDate = DateTime.Now }
                );
        }
    }
}
