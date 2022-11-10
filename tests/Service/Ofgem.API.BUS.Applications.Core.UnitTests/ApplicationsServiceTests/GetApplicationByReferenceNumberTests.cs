using FluentAssertions;
using Moq;
using NUnit.Framework;
using Ofgem.API.BUS.Applications.Domain;
using Ofgem.Lib.BUS.APIClient.Domain.Exceptions;
using System;
using System.Threading.Tasks;

namespace Ofgem.API.BUS.Applications.Core.UnitTests.ApplicationsServiceTests;

/// <summary>
/// Tests for the ApplicationsService class' implementation of IApplicationsService.GetApplicationByReferenceNumber
/// </summary>
public class GetApplicationByReferenceNumberTests : ApplicationsServiceTestsBase
{
    /// <summary>
    /// Application ID AKA Reference Number
    /// </summary>
    [Test]
    public void GetApplications_Returns_By_ReferenceNumber()
    {
        // Arrange
        var referenceNumber = "CS1000009";

        _mockApplicationsProvider
                    .Setup(m => m.GetApplicationOrDefaultByReferenceNumberAsync(referenceNumber))
                    .ReturnsAsync(TestApplication);

        var systemUnderTest = GenerateSystemUnderTest();

        // Act
        var foundApplication = systemUnderTest.GetApplicationByReferenceNumber(referenceNumber);

        // Assert
        foundApplication.Should().NotBeNull();
    }

    [Test]
    public async Task GetApplicationByReferenceNumber_Should_Throw_ResourceNotFoundException_When_ReferenceNumber_Not_Found_By_Provider()
    {
        // Arrange
        _mockApplicationsProvider = new Mock<Providers.DataAccess.Interfaces.IApplicationsProvider>(MockBehavior.Strict);
        Add_NullReturning_GetApplicationOrDefaultByReferenceNumberAsync_ToMockedApplicationsProvider(ref _mockApplicationsProvider);

        var systemUnderTest = GenerateSystemUnderTest();

        // act
        Func<Task> act = () => _ = systemUnderTest.GetApplicationByReferenceNumber(applicationReferenceNumber);

        // assert
        await act
          .Should().ThrowExactlyAsync<ResourceNotFoundException>()
          .WithMessage($"Could not find application for reference number {applicationReferenceNumber}");
    }
}
