using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ofgem.API.BUS.Applications.Domain;

namespace Ofgem.API.BUS.Applications.Providers.DataAccess.Configurations;

public class EpcEntityTypeConfiguration : IEntityTypeConfiguration<Epc>
{
    public void Configure(EntityTypeBuilder<Epc> builder)
    {
        builder
            .HasKey(b => b.ID);

        builder
            .Property(b => b.EpcReferenceNumber)
            .IsUnicode(false)
            .HasMaxLength(24);
    }
}
