using FluentAssertions;
using NUnit.Framework;
using Ofgem.API.BUS.Applications.Domain;
using Ofgem.Lib.BUS.APIClient.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ofgem.API.BUS.Applications.Core.UnitTests.ApplicationsServiceTests;

/// <summary>
/// Tests for the ApplicationsService class' implementation of IApplicationsService.CheckForActiveConsentByApplicationIdAsync
/// </summary>
public class CheckForActiveConsentByApplicationIdAsyncTests : ApplicationsServiceTestsBase
{
    [Test]
    [Ignore("Does not reflect current behaviour")]
    public async Task CheckForActiveConsentByApplicationIdAsync_Should_Throw_ArgumentException_If_ApplicationId_Is_Empty_Guid()
    {
        // arrange
        var systemUnderTest = GenerateSystemUnderTest();

        // act
        Func<Task> act = () => _ = systemUnderTest.CheckForActiveConsentByApplicationIdAsync(Guid.Empty);

        // assert
        await act
          .Should().ThrowExactlyAsync<ArgumentException>()
          .WithParameterName("applicationId")
          .WithMessage("Value cannot be empty GUID. (Parameter 'applicationId')");
    }

    [Test]
    public async Task CheckForActiveConsentByApplicationIdAsync_Should_Throw_ResourceNotFoundException_If_ApplicationId_Is_Empty_Guid()
    {
        // this tests current, potentially udesired behvaiour

        // arrange
        var systemUnderTest = GenerateSystemUnderTest();

        // act
        Func<Task> act = () => _ = systemUnderTest.CheckForActiveConsentByApplicationIdAsync(Guid.Empty);

        // assert
        await act
          .Should().ThrowExactlyAsync<ResourceNotFoundException>()
          .WithMessage($"Application ID {Guid.Empty} not found");
    }

    [Test]
    public async Task CheckForActiveConsentByApplicationIdAsync_Should_Return_False_If_No_Consented_Competing_Application_Exists()
    {
        // arrange
        _mockApplicationsProvider = new Moq.Mock<Providers.DataAccess.Interfaces.IApplicationsProvider>(Moq.MockBehavior.Strict);
        Add_GetApplicationOrDefaultWithConsentObjectsByIdAsync_ToMockedApplicationsProvider(ref _mockApplicationsProvider);
        Add_GetApplicationsByInstallationAddressUprnAsync_Returns(ref _mockApplicationsProvider, new List<Application> {  TestApplication });

        var systemUnderTest = GenerateSystemUnderTest();

        // act
        var result = await systemUnderTest.CheckForActiveConsentByApplicationIdAsync(applicationId);

        // assert
        result.Should().BeFalse();
    }

    [Test]
    public async Task CheckForActiveConsentByApplicationIdAsync_Should_Return_True_If_Consented_Competing_Application_Exists()
    {
        // arrange
        _mockApplicationsProvider = new Moq.Mock<Providers.DataAccess.Interfaces.IApplicationsProvider>(Moq.MockBehavior.Strict);
        Add_GetApplicationOrDefaultWithConsentObjectsByIdAsync_ToMockedApplicationsProvider(ref _mockApplicationsProvider);

        var application1 = TestApplication;
        var application2 = TestApplication;
        application2.ConsentRequests.First().ConsentReceivedDate = DateTime.Now;

        var listWithConsentedApplication = new List<Application>
        {
            application1, application2
        };

        Add_GetApplicationsByInstallationAddressUprnAsync_Returns(ref _mockApplicationsProvider, listWithConsentedApplication);

        var systemUnderTest = GenerateSystemUnderTest();

        // act
        var result = await systemUnderTest.CheckForActiveConsentByApplicationIdAsync(applicationId);

        // assert
        result.Should().BeTrue();
    }
}
