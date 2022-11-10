using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ofgem.API.BUS.Applications.Domain;

namespace Ofgem.API.BUS.Applications.Providers.DataAccess.Configurations;

/// <summary>
/// Entity configuration for the TechType type for EF Core
/// </summary>
public class TechTypeEntityTypeConfiguration : IEntityTypeConfiguration<TechType>
{
    public void Configure(EntityTypeBuilder<TechType> builder)
    {
        builder
            .HasKey(b => b.ID);

        builder
            .Property(b => b.TechTypeDescription)
            .IsUnicode(false)
            .HasMaxLength(127)
            .IsRequired();
    }
}
