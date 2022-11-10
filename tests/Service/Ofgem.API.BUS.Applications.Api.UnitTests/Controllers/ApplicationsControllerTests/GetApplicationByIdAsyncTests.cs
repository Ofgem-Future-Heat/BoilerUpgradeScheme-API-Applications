using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using Ofgem.API.BUS.Applications.Domain;
using Ofgem.Lib.BUS.APIClient.Domain.Exceptions;
using System;
using System.Threading.Tasks;

namespace Ofgem.API.BUS.Applications.Api.UnitTests.Controllers.ApplicationsControllerTests;

/// <summary>
/// Tests for the ApplicationsController.GetApplicationByIdAsync method
/// </summary>
public class GetApplicationByIdAsyncTests : ApplicationsControllerTestsBase
{
    [Test]
    public async Task GetApplicationByIdAsync_ShouldReturnOkObjectResultWithServiceObject_WhenApplicationIdIsValid()
    {
        // arrange
        var applicationId = Guid.NewGuid();
        var serviceReturnObject = new Application
        {
            ID = applicationId
        };

        _mockApplicationsService = new Moq.Mock<Core.Interfaces.IApplicationsService>(Moq.MockBehavior.Strict);
        _mockApplicationsService
            .Setup(x => x.GetApplicationByIdAsync(applicationId))
            .ReturnsAsync(serviceReturnObject);

        var systemUnderTest = GenerateSystemUnderTest();

        // act
        var result = await systemUnderTest.GetApplicationByIdAsync(applicationId);

        // assert
        Assert.IsInstanceOf<OkObjectResult>(result);
        var castResult = result as OkObjectResult;
        castResult.Should().NotBeNull();
        if (castResult is not null)
        {
            var retrievedApplication = castResult.Value as Application;
            retrievedApplication.Should().NotBeNull();
            if (retrievedApplication is not null)
            {
                retrievedApplication.ID.Should().Be(applicationId);
            }
        }
    }

    [Test]
    public async Task GetApplicationByIdAsync_ShouldReturnBadRequestObjectResult_WhenServiceThrowsBadRequestException()
    {
        // arrange
        _mockApplicationsService = new Mock<Core.Interfaces.IApplicationsService>(MockBehavior.Strict);
        _mockApplicationsService
            .Setup(m => m.GetApplicationByIdAsync(It.IsAny<Guid>()))
            .Throws(new BadRequestException("Test exception text"));

        var systemUnderTest = GenerateSystemUnderTest();

        // act
        var result = await systemUnderTest.GetApplicationByIdAsync(Guid.NewGuid());

        // assert
        Assert.IsInstanceOf<BadRequestObjectResult>(result);
    }
}
