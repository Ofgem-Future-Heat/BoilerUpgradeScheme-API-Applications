using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using Ofgem.API.BUS.Applications.Domain;
using Ofgem.Lib.BUS.APIClient.Domain.Exceptions;
using System.Threading.Tasks;

namespace Ofgem.API.BUS.Applications.Api.UnitTests.Controllers.ApplicationsControllerTests;

/// <summary>
/// Tests for the ApplicationsController.UpdateVoucher method
/// </summary>
public class UpdateVoucherTests : ApplicationsControllerTestsBase
{
    [Test]
    [TestCase(true)]
    [TestCase(false)]
    public async Task UpdateVoucher_ShouldReturnOkResultWithServiceObject_WhenServiceReturnsAResult(bool updateVoucherResult)
    {
        // arrange
        var updateVoucher = new Voucher();

        _mockApplicationsService = new Moq.Mock<Core.Interfaces.IApplicationsService>(Moq.MockBehavior.Strict);
        _mockApplicationsService
            .Setup(x => x.UpdateVoucherAsync(updateVoucher))
            .ReturnsAsync(updateVoucherResult);

        var systemUnderTest = GenerateSystemUnderTest();

        // act
        var result = await systemUnderTest.UpdateVoucher(updateVoucher);

        // assert
        Assert.IsInstanceOf<OkResult>(result);
    }

    [Test]
    public async Task UpdateVoucher_ShouldReturnBadObjectResult_WhenServiceThrowsBadRequestException()
    {
        // arrange
        _mockApplicationsService = new Mock<Core.Interfaces.IApplicationsService>(MockBehavior.Strict);
        _mockApplicationsService
            .Setup(m => m.UpdateVoucherAsync(It.IsAny<Voucher>()))
            .Throws(new BadRequestException("Test exception text"));

        var systemUnderTest = GenerateSystemUnderTest();

        // act
        var result = await systemUnderTest.UpdateVoucher(new Voucher());

        // assert
        Assert.IsInstanceOf<BadRequestObjectResult>(result);
    }
}
