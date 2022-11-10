using Microsoft.EntityFrameworkCore;
using Ofgem.API.BUS.Applications.Domain;
using Ofgem.API.BUS.Applications.Domain.Entities.Views;
using Ofgem.API.BUS.Applications.Domain.Interfaces;
using Ofgem.API.BUS.Applications.Providers.DataAccess.Configurations;
using Ofgem.Lib.BUS.AuditLogging.Domain.Entities;
using Ofgem.Lib.BUS.AuditLogging.Interfaces;

namespace Ofgem.API.BUS.Applications.Providers.DataAccess;

/// <summary>
/// DBContext for interrogating the database and datasets within
/// </summary>
public class ApplicationsDBContext : DbContext, IAuditLogsDbContext
{
    public ApplicationsDBContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Application> Applications { get; set; }
    public DbSet<ApplicationDashboard> ApplicationDashboards { get; set; }
    public DbSet<ApplicationStatus> ApplicationStatuses { get; set; }
    public DbSet<ApplicationStatusHistory> ApplicationStatusHistories { get; set; }
    public DbSet<ApplicationSubStatus> ApplicationSubStatuses { get; set; }
    public DbSet<AuditLog> AuditLogs { get; set; }
    public DbSet<ConsentRequest> ConsentRequests { get; set; }
    public DbSet<Epc> EPCs { get; set; }
    public DbSet<ExternalPortalDashboardApplication> ExternalPortalDashboardApplications { get; set; }
    public DbSet<Feedback> Feedbacks { get; set; }
    public DbSet<GlobalSetting> GlobalSettings { get; set; }
    public DbSet<Grant> Grants { get; set; }
    public DbSet<InstallationAddress> InstallationAddresses { get; set; }
    public DbSet<InstallationDetail> InstallationDetails { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<PropertyOwnerDetail> PropertyOwnerDetails { get; set; }
    public DbSet<PropertyOwnerAddress> PropertyOwnerAddresses { get; set; }
    public DbSet <SurveyOption> SurveyOptions { get; set; }
    public DbSet<TechType> TechTypes { get; set; }
    public DbSet<Voucher> Vouchers { get; set; }
    public DbSet<VoucherStatus> VoucherStatuses { get; set; }
    public DbSet<VoucherStatusHistory> VoucherStatusHistories { get; set; }
    public DbSet<VoucherSubStatus> VoucherSubStatuses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationEntityTypeConfiguration).Assembly);

        modelBuilder.Entity<Application>().HasIndex(b => b.ReferenceNumber).HasDatabaseName("A_Index_ReferenceNumber");

        modelBuilder.Entity<ApplicationStatus>().HasIndex(b => b.Code).HasDatabaseName("AS_Index_Code");
        modelBuilder.Entity<ApplicationSubStatus>().HasIndex(b => b.Code).HasDatabaseName("ASS_Index_Code");

        modelBuilder.Entity<VoucherStatus>().HasIndex(b => b.Code).HasDatabaseName("VS_Index_Code");
        modelBuilder.Entity<VoucherSubStatus>().HasIndex(b => b.Code).HasDatabaseName("VSS_Index_Code");

        modelBuilder.Entity<InstallationAddress>().HasIndex(b => b.UPRN).HasDatabaseName("IA_Index_UPRN");

        modelBuilder.Entity<GlobalSetting>().Property(b => b.ID).HasDefaultValueSql("NEWID()");

        modelBuilder.Entity<ApplicationDashboard>(eb => { eb.HasNoKey(); eb.ToView("vw_Dashboard_Applications_Only"); });
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var addedEntities = ChangeTracker.Entries<ICreateModify>()
            .Where(entity => entity.State == EntityState.Added)
            .ToList();

        addedEntities.ForEach(entity =>
        {
            entity.Property(x => x.CreatedDate).CurrentValue = DateTime.UtcNow;
            entity.Property(x => x.CreatedDate).IsModified = true;

        });

        var editedEntities = ChangeTracker.Entries<ICreateModify>()
            .Where(entity => entity.State == EntityState.Modified)
            .ToList();

        editedEntities.ForEach(entity =>
        {
            entity.Property(x => x.LastUpdatedDate).CurrentValue = DateTime.UtcNow;
            entity.Property(x => x.LastUpdatedDate).IsModified = true;

            entity.Property(x => x.CreatedDate).CurrentValue = entity.Property(x => x.CreatedDate).OriginalValue;
            entity.Property(x => x.CreatedDate).IsModified = false;
        });

        return base.SaveChangesAsync(true, cancellationToken);
    }
}
