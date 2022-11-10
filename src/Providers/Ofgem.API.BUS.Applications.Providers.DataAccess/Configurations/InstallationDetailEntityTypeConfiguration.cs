using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ofgem.API.BUS.Applications.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ofgem.API.BUS.Applications.Providers.DataAccess.Configurations;

public class InstallationDetailEntityTypeConfiguration : IEntityTypeConfiguration<InstallationDetail>
{
    public void Configure(EntityTypeBuilder<InstallationDetail> builder)
    {
        builder
            .HasKey(b => b.Id);

        builder
            .Property(b => b.MCSProductCode)
            .IsUnicode(false)
            .HasMaxLength(100);

        builder
            .Property(b => b.SCOP)
            .IsUnicode(false)
            .HasMaxLength(100);

        builder
            .Property(b => b.SystemDesignedToProvideId)
            .IsUnicode(false)
            .HasMaxLength(10);

        builder
            .Property(b => b.SystemDesignedToProvideDescription)
            .IsUnicode(false)
            .HasMaxLength(100);

        builder
            .Property(b => b.AlternativeHeatingSystemId)
            .IsUnicode(false)
            .HasMaxLength(10);

        builder
            .Property(b => b.AlternativeHeatingSystemDescription)
            .IsUnicode(false)
            .HasMaxLength(100);

        builder
            .Property(b => b.AlternativeHeatingFuelDescription)
            .IsUnicode(false)
            .HasMaxLength(100);

        builder
            .Property(b => b.ProductManufacturer)
            .IsUnicode(false)
            .HasMaxLength(100);

        builder
            .Property(b => b.MCSCertifiedProductName)
            .IsUnicode(false)
            .HasMaxLength(100);

        builder
            .Property(b => b.Capacity)
            .IsUnicode(false)
            .HasMaxLength(100);

        builder
            .Property(b => b.TotalInstallationCost)
            .HasPrecision(8, 2);
    }
}
