﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ofgem.API.BUS.Applications.Domain;

namespace Ofgem.API.BUS.Applications.Providers.DataAccess.Configurations;

public class VoucherStatusEntityTypeConfiguration : IEntityTypeConfiguration<VoucherStatus>
{
    public void Configure(EntityTypeBuilder<VoucherStatus> builder)
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
            .IsUnicode(false)
            .IsRequired();

        builder
            .Property(b => b.DisplayName)
            .IsUnicode(false)
            .HasMaxLength(255)
            .IsRequired(false);

        builder
            .HasMany(b => b.VoucherSubStatus)
            .WithOne(b => b.VoucherStatus);
    }
}
