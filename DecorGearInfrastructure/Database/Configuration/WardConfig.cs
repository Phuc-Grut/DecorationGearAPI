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
    public class WardConfig : IEntityTypeConfiguration<Wards>
    {
        public void Configure(EntityTypeBuilder<Wards> builder)
        {
            builder.ToTable("Wards");
            builder.HasKey(k => k.WardId);
            builder.HasOne(a => a.Districts)
                            .WithMany(p => p.Wards)
                            .HasForeignKey(a => a.DistrictId)
                            .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
