using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ofgem.API.BUS.Applications.Domain;

namespace Ofgem.API.BUS.Applications.Providers.DataAccess.Configurations;

/// <summary>
/// Entity configuration for the PropertyOwnerDetail type for EF Core
/// </summary>
public class PropertyOwnerDetailEntityTypeConfiguration : IEntityTypeConfiguration<PropertyOwnerDetail>
{
    public void Configure(EntityTypeBuilder<PropertyOwnerDetail> builder)
    {
        builder
            .HasKey(b => b.ID);

        builder
            .Property(b => b.FullName)
            .IsUnicode(false)
            .HasMaxLength(100);

        builder
            .Property(b => b.Email)
            .IsUnicode(false)
            .HasMaxLength(254);

        builder
            .Property(b => b.TelephoneNumber)
            .IsUnicode(false)
            .HasMaxLength(100);
    }
}
