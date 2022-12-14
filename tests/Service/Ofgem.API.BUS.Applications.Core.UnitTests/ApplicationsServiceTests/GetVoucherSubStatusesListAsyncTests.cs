using FluentAssertions;
using Moq;
using NUnit.Framework;
using Ofgem.API.BUS.Applications.Domain;
using Ofgem.API.BUS.Applications.Providers.DataAccess.Interfaces;
using Ofgem.Lib.BUS.APIClient.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ofgem.API.BUS.Applications.Core.UnitTests.ApplicationsServiceTests;

/// <summary>
/// Tests for the ApplicationsService class' implementation of IApplicationsService.GetVoucherSubStatusesListAsync
/// </summary>
public class GetVoucherSubStatusesListAsyncTests : ApplicationsServiceTestsBase
{
    [Test]
    public async Task GetVoucherSubStatusesListAsync_Should_Return_What_Provider_Returns()
    {
        // arrange
        var expected = TestVoucherSubStatusList;

        _mockApplicationsProvider = new Mock<IApplicationsProvider>(MockBehavior.Strict);
        Add_GetVoucherSubStatusesListAsync_ToMockedApplicationsProvider(ref _mockApplicationsProvider, expected);

        var systemUnderTest = GenerateSystemUnderTest();

        // act
        var result = await systemUnderTest.GetVoucherSubStatusesListAsync();

        // assert
        result.Should().BeEquivalentTo(expected);
    }

    [Test]
    public async Task GetVoucherSubStatusesListAsync_Should_Throw_BadRequestException_When_Provider_Returns_No_Objects()
    {
        // arrange
        var providerOutput = new List<VoucherSubStatus>();

        _mockApplicationsProvider = new Mock<IApplicationsProvider>(MockBehavior.Strict);
        Add_GetVoucherSubStatusesListAsync_ToMockedApplicationsProvider(ref _mockApplicationsProvider, providerOutput);

        var systemUnderTest = GenerateSystemUnderTest();

        // act
        Func<Task> act = () => _ = systemUnderTest.GetVoucherSubStatusesListAsync();

        // assert
        await act
          .Should().ThrowExactlyAsync<BadRequestException>()
          .WithMessage("Could not find list of voucher statuses");
    }

}
