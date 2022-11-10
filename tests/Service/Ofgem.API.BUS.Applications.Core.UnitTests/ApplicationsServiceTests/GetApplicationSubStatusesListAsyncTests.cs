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
/// Tests for the ApplicationsService class' implementation of IApplicationsService.GetApplicationSubStatusesListAsync
/// </summary>
public class GetApplicationSubStatusesListAsyncTests : ApplicationsServiceTestsBase
{
    [Test]
    public async Task GetApplicationSubStatusesListAsync_Should_Return_What_Provider_Returns()
    {
        // arrange
        var expected = TestApplicationSubStatusList;

        _mockApplicationsProvider = new Mock<IApplicationsProvider>(MockBehavior.Strict);
        Add_GetApplicationSubStatusesListAsync_ToMockedApplicationsProvider(ref _mockApplicationsProvider, expected);

        var systemUnderTest = GenerateSystemUnderTest();

        // act
        var result = await systemUnderTest.GetApplicationSubStatusesListAsync();

        // assert
        result.Should().BeEquivalentTo(expected);
    }

    [Test]
    public async Task GetApplicationSubStatusesListAsync_Should_Throw_BadRequestException_When_Provider_Returns_No_Objects()
    {
        // arrange
        var providerOutput = new List<ApplicationSubStatus>();

        _mockApplicationsProvider = new Mock<IApplicationsProvider>(MockBehavior.Strict);
        Add_GetApplicationSubStatusesListAsync_ToMockedApplicationsProvider(ref _mockApplicationsProvider, providerOutput);

        var systemUnderTest = GenerateSystemUnderTest();

        // act
        Func<Task> act = () => _ = systemUnderTest.GetApplicationSubStatusesListAsync();

        // assert
        await act
          .Should().ThrowExactlyAsync<BadRequestException>()
          .WithMessage("Could not find list of application statuses");
    }
}
