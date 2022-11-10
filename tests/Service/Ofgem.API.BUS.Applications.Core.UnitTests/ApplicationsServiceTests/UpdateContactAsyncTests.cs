using System;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;
using Ofgem.API.BUS.Applications.Domain.Entities.CommsObjects;

namespace Ofgem.API.BUS.Applications.Core.UnitTests.ApplicationsServiceTests;

/// <summary>
/// Tests for the ApplicationsService class' implementation of IApplicationsService.UpdateContactAsync
/// </summary>
public class UpdateContactAsyncTests : ApplicationsServiceTestsBase
{
    [Test]
    public async Task When_UpdateContact_Successful()
    {
        // Arrange
        var mockOfRequest = new Mock<UpdateContactRequest>();

        _mockApplicationsProvider = new();

        _mockApplicationsProvider.Setup(m => m.UpdateContactAsync(It.IsAny<Guid>(), It.IsAny<Guid>())).ReturnsAsync(true);

        var systemUnderTest = GenerateSystemUnderTest();

        // Act.
        var returnedState = await systemUnderTest.UpdateContactAsync(mockOfRequest.Object).ConfigureAwait(false);

        // Assert
        returnedState.Should().BeTrue();
    }
}
