using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using Ofgem.API.BUS.Applications.Domain;
using Ofgem.Lib.BUS.APIClient.Domain.Exceptions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ofgem.API.BUS.Applications.Api.UnitTests.Controllers.ApplicationsControllerTests;

/// <summary>
/// Tests for the ApplicationsController.GetVoucherStatuses method
/// </summary>
public class GetVoucherStatusesTests : ApplicationsControllerTestsBase
{
    [Test]
    public async Task GetVoucherStatuses_ShouldReturnOkObjectResultWithServiceObject_WhenServiceReturnsAResult()
    {
        // arrange
        var serviceReturnObject = new List<VoucherSubStatus>
        {
            new VoucherSubStatus()
        };

        _mockApplicationsService = new Moq.Mock<Core.Interfaces.IApplicationsService>(Moq.MockBehavior.Strict);
        _mockApplicationsService
            .Setup(x => x.GetVoucherSubStatusesListAsync())
            .ReturnsAsync(serviceReturnObject);

        var systemUnderTest = GenerateSystemUnderTest();

        // act
        var result = await systemUnderTest.GetVoucherStatuses();

        // assert
        Assert.IsInstanceOf<OkObjectResult>(result);
        var castResult = result as OkObjectResult;
        castResult.Should().NotBeNull();
        if (castResult is not null)
        {
            var voucherSubStatusList = castResult.Value as List<VoucherSubStatus>;
            voucherSubStatusList.Should().NotBeNull();
            if (voucherSubStatusList is not null)
            {
                voucherSubStatusList.Count.Should().Be(1);
            }
        }
    }

    [Test]
    public async Task GetVoucherStatuses_ShouldReturnBadObjectResult_WhenServiceThrowsBadRequestException()
    {
        // arrange
        _mockApplicationsService = new Mock<Core.Interfaces.IApplicationsService>(MockBehavior.Strict);
        _mockApplicationsService
            .Setup(m => m.GetVoucherSubStatusesListAsync())
            .Throws(new BadRequestException("Test exception text"));

        var systemUnderTest = GenerateSystemUnderTest();

        // act
        var result = await systemUnderTest.GetVoucherStatuses();

        // assert
        Assert.IsInstanceOf<BadRequestObjectResult>(result);
    }
}
