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
    public class CustomerAddressesConfig : IEntityTypeConfiguration<CustomerAddresses>
    {
        public void Configure(EntityTypeBuilder<CustomerAddresses> builder)
        {
            builder.ToTable("CustomerAddresses");
            builder.HasKey(k => k.AddressId);
            builder.HasOne(a => a.Provinces)
                            .WithMany(p => p.CustomerAddresses)
                            .HasForeignKey(a => a.ProvinceId)
                            .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(a => a.Districts)
                            .WithMany(p => p.CustomerAddresses)
                            .HasForeignKey(a => a.DistrictId)
                            .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(a => a.Wards)
                            .WithMany(p => p.CustomerAddresses)
                            .HasForeignKey(a => a.WardId)
                            .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(a => a.User)
                            .WithMany(p => p.CustomerAddresses)
                            .HasForeignKey(a => a.UserID)
                            .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
