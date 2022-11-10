using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Ofgem.API.BUS.Applications.Domain;
using System;

namespace Ofgem.API.BUS.Applications.Providers.DataAccess;

/// <summary>
/// This Class forms as a provider to unit tests (or any other test suite), to instanciate a mock Applications Db Context.
/// </summary>
public class TestBaseWithSqlite
{
    private const string InMemoryConnectionString = "DataSource=:memory:";
    private readonly SqliteConnection _connection;
    public readonly ApplicationsDBContext DbContext;

    public TestBaseWithSqlite()
    {
        _connection = new SqliteConnection(InMemoryConnectionString);
        _connection.Open();

        var options = new DbContextOptionsBuilder<ApplicationsDBContext>()
            .UseSqlite(_connection)
            .Options;

        DbContext = new ApplicationsDBContext(options);
        DbContext.Database.EnsureCreated();
    }

    /// <summary>
    /// This function seeds the mock database.
    /// </summary>
    public void SeedInMemoryDb()
    {
        var application = new Application
        {
            ID = new Guid("73452DC3-048E-4372-9317-08DA3763D379"),
            ReferenceNumber = "GID10000354",
            SubStatusId = new Guid("9E8FA3F3-8E5A-4C54-893A-70D6EF1B0EC6"),
            CreatedBy = "seth nettles",
            ApplicationDate = DateTime.Now,
            PropertyOwnerDetailId = new Guid("4D0AB1C8-8B6E-445B-0A21-08DA2D0B1978"),
            InstallationAddressID = new Guid("7D26A195-D305-4EB7-A3E5-9B884D826B9D"),
            TechTypeId = new Guid("CAE743AA-E0AB-4CFD-9820-300CB1F12074"),
        };
        DbContext.Add(application);

        var applicationSubStatuses = new ApplicationSubStatus
        {
            Id = new Guid("9E8FA3F3-8E5A-4C54-893A-70D6EF1B0EC6"),
            ApplicationStatusId = new Guid("7D26A195-D305-4EB7-A3E5-9B884D826B9D"),
            Code = ApplicationSubStatus.ApplicationSubStatusCode.SUB,
            Description = "Submitted",
            DisplayName = "Submitted",
            SortOrder = 10
        };
        DbContext.Add(applicationSubStatuses);

        var applicationStatuses = new ApplicationStatus
        {
            Id = new Guid("7D26A195-D305-4EB7-A3E5-9B884D826B9D"),
            Code = ApplicationStatus.ApplicationStatusCode.SUB,
            Description = "Submitted",
            SortOrder = 10
        };
        DbContext.Add(applicationStatuses);

        /*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/
        var installationAddress = new InstallationAddress
        {
            ID = new Guid("7D26A195-D305-4EB7-A3E5-9B884D826B9D"),
            AddressLine1 = "Address Line 1",
            AddressLine2 = "Address Line 2",
            AddressLine3 = "Address Line 3",
            County = "County",
            Postcode = "LU6 1AD",
            UPRN = "UPRN",
            IsGasGrid = true,
            IsRural = "YES"

        };
        DbContext.Add(installationAddress);

        var propertyOwnerDetail = new PropertyOwnerDetail
        {
            ID = new Guid("4D0AB1C8-8B6E-445B-0A21-08DA2D0B1978"),
            PropertyOwnerAddressId = new Guid("90E8F1BD-317A-4FAB-9B81-08DA2EB43BB1"),
            Email = "sethnettles@rocketmail.com",
            FullName = "Seth Nettles Esq.",
            IsAssistedDigital = true,
            IsWelshTranslation = true,
            TelephoneNumber = "07973346529",
            PropertyOwnerAddress = new PropertyOwnerAddress
            {
                ID = new Guid("90E8F1BD-317A-4FAB-9B81-08DA2EB43BB1"),
                AddressLine1 = "Adress Line1",
                AddressLine2 = "Address Line2",
                AddressLine3 = "Address Line3",
                County = "County",
                Postcode = "LU6 1AD",
                UPRN = "URPN"

            }
        };
        DbContext.Add(propertyOwnerDetail);

        var consentRequests = new ConsentRequest
        {
            ID = new Guid("EB02B5E1-758B-4E52-96DB-02957A6C5754"),
            ApplicationID = new Guid("73452DC3-048E-4372-9317-08DA3763D379"),
            ConsentExpiryDate = DateTime.Now,
            ConsentIssuedDate = DateTime.Now,
            ConsentReceivedDate = DateTime.Now
        };
        DbContext.Add(consentRequests);

        var techType = new TechType
        {
            ID = new Guid("CAE743AA-E0AB-4CFD-9820-300CB1F12074"),
            ExpiryIntervalMonths = 1,
            TechTypeDescription = "Air source heat pump"
        };
        DbContext.Add(techType);

        var voucher = new Voucher
        {
            ID = new Guid("BDDD9597-3D4F-4691-B86C-08DA37DCD114"),
            ApplicationID = new Guid("73452DC3-048E-4372-9317-08DA3763D379"),
            DARecommendation = true,
            ExpiryDate = DateTime.Now,
            QCRecommendation = true,
            RedemptionRequestDate = DateTime.Now,
            VoucherSubStatusID = new Guid("20014E3A-20F7-4DFA-A77E-0DD166E3C81B"),
            GrantId = new Guid("E5BBB215-2C41-48B0-B72D-E188BF9B9538"),
        };
        DbContext.Add(voucher);

        var voucherSubStatus = new VoucherSubStatus
        {
            Id = new Guid("20014E3A-20F7-4DFA-A77E-0DD166E3C81B"),
            Code = VoucherSubStatus.VoucherSubStatusCode.SUB,
            DisplayName = "SUB",
            Description = "SUB",
            SortOrder = 10,
            VoucherStatusId = new Guid("20014E3A-20F7-4DFA-A77E-0DD166E3C81B"),
        };
        DbContext.Add(voucherSubStatus);

        var voucherStatus = new VoucherStatus
        {
            Id = new Guid("20014E3A-20F7-4DFA-A77E-0DD166E3C81B"),
            Code = VoucherStatus.VoucherStatusCode.PAID,
            Description = "PAID",
            SortOrder = 10
        };
        DbContext.Add(voucherStatus);

        var grant = new Grant
        {
            ID = new Guid("90376C34-896C-4E97-A81E-0EFD3CD05877"),
            TechTypeID = new Guid("CAE743AA-E0AB-4CFD-9820-300CB1F12074"),
            Amount = 9,
            EndDate = DateTime.Now,
            StartDate = DateTime.Now,
        };
        DbContext.Add(grant);

        DbContext.SaveChanges();
    }

    /// <summary>
    /// This function deletes the mock database after its use is not needed.
    /// Its should be implemented at the end of every test run.
    /// </summary>
    public void Dispose()
    {
        DbContext.Dispose();
    }
}
