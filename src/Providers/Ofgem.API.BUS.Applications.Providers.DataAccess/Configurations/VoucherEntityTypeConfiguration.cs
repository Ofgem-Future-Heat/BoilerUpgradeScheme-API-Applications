using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ofgem.API.BUS.Applications.Domain;

namespace Ofgem.API.BUS.Applications.Providers.DataAccess.Configurations;

public class VoucherEntityTypeConfiguration : IEntityTypeConfiguration<Voucher>
{
    public void Configure(EntityTypeBuilder<Voucher> builder)
    {
        builder
            .HasKey(b => b.ID);

        builder
            .Property(b => b.GrantId)
            .IsRequired();
    }
}
