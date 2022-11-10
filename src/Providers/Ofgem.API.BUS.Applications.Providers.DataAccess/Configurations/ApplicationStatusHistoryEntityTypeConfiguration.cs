using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ofgem.API.BUS.Applications.Domain;

namespace Ofgem.API.BUS.Applications.Providers.DataAccess.Configurations;

public class ApplicationStatusHistoryEntityTypeConfiguration : IEntityTypeConfiguration<ApplicationStatusHistory>
{
    public void Configure(EntityTypeBuilder<ApplicationStatusHistory> builder)
    {
        builder
            .HasKey(x => x.Id);

        builder
            .Property(x => x.ApplicationId)
            .IsRequired();

        builder
            .Property(x => x.ApplicationSubStatusId)
            .IsRequired();

        builder
            .Property(x => x.StartDateTime)
            .IsRequired();

        builder
            .HasOne(b => b.ApplicationSubStatus)
            .WithMany(b => b.ApplicationStatusHistories)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
