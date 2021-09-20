using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Needs.API.Entities
{
    public class Product : IEntityTypeConfiguration<Product>
    {
        public string Id { get; set; }
        
        public string ProductName { get; set; }
        
        public decimal Price { get; set; }

        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(x => x.Id);
        }
    }
}
