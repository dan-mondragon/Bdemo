using Brio.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Brio.Data.Configuration
{
    public class BrandConfiguration
    {
        public BrandConfiguration(EntityTypeBuilder<Brand> entity)
        {
            entity.Property(e => e.Name).IsRequired().HasMaxLength(50);
        }
    }
}
