using DecorGearDomain.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecorGearInfrastructure.Database.Configuration
{
    public class ProvinceConfig : IEntityTypeConfiguration<Provinces>
    {
        public void Configure(EntityTypeBuilder<Provinces> builder)
        {
            builder.ToTable("Provinces");
            builder.HasKey(k => k.ProvinceId);
        }
    }
}
