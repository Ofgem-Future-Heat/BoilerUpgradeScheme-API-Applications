using FluentAssertions;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ofgem.API.BUS.Applications.Core.UnitTests.ApplicationsServiceTests;

/// <summary>
/// Tests for the ApplicationsService class' implementation of IApplicationsService.UpdateEpcReferenceAsync
/// </summary>
public class UpdateEpcReferenceAsyncTests : ApplicationsServiceTestsBase
{
    [Test]
    public async Task When_UpdateEpc_Successful()
    {
        // Arrange
        _mockApplicationsProvider = new();

        _mockApplicationsProvider.Setup(m => m.UpdateEpcReferenceAsync(It.IsAny<Guid>()))
            .ReturnsAsync(new List<string>());

        var systemUnderTest = GenerateSystemUnderTest();

        // Act.
        var returnedState = await systemUnderTest.UpdateEpcReferenceAsync(It.IsAny<Guid>()).ConfigureAwait(false);

        // Assert
        returnedState.Should().BeOfType<List<string>>();
    }
}
