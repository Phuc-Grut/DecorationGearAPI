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
    public class DistrictConfig : IEntityTypeConfiguration<Districts>
    {
        public void Configure(EntityTypeBuilder<Districts> builder)
        {
            builder.ToTable("Districts");
            builder.HasKey(k => k.DistrictId);
            builder.HasOne(a => a.Provinces)
                            .WithMany(p => p.Districts)
                            .HasForeignKey(a => a.ProvinceId)
                            .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
