using Ofgem.API.BUS.Applications.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ofgem.API.BUS.Applications.Providers.DataAccess.Configurations;

/// <summary>
/// Entity configuration for the application type for EF Core
/// </summary>
public class ApplicationEntityTypeConfiguration : IEntityTypeConfiguration<Application>
{
    public void Configure (EntityTypeBuilder<Application> builder)
    {
        builder
            .HasKey(b => b.ID);

        builder
            .Property(b => b.ReferenceNumber)
            .IsUnicode(false)
            .HasMaxLength(11)
            .IsRequired();

        builder
            .Property(b => b.BusinessAccountId)
            .IsRequired();

        builder
            .Property(b => b.QuoteReferenceNumber)
            .IsUnicode(false)
            .HasMaxLength(50);

        builder
            .Property(b => b.QuoteAmount)
            .HasPrecision(8, 2);

        builder
            .Property(b => b.QuoteProductPrice)
            .HasPrecision(8, 2);

        builder
            .Property(b => b.ReviewRecommendation)
            .IsUnicode(false)
            .HasMaxLength(10);

        builder
            .Ignore(b => b.ConsentState);

        builder
            .Property(b => b.SubmitterId)
            .IsRequired();

        builder
            .Property(b => b.PreviousFuelType)
            .IsUnicode(false)
            .HasMaxLength(20);

        builder
            .Property(b => b.FuelTypeOther)
            .IsUnicode(false)
            .HasMaxLength(100);

        builder
            .Property(b => b.PropertyType)
            .IsUnicode(false)
            .HasMaxLength(20);
    }
}
