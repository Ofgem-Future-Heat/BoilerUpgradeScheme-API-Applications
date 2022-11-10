using Ofgem.API.BUS.Applications.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ofgem.API.BUS.Applications.Providers.DataAccess.Configurations;

/// <summary>
/// Entity configuration for the Feedback type for EF Core
/// </summary>
public class SurveyOptionEntityTypeConfiguration : IEntityTypeConfiguration<SurveyOption>
{
    public void Configure (EntityTypeBuilder<SurveyOption> builder)
    {
        builder
            .HasKey(b => b.ID);

        builder
            .Property(b => b.FeedbackOption)
            .IsRequired();
    }
}
