using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ofgem.API.BUS.Applications.Domain;

namespace Ofgem.API.BUS.Applications.Providers.DataAccess.Configurations;

public class VoucherSubStatusHistoryEntityTypeConfiguration : IEntityTypeConfiguration<VoucherStatusHistory>
{
    public void Configure(EntityTypeBuilder<VoucherStatusHistory> builder)
    {
        builder
            .HasKey(x => x.Id);

        builder
            .Property(x => x.VoucherId)
            .IsRequired();

        builder
            .Property(x => x.VoucherSubStatusId)
            .IsRequired();

        builder
            .Property(x => x.StartDateTime)
            .IsRequired();

        builder
            .HasOne(b => b.VoucherSubStatus)
            .WithMany(b => b.VoucherStatusHistories)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
