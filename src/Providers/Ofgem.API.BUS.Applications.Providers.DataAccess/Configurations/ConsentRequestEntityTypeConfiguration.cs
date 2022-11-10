using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ofgem.API.BUS.Applications.Domain;

namespace Ofgem.API.BUS.Applications.Providers.DataAccess.Configurations;

/// <summary>
/// Entity configuration for the ConsentRequest type for EF Core
/// </summary>
public class ConsentRequestEntityTypeConfiguration : IEntityTypeConfiguration<ConsentRequest>
{
    public void Configure(EntityTypeBuilder<ConsentRequest> builder)
    {
        builder
            .HasKey(b => b.ID);

        builder
            .Property(b => b.ApplicationID)
            .IsRequired();

        builder
            .Property(b => b.ConsentIssuedDate)
            .IsRequired(false);

        builder
            .Property(b => b.ConsentExpiryDate)
            .IsRequired(false);

        builder
            .Property(b => b.CreatedDate)
            .IsRequired();

        builder
            .Property(b => b.CreatedBy)
            .IsUnicode(false)
            .HasMaxLength(100)
            .IsRequired();

        builder
            .Property(b => b.LastUpdatedDate)
            .IsRequired(false);

        builder
            .Property(b => b.LastUpdatedBy)
            .IsUnicode(false)
            .HasMaxLength(100)
            .IsRequired(false);
    }
}
