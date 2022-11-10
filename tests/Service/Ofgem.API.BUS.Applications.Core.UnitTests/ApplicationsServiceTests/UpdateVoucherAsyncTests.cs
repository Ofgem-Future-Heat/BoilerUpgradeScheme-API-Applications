using FluentAssertions;
using Moq;
using NUnit.Framework;
using Ofgem.API.BUS.Applications.Domain;
using System.Threading.Tasks;

namespace Ofgem.API.BUS.Applications.Core.UnitTests.ApplicationsServiceTests;

/// <summary>
/// Tests for the ApplicationsService class' implementation of IApplicationsService.UpdateVoucherAsync
/// </summary>
public class UpdateVoucherAsyncTests : ApplicationsServiceTestsBase
{
    [Test]
    public async Task When_UpdateVoucher_Successful()
    {
        // Arrange
        var mockOfVoucher = new Mock<Voucher>();

        _mockApplicationsProvider = new();

        _mockApplicationsProvider.Setup(m => m.UpdateVoucherAsync(It.IsAny<Voucher>()))
            .ReturnsAsync(true);

        var systemUnderTest = GenerateSystemUnderTest();

        // Act.
        var returnedState = await systemUnderTest.UpdateVoucherAsync(mockOfVoucher.Object).ConfigureAwait(false);

        // Assert
        returnedState.Should().BeTrue();
    }
}
