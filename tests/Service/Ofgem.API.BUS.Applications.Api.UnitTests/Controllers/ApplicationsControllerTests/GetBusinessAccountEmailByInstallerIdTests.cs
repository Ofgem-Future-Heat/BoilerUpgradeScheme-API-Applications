using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using Ofgem.Lib.BUS.APIClient.Domain.Exceptions;
using System;
using System.Threading.Tasks;

namespace Ofgem.API.BUS.Applications.Api.UnitTests.Controllers.ApplicationsControllerTests;

/// <summary>
/// Tests for the ApplicationsController.GetBusinessAccountEmailByInstallerId method
/// </summary>
public class GetBusinessAccountEmailByInstallerIdTests : ApplicationsControllerTestsBase
{
    [Test]
    public async Task GetBusinessAccountEmailByInstallerId_ShouldReturnOkObjectResultWithServiceObject_WhenServiceReturnsAResult()
    {
        // arrange
        var serviceReturnObject = "testInstallerEmail@example.com";

        _mockApplicationsService = new Moq.Mock<Core.Interfaces.IApplicationsService>(Moq.MockBehavior.Strict);
        _mockApplicationsService
            .Setup(x => x.GetBusinessEmailAddressByInstallerId(It.IsAny<Guid>()))
            .ReturnsAsync(serviceReturnObject);

        var systemUnderTest = GenerateSystemUnderTest();

        // act
        var result = await systemUnderTest.GetBusinessAccountEmailByInstallerId(Guid.NewGuid());

        // assert
        Assert.IsInstanceOf<OkObjectResult>(result);
        var castResult = result as OkObjectResult;
        castResult.Should().NotBeNull();
        if (castResult is not null)
        {
            var retrievedEmailAddress = castResult.Value as string;
            retrievedEmailAddress.Should().NotBeNull();
            if (retrievedEmailAddress is not null)
            {
                retrievedEmailAddress.Should().Be(serviceReturnObject);
            }
        }
    }

    [Test]
    public async Task GetBusinessAccountEmailByInstallerId_ShouldReturnBadObjectResult_WhenServiceThrowsBadRequestException()
    {
        // arrange
        _mockApplicationsService = new Mock<Core.Interfaces.IApplicationsService>(MockBehavior.Strict);
        _mockApplicationsService
            .Setup(m => m.GetBusinessEmailAddressByInstallerId(It.IsAny<Guid>()))
            .Throws(new BadRequestException("Test exception text"));

        var systemUnderTest = GenerateSystemUnderTest();

        // act
        var result = await systemUnderTest.GetBusinessAccountEmailByInstallerId(Guid.NewGuid());

        // assert
        Assert.IsInstanceOf<BadRequestObjectResult>(result);
    }
}
