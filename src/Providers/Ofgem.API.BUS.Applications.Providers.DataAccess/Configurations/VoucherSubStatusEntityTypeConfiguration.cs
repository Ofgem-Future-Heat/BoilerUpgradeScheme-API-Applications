using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ofgem.API.BUS.Applications.Domain;

namespace Ofgem.API.BUS.Applications.Providers.DataAccess.Configurations;

public class VoucherSubStatusEntityTypeConfiguration : IEntityTypeConfiguration<VoucherSubStatus>
{
    public void Configure(EntityTypeBuilder<VoucherSubStatus> builder)
    {
        builder
            .HasKey(b => b.Id);

        builder
            .Property(b => b.Code)
            .HasConversion<string>()
            .HasMaxLength(32)
            .IsRequired();

        builder
            .Property(b => b.DisplayName)
            .HasMaxLength(255)
            .IsRequired();

        builder
            .Property(b => b.Description)
            .HasMaxLength(255)
            .IsRequired();

        builder
            .Property(b => b.SortOrder)
            .IsRequired();

        builder
            .Property(b => b.VoucherStatusId)
            .IsRequired();
    }
}
