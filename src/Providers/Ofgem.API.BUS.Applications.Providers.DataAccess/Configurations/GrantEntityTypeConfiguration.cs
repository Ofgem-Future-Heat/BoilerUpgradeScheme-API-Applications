using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ofgem.API.BUS.Applications.Domain;

namespace Ofgem.API.BUS.Applications.Providers.DataAccess.Configurations;

/// <summary>
/// Entity configuration for the Grant type for EF Core
/// </summary>
public class GrantEntityTypeConfiguration : IEntityTypeConfiguration<Grant>
{
    public void Configure(EntityTypeBuilder<Grant> builder)
    {
        builder
            .HasKey(b => b.ID);

        // Precision is:  (total digits, decimal digits)
        builder
            .Property(b => b.Amount)
            .HasPrecision(8, 2)
            .IsRequired();
    }
}
