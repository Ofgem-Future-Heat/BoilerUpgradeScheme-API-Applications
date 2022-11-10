using FluentAssertions;
using Moq;
using NUnit.Framework;
using Ofgem.API.BUS.Applications.Domain;
using Ofgem.API.BUS.Applications.Providers.DataAccess.Interfaces;
using Ofgem.API.BUS.BusinessAccounts.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ofgem.API.BUS.Applications.Core.UnitTests.ApplicationsServiceTests;

/// <summary>
/// Tests for the ApplicationsService class' implementation of IApplicationsService.GetGrantsListAsync
/// </summary>
public class GetGrantsListAsyncTests : ApplicationsServiceTestsBase
{

    [Test]
    public async Task GetGrantsListAsync_Should_Return_What_Provider_Returns()
    {
        // arrange
        var expected = TestGrantList;

        _mockApplicationsProvider = new Mock<IApplicationsProvider>(MockBehavior.Strict);
        Add_GetGrantsListAsync_ToMockedApplicationsProvider(ref _mockApplicationsProvider, expected);

        var systemUnderTest = GenerateSystemUnderTest();

        // act
        var result = await systemUnderTest.GetGrantsListAsync();

        // assert
        result.Should().BeEquivalentTo(expected);
    }

    [Test]
    [Ignore("This expected behaviour is not currently implemented")]
    public async Task GetGrantsListAsync_Should_Throw_BadRequestException_When_Provider_Returns_No_Objects()
    {
        // arrange
        var providerOutput = new List<Grant>();

        _mockApplicationsProvider = new Mock<IApplicationsProvider>(MockBehavior.Strict);
        Add_GetGrantsListAsync_ToMockedApplicationsProvider(ref _mockApplicationsProvider, providerOutput);

        var systemUnderTest = GenerateSystemUnderTest();

        // act
        Func<Task> act = () => _ = systemUnderTest.GetGrantsListAsync();

        // assert
        await act
          .Should().ThrowExactlyAsync<BadRequestException>()
          .WithMessage("Could not find list of grants");
    }

    [Test]
    public async Task GetGrantsListAsync_Should_Not_Return_Null()
    {
        // arrange
        var systemUnderTest = GenerateSystemUnderTest();

        // act
        var result = await systemUnderTest.GetGrantsListAsync().ConfigureAwait(false);

        // assert
        result.Should().NotBeNull();
    }

    [Test]
    public async Task GetGrantsListAsync_Should_Return_Provider_Response()
    {
        // arrange
        _mockApplicationsProvider = new Mock<IApplicationsProvider>(MockBehavior.Strict);
        Add_GetGrantListAsync_ToMockedApplicationsProvider(ref _mockApplicationsProvider);

        var systemUnderTest = GenerateSystemUnderTest();

        // act
        var result = await systemUnderTest.GetGrantsListAsync().ConfigureAwait(false);

        // assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(grantsList);
    }
}
