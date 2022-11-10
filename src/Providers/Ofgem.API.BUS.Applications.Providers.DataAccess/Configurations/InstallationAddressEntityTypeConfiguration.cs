using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ofgem.API.BUS.Applications.Domain;

namespace Ofgem.API.BUS.Applications.Providers.DataAccess.Configurations;

/// <summary>
/// Entity configuration for the InstallationAddress type for EF Core
/// </summary>
public class InstallationAddressEntityTypeConfiguration : IEntityTypeConfiguration<InstallationAddress>
{
    public void Configure(EntityTypeBuilder<InstallationAddress> builder)
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
            .HasMaxLength(8)
            .IsRequired();

        builder
            .Property(b => b.UPRN)
            .IsUnicode(false)
            .HasMaxLength(12);

        builder
            .Property(b => b.IsRural)
            .IsUnicode(false)
            .HasMaxLength(30);

        builder
            .Property(b => b.CountryCode)
            .IsUnicode(false)
            .HasMaxLength(10);
    }
}
