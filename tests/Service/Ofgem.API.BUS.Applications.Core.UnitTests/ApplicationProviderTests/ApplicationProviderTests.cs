using FluentAssertions;
using NUnit.Framework;
using Ofgem.API.BUS.Applications.Domain;
using Ofgem.API.BUS.Applications.Providers.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ofgem.API.BUS.Applications.Core.UnitTests.ApplicationProviderTests;

public class ApplicationProviderTests : TestBaseWithSqlite
{
    private ApplicationsProvider _applicationProvider = null!;

    [SetUp]
    public void SetUp()
    {
        _applicationProvider = new ApplicationsProvider(DbContext);
    }

    [Test]
    [TestCase("Postcode", true, null, null, null)]
    [TestCase("Postcode", false, null, null, null)]
    [TestCase("ReferenceNumber", true, null, null, null)]
    [TestCase("ReferenceNumber", false, null, null, null)]
    [TestCase("ApplicationSubStatus", true, null, null, null)]
    [TestCase("ReviewRecommendation", true, null, null, null)]
    [TestCase("ConsentState", true, null, null, null)]
    [TestCase("RedemptionRequestDate", true, null, null, null)]
    [TestCase("ApplicationDate", true, null, "PAID", null)]
    [TestCase("ApplicationDate", true, "SUB", null, null)]
    [TestCase("ApplicationDate", true, "SUB", "SUB", "LU6 1AD")]
    [TestCase("ApplicationDate", true, null, null, "LU6 1AD")]
    [TestCase("ApplicationDate", true, "SUB", "SUB", "LU")]
    [TestCase("ApplicationDate", true, null, null, "LU")]
    [TestCase("ApplicationDate", true, "WITHDRAWN", null, "LU")]
    [TestCase("ApplicationDate", true, "WITHDRAWN", "REVOKED", "LU")]
    [TestCase("ApplicationDate", true, null, "REVOKED", null)]
    [Ignore("You cannot mock or access views unless you use a real database!")]
    public async Task Get_PagedResults_With_Various_Parameter_Combos(string columnSortBy,
        bool orderBy,
        string? filterApplicationStatusBy,
        string? filterVoucherStatusBy,
        string searchBy)
    {
        // Arrange
        /* Parse the application statuses passed into the operation*/
        List<string>? listOfFilterApplicationStatusBy = null;
        if (!string.IsNullOrEmpty(filterApplicationStatusBy) && filterApplicationStatusBy.Length > 2)
        {
            listOfFilterApplicationStatusBy = filterApplicationStatusBy.Replace(@"\", "").Split(",").ToList();
        }

        /* Parse the voucher statuses passed into the operation*/
        List<string>? listOfFilterVoucherStatusBy = null;
        if (!string.IsNullOrEmpty(filterVoucherStatusBy) && filterVoucherStatusBy.Length > 2)
        {
            listOfFilterVoucherStatusBy = filterVoucherStatusBy.Replace(@"\", "").Split(",").ToList();
        }

        // Act.
        var pagedResults = await _applicationProvider.GetPagedApplications(1, 20, columnSortBy, orderBy, listOfFilterApplicationStatusBy, listOfFilterVoucherStatusBy, searchBy).ConfigureAwait(false);

        // Assert
        pagedResults.Should().NotBeNull();
    }

    [Test]
    [Ignore("You cannot mock or access views unless you use a real database!")]
    public async Task Get_PagedResults_With_SortBy_Missing()
    {
        // Arrange

        // Act.
        Func<Task> act = () => _applicationProvider.GetPagedApplications(1, 20, "", false, null, null, null);

        // Assert
        await act.Should().ThrowExactlyAsync<ArgumentException>()
            .WithMessage("'sortBy' cannot be null or empty. (Parameter 'sortBy')");
    }
}
