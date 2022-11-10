using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ofgem.API.BUS.Applications.Domain;

namespace Ofgem.API.BUS.Applications.Providers.DataAccess.Configurations;

public class ApplicationStatusEntityTypeConfiguration : IEntityTypeConfiguration<ApplicationStatus>
{
    public void Configure(EntityTypeBuilder<ApplicationStatus> builder)
    {
        builder
            .HasKey(b => b.Id);

        builder
            .Property(b => b.Code)
            .HasConversion<string>()
            .IsUnicode(false)
            .HasMaxLength(32)
            .IsRequired();

        builder
            .Property(b => b.Description)
            .IsUnicode(false)
            .HasMaxLength(255)
            .IsRequired();

        builder
            .Property(b => b.SortOrder)
            .IsRequired();

        builder
            .HasMany(b => b.ApplicationSubStatus)
            .WithOne(b => b.ApplicationStatus);

        builder
            .Property(b => b.DisplayName)
            .IsUnicode(false)
            .HasMaxLength(255)
            .IsRequired(false);
    }
}
