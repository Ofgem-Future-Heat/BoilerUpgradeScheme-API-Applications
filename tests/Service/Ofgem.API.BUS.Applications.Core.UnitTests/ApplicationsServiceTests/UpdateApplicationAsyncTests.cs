using FluentAssertions;
using Moq;
using NUnit.Framework;
using Ofgem.API.BUS.Applications.Domain;
using System.Threading.Tasks;

namespace Ofgem.API.BUS.Applications.Core.UnitTests.ApplicationsServiceTests;

/// <summary>
/// Tests for the ApplicationsService class' implementation of IApplicationsService.UpdateApplicationAsync
/// </summary>
public class UpdateApplicationAsyncTests : ApplicationsServiceTestsBase
{
    [Test]
    public async Task When_UpdateApplication_Successful()
    {
        // Arrange
        var mockOfApplication = new Mock<Application>();

        _mockApplicationsProvider = new();

        _mockApplicationsProvider.Setup(m => m.UpdateApplicationAsync(It.IsAny<Application>()))
            .ReturnsAsync(true);

        var systemUnderTest = GenerateSystemUnderTest();

        // Act.
        var returnedState = await systemUnderTest.UpdateApplicationAsync(mockOfApplication.Object).ConfigureAwait(false);

        // Assert
        returnedState.Should().BeTrue();
    }
}
