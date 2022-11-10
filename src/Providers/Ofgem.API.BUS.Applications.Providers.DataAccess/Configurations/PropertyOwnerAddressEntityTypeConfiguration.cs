using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ofgem.API.BUS.Applications.Domain;

namespace Ofgem.API.BUS.Applications.Providers.DataAccess.Configurations;

public class PropertyOwnerAddressEntityTypeConfiguration : IEntityTypeConfiguration<PropertyOwnerAddress>
{
    public void Configure(EntityTypeBuilder<PropertyOwnerAddress> builder)
    {
        builder
            .HasKey(b => b.ID);

        builder
            .Property(b => b.AddressLine1)
            .IsUnicode(false)
            .HasMaxLength(100);

        builder
            .Property(b => b.AddressLine2)
            .IsUnicode(false)
            .HasMaxLength(100);

        builder
            .Property(b => b.AddressLine3)
            .IsUnicode(false)
            .HasMaxLength(100);

        builder
            .Property(b => b.County)
            .IsUnicode(false)
            .HasMaxLength(100);

        builder
            .Property(b => b.Postcode)
            .IsUnicode(false)
            .HasMaxLength(12)
            .IsRequired();

        builder.Property(b => b.UPRN)
            .IsUnicode(false)
            .HasMaxLength(12);

        builder.Property(b => b.Country)
            .IsUnicode(false)
            .HasMaxLength(60);
    }
}
