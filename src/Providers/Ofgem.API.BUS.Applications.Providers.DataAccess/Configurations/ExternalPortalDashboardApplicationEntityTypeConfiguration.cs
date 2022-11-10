using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ofgem.API.BUS.Applications.Domain.Entities.Views;

namespace Ofgem.API.BUS.Applications.Providers.DataAccess.Configurations;

public class ExternalPortalDashboardApplicationEntityTypeConfiguration : IEntityTypeConfiguration<ExternalPortalDashboardApplication>
{
    public void Configure(EntityTypeBuilder<ExternalPortalDashboardApplication> builder)
    {
        builder
            .HasNoKey();

        builder
            .ToView("vw_External_Dashboard_Applications");

        builder
            .Property(b => b.ApplicationStatusCode)
            .HasConversion<string>();

        builder
            .Property(b => b.VoucherStatusCode)
            .HasConversion<string>();

        builder
            .Property(b => b.ConsentState)
            .HasConversion<string>();
    }
}
