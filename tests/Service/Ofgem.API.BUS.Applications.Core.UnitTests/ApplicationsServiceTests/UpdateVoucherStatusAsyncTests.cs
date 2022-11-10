using FluentAssertions;
using Moq;
using NUnit.Framework;
using Ofgem.API.BUS.Applications.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ofgem.API.BUS.Applications.Core.UnitTests.ApplicationsServiceTests;

/// <summary>
/// Tests for the ApplicationsService class' implementation of IApplicationsService.UpdateVoucherStatusAsync
/// </summary>
public class UpdateVoucherStatusAsyncTests : ApplicationsServiceTestsBase
{

    [Test]
    public async Task When_UpdateVoucherStatusAsync_Successful()
    {
        // Arrange
        var mockOfVoucher = new Mock<Voucher>();

        _mockApplicationsProvider = new();

        _mockApplicationsProvider.Setup(m => m.UpdateVoucherStatusAsync(It.IsAny<Guid>(), It.IsAny<Guid>()))
            .ReturnsAsync(It.IsAny<List<string>>());

        var systemUnderTest = GenerateSystemUnderTest();

        // Act.
        var returnedState = await systemUnderTest.UpdateVoucherStatusAsync(It.IsAny<Guid>(), It.IsAny<Guid>()).ConfigureAwait(false);

        // Assert
        returnedState.Should().BeNullOrEmpty();
    }

    [Test]
    public async Task When_UpdateVoucherStatusAsync_UnSuccessful()
    {
        // Arrange
        var mockOfVoucher = new Mock<Voucher>();

        _mockApplicationsProvider = new();

        _mockApplicationsProvider.Setup(m => m.UpdateVoucherStatusAsync(It.IsAny<Guid>(), It.IsAny<Guid>()))
            .ReturnsAsync(new List<string>() { $"Failed to update Status {It.IsAny<Guid>()} {It.IsAny<string>()}" });

        var systemUnderTest = GenerateSystemUnderTest();

        // Act.
        List<string>? returnedState = await systemUnderTest.UpdateVoucherStatusAsync(It.IsAny<Guid>(), It.IsAny<Guid>()).ConfigureAwait(false);

        // Assert
        returnedState.Should().HaveCount(1);
    }
}
