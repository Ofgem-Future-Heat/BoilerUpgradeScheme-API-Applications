using Moq;
using NUnit.Framework;
using Ofgem.API.BUS.Applications.Domain.Entities.Enums;
using System;
using System.Collections.Generic;

namespace Ofgem.API.BUS.Applications.Core.UnitTests.ApplicationsServiceTests;

/// <summary>
/// Tests for the ApplicationsService class' implementation of IApplicationsService.GetApplicationsByBusinessAccountIdAsync
/// </summary>
[TestFixture]
public class GetExternalDashboardApplicationsByBusinessAccountIdAsyncTests : ApplicationsServiceTestsBase
{
    [TestCase("9bd6f550-3680-4f42-9b00-08da2cf1aeaf", null, null, null)]
    [TestCase("9bd6f550-3680-4f42-9b00-08da2cf1aAAf", "postcode", null, null)]
    [TestCase("9bd6f550-3680-4f42-9b00-08da2cf1aAAf", "", null, null)]
    [TestCase("9bd6f550-3680-4f42-9b00-08da2cf1aAAf", "postcode", null, ConsentState.Received)]
    public void GetExternalDashboardApplicationsByBusinessAccountIdAsync_Is_Simple_Passthrough_To_ProviderMethod(Guid businessAccountId, string? seachBy, IEnumerable<Guid>? statusIds, ConsentState? consentState)
    {
        // arrange
        var systemUnderTest = GenerateSystemUnderTest();

        // act
        _ = systemUnderTest.GetExternalDashboardApplicationsByBusinessAccountId(businessAccountId, seachBy, statusIds, consentState.ToString());

        // assert
        _mockApplicationsProvider.Verify(m => m.GetExternalDashboardApplicationsByBusinessAccountId(businessAccountId, seachBy, statusIds, consentState), Times.Once);
    }
}
