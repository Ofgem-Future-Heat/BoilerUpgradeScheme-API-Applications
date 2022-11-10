using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ofgem.API.BUS.Applications.Domain;
using Ofgem.Lib.BUS.AuditLogging.Configurations;
using Ofgem.Lib.BUS.AuditLogging.Domain.Entities;

namespace Ofgem.API.BUS.Applications.Providers.DataAccess.Configurations;

/// <summary>
/// Configures the <see cref="AuditLog"/> database entity.
/// </summary>
public class AuditLogEntityTypeConfiguration : BaseAuditLogEntityTypeConfiguration
{
    public override void Configure(EntityTypeBuilder<AuditLog> builder)
    {
        base.Configure(builder);

        builder.HasOne<Application>()
               .WithMany()
               .HasForeignKey("EntityId");
    }
}
