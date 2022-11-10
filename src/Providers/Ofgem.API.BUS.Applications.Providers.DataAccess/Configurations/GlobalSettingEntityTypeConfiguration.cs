using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ofgem.API.BUS.Applications.Domain;

namespace Ofgem.API.BUS.Applications.Providers.DataAccess.Configurations;

public class GlobalSettingEntityTypeConfiguration : IEntityTypeConfiguration<GlobalSetting>
{
    public void Configure(EntityTypeBuilder<GlobalSetting> builder)
    {
        builder
            .HasKey(b => b.ID);

        builder
            .Property(b => b.NextApplicationReferenceNumber)
            .UseIdentityColumn(10000000, 1);

        builder
            .Property(b => b.GeneratedByID)
            .IsRequired();
    }
}
