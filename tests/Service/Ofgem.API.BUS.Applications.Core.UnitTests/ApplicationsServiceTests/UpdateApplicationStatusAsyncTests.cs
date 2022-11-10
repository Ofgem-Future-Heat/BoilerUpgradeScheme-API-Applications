using FluentAssertions;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ofgem.API.BUS.Applications.Core.UnitTests.ApplicationsServiceTests;

/// <summary>
/// Tests for the ApplicationsService class' implementation of IApplicationsService.UpdateApplicationStatusAsync
/// </summary>
public class UpdateApplicationStatusAsyncTests : ApplicationsServiceTestsBase
{
    [Test]
    public async Task When_UpdateApplication_Status_By_ApplicationID_Successful()
    {
        // Arrange
        Guid applicationId = Guid.NewGuid();
        Guid applicationStatusId = Guid.NewGuid();

        _mockApplicationsProvider = new();

        _mockApplicationsProvider.Setup(m => m.UpdateApplicationStatusAsync(applicationId, applicationStatusId))
            .ReturnsAsync(new List<string>());

        var systemUnderTest = GenerateSystemUnderTest();

        // Act.
        var listOfErrors = await systemUnderTest.UpdateApplicationStatusAsync(applicationId, applicationStatusId).ConfigureAwait(false);

        // Assert
        listOfErrors.Should().BeEmpty();
    }

    [Test]
    public async Task When_UpdateApplication_Status_By_ApplicationID_Errors()
    {
        // Arrange
        Guid applicationId = Guid.NewGuid();
        Guid applicationStatusId = Guid.NewGuid();

        _mockApplicationsProvider = new();

        _mockApplicationsProvider.Setup(m => m.UpdateApplicationStatusAsync(applicationId, applicationStatusId))
            .ReturnsAsync(new List<string> { "Failed to update Application" });

        var systemUnderTest = GenerateSystemUnderTest();

        // Act.
        var listOfErrors = await systemUnderTest.UpdateApplicationStatusAsync(applicationId, applicationStatusId).ConfigureAwait(false);

        // Assert
        listOfErrors.Should().NotBeEmpty();
    }
}
