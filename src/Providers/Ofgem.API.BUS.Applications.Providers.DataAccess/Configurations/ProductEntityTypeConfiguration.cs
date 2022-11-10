using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ofgem.API.BUS.Applications.Domain;

namespace Ofgem.API.BUS.Applications.Providers.DataAccess.Configurations;

public class ProductEntityTypeConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder
            .HasKey(b => b.Id);

        builder
            .Property(b => b.MCSProductName)
            .IsUnicode(false)
            .HasMaxLength(255)
            .IsRequired();

        builder
            .Property(b => b.MCSModelNumber)
            .IsUnicode(false)
            .HasMaxLength(255)
            .IsRequired();

        builder
            .Property(b => b.Manufacturer)
            .IsUnicode(false)
            .HasMaxLength(255)
            .IsRequired();

        builder
            .Property(b => b.TechnologyId)
            .IsUnicode(false)
            .HasMaxLength(255)
            .IsRequired();

        builder
            .Property(b => b.TechnologyDescription)
            .IsUnicode(false)
            .HasMaxLength(255)
            .IsRequired();

        builder
            .Property(b => b.ProductTypeId)
            .IsUnicode(false)
            .HasMaxLength(255)
            .IsRequired();

        builder
            .Property(b => b.ProductTypeDescription)
            .IsUnicode(false)
            .HasMaxLength(255)
            .IsRequired();

        builder
            .Property(b => b.SCOP35ToSCOP65)
            .IsRequired();

        builder
            .Property(b => b.CertifiedFrom)
            .IsRequired();

        builder
            .Property(b => b.CertifiedTo)
            .IsRequired();
    }
}
