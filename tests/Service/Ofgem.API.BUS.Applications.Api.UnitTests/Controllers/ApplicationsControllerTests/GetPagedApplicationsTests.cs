using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using Ofgem.API.BUS.Applications.Domain;
using Ofgem.API.BUS.Applications.Domain.Request;
using Ofgem.Lib.BUS.APIClient.Domain.Exceptions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ofgem.API.BUS.Applications.Api.UnitTests.Controllers.ApplicationsControllerTests;

/// <summary>
/// Tests for the ApplicationsController.GetPagedApplications method
/// </summary>
public class GetPagedApplicationsTests : ApplicationsControllerTestsBase
{
    [Test]
    public async Task GetPagedApplications_ShouldReturnOkObjectResultWithServiceObject_WhenServiceReturnsAResult()
    {
        // arrange
        var serviceReturnObject = new PagedResult<ApplicationDashboard>();

        _mockApplicationsService = new Moq.Mock<Core.Interfaces.IApplicationsService>(Moq.MockBehavior.Strict);
        _mockApplicationsService
            .Setup(x => x.GetPagedApplications(
                It.IsAny<int>(),
                It.IsAny<int>(),
                It.IsAny<string>(),
                It.IsAny<bool>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string?>(),
                It.IsAny<string?>()
                )).ReturnsAsync(serviceReturnObject);

        var systemUnderTest = GenerateSystemUnderTest();

        // act
        var result = await systemUnderTest.GetPagedApplications();

        // assert
        Assert.IsInstanceOf<OkObjectResult>(result);
        var castResult = result as OkObjectResult;
        castResult.Should().NotBeNull();
        if (castResult is not null)
        {
            var pagedResult = castResult.Value as PagedResult<ApplicationDashboard>;
            pagedResult.Should().NotBeNull();
            if (pagedResult is not null)
            {
                pagedResult.Should().Be(serviceReturnObject);
            }
        }
    }

    [Test]
    public async Task GetPagedApplications_ShouldReturnBadObjectResult_WhenServiceThrowsBadRequestException()
    {
        // arrange
        _mockApplicationsService = new Mock<Core.Interfaces.IApplicationsService>(MockBehavior.Strict);
        _mockApplicationsService
            .Setup(x => x.GetPagedApplications(
                It.IsAny<int>(), 
                It.IsAny<int>(),
                It.IsAny<string>(), 
                It.IsAny<bool>(),
                It.IsAny<string>(), 
                It.IsAny<string>(), 
                It.IsAny<string?>(), 
                It.IsAny<string?>()
                )).Throws(new BadRequestException("Test exception text"));

        var systemUnderTest = GenerateSystemUnderTest();

        // act
        var result = await systemUnderTest.GetPagedApplications();

        // assert
        Assert.IsInstanceOf<BadRequestObjectResult>(result);
    }
}
