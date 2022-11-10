using Ofgem.API.BUS.Applications.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ofgem.API.BUS.Applications.Providers.DataAccess.Configurations;

/// <summary>
/// Entity configuration for the Feedback type for EF Core
/// </summary>
public class FeedbackEntityTypeConfiguration : IEntityTypeConfiguration<Feedback>
{
    public void Configure (EntityTypeBuilder<Feedback> builder)
    {
        builder
            .HasKey(b => b.ID);

        builder
            .Property(b => b.ApplicationID)
            .IsRequired();

        builder
            .Property(b => b.SurveyOptionId)
            .IsRequired();

        builder
            .Property(b => b.AppliesTo)
            .HasConversion<int>()
            .IsRequired();

        builder
            .Property(b => b.FedbackOn)
            .IsRequired();
    }
}
