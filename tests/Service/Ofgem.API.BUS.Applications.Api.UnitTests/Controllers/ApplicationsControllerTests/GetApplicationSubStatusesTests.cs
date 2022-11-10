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
/// Tests for the ApplicationsController.GetApplicationSubStatuses method
/// </summary>
public class GetApplicationSubStatusesTests : ApplicationsControllerTestsBase
{
    [Test]
    public async Task GetApplicationSubStatuses_ShouldReturnOkObjectResultWithServiceObject_WhenServiceReturnsAResult()
    {
        // arrange
        var serviceReturnObject = new List<ApplicationSubStatus>
        {
            new ApplicationSubStatus
            {
                DisplayName = "Test Display Name"
            }
        };

        _mockApplicationsService = new Moq.Mock<Core.Interfaces.IApplicationsService>(Moq.MockBehavior.Strict);
        _mockApplicationsService
            .Setup(x => x.GetApplicationSubStatusesListAsync())
            .ReturnsAsync(serviceReturnObject);

        var systemUnderTest = GenerateSystemUnderTest();

        // act
        var result = await systemUnderTest.GetApplicationSubStatuses();

        // assert
        Assert.IsInstanceOf<OkObjectResult>(result);
        var castResult = result as OkObjectResult;
        castResult.Should().NotBeNull();
        if (castResult is not null)
        {
            var retrievedApplicationSubStatusList = castResult.Value as List<ApplicationSubStatus>;
            retrievedApplicationSubStatusList.Should().NotBeNull();
            if (retrievedApplicationSubStatusList is not null)
            {
                retrievedApplicationSubStatusList.Count.Should().Be(1);
            }
        }
    }

    [Test]
    public async Task GetApplicationSubStatuses_ShouldReturnBadObjectResult_WhenServiceThrowsBadRequestException()
    {
        // arrange
        _mockApplicationsService = new Mock<Core.Interfaces.IApplicationsService>(MockBehavior.Strict);
        _mockApplicationsService
            .Setup(m => m.GetApplicationSubStatusesListAsync())
            .Throws(new BadRequestException("Test exception text"));

        var systemUnderTest = GenerateSystemUnderTest();

        // act
        var result = await systemUnderTest.GetApplicationSubStatuses();

        // assert
        Assert.IsInstanceOf<BadRequestObjectResult>(result);
    }
}
