using FluentAssertions;
using Moq;
using NUnit.Framework;
using Ofgem.API.BUS.Applications.Providers.DataAccess.Interfaces;
using Ofgem.Lib.BUS.APIClient.Domain.Exceptions;
using System;
using System.Threading.Tasks;

namespace Ofgem.API.BUS.Applications.Core.UnitTests.ApplicationsServiceTests;

/// <summary>
/// Tests for the ApplicationsService class' implementation of IApplicationsService.GetApplicationByIdAsync
/// </summary>
public class GetApplicationByIdAsyncTests : ApplicationsServiceTestsBase
{
    [Test]
    public async Task GetApplicationByIdAsync_Should_Throw_BadRequestException_When_ApplicationId_Is_Empty_Guid()
    {
        // arrange
        var systemUnderTest = GenerateSystemUnderTest();

        // act
        Func<Task> act = () => _ = systemUnderTest.GetApplicationByIdAsync(Guid.Empty);

        // assert
        await act
          .Should().ThrowExactlyAsync<BadRequestException>()
          .WithMessage($"Could not find application for ID {Guid.Empty}");
    }

    [Test]
    public async Task GetApplicationByIdAsync_Should_Return_Provider_Result()
    {
        // arrange
        var expectedReturnedApplication = TestApplication;

        _mockApplicationsProvider = new Moq.Mock<Providers.DataAccess.Interfaces.IApplicationsProvider>(Moq.MockBehavior.Strict);
        Add_GetApplicationOrDefaultByIdAsync_ToMockedApplicationsProvider(ref _mockApplicationsProvider);

        var systemUnderTest = GenerateSystemUnderTest();

        // act
        var result = await systemUnderTest.GetApplicationByIdAsync(applicationId);

        // assert
        result.Should().BeEquivalentTo(expectedReturnedApplication);
    }

    [Test]
    public async Task GetApplicationByIdAsync_Should_Throw_BadRequestException_When_Provider_Returns_Null()
    {
        // arrange
        var testApplicationId = Guid.NewGuid();

        _mockApplicationsProvider = new Mock<IApplicationsProvider>(MockBehavior.Strict);
        Add_NullReturning_GetApplicationWithConsentObjectsByIdAsync_ToMockedApplicationsProvider(ref _mockApplicationsProvider);

        var systemUnderTest = GenerateSystemUnderTest();

        // act
        Func<Task> act = () => _ = systemUnderTest.GetApplicationByIdAsync(testApplicationId);

        // assert
        await act
          .Should().ThrowExactlyAsync<BadRequestException>()
          .WithMessage($"Could not find application for ID {testApplicationId}");
    }
}
