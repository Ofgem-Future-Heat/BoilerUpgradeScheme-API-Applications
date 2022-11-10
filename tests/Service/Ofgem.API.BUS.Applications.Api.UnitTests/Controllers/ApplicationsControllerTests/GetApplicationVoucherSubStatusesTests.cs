using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using Ofgem.API.BUS.Applications.Domain.Entities;
using Ofgem.Lib.BUS.APIClient.Domain.Exceptions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ofgem.API.BUS.Applications.Api.UnitTests.Controllers.ApplicationsControllerTests;

/// <summary>
/// Tests for the ApplicationsController.GetApplicationVoucherSubStatuses method
/// </summary>
public class GetApplicationVoucherSubStatusesTests : ApplicationsControllerTestsBase
{
    [Test]
    public async Task GetApplicationVoucherSubStatuses_ShouldReturnOkObjectResultWithServiceObject_WhenServiceReturnsAResult()
    {
        // arrange
        var serviceReturnObject = new List<ApplicationVoucherSubStatus>
        {
            new ApplicationVoucherSubStatus
            {
                DisplayName = "Test Display Name"
            }
        };

        _mockApplicationsService = new Moq.Mock<Core.Interfaces.IApplicationsService>(Moq.MockBehavior.Strict);
        _mockApplicationsService
            .Setup(x => x.GetApplicationVoucherSubStatusesListAsync())
            .ReturnsAsync(serviceReturnObject);

        var systemUnderTest = GenerateSystemUnderTest();

        // act
        var result = await systemUnderTest.GetApplicationVoucherSubStatuses();

        // assert
        Assert.IsInstanceOf<OkObjectResult>(result);
        var castResult = result as OkObjectResult;
        castResult.Should().NotBeNull();
        if (castResult is not null)
        {
            var retrievedApplicationVoucherSubStatusList = castResult.Value as List<ApplicationVoucherSubStatus>;
            retrievedApplicationVoucherSubStatusList.Should().NotBeNull();
            if (retrievedApplicationVoucherSubStatusList is not null)
            {
                retrievedApplicationVoucherSubStatusList.Count.Should().Be(1);
            }
        }
    }

    [Test]
    public async Task GetApplicationVoucherSubStatuses_ShouldReturnBadObjectResult_WhenServiceThrowsBadRequestException()
    {
        // arrange
        _mockApplicationsService = new Mock<Core.Interfaces.IApplicationsService>(MockBehavior.Strict);
        _mockApplicationsService
            .Setup(m => m.GetApplicationVoucherSubStatusesListAsync())
            .Throws(new BadRequestException("Test exception text"));

        var systemUnderTest = GenerateSystemUnderTest();

        // act
        var result = await systemUnderTest.GetApplicationVoucherSubStatuses();

        // assert
        Assert.IsInstanceOf<BadRequestObjectResult>(result);
    }
}
