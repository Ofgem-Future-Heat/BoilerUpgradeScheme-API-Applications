using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using Ofgem.API.BUS.Applications.Domain.Entities.CommsObjects;
using Ofgem.API.BUS.PropertyConsents.Domain.Models.CommsObjects;
using Ofgem.Lib.BUS.APIClient.Domain.Exceptions;
using System;
using System.Threading.Tasks;

namespace Ofgem.API.BUS.Applications.Api.UnitTests.Controllers.ExternalApplicationsControllerTests;

/// <summary>
/// Tests for the ApplicationsController.RequestConsentEmailAsync method
/// </summary>
public class RequestConsentEmailAsyncTests : ExternalApplicationsControllerTestsBase
{
    [Test]
    public async Task RequestConsentEmailAsync_ShouldReturnOkObjectResultWithServiceObject_WhenServiceReturnsASuccessResult()
    {
        // arrange
        var applicationId = Guid.NewGuid();
        var consentRequestId = Guid.NewGuid();
        var consentTokenExpires = DateTime.UtcNow.AddDays(1);
        var serviceRequestObject = new RequestConsentEmailRequest
        {
            CreatedByUsername = "Test"
        };
        var serviceReturnObject = new SendConsentEmailResult
        {
            IsSuccess = true,
            ConsentRequestId = consentRequestId,
            ConsentTokenExpires = consentTokenExpires
        };

        _mockApplicationsService = new Mock<Core.Interfaces.IApplicationsService>(MockBehavior.Strict);
        _mockApplicationsService
            .Setup(x => x.SendConsentEmailAsync(applicationId, It.IsAny<string>()))
            .ReturnsAsync(serviceReturnObject);

        var systemUnderTest = GenerateSystemUnderTest();

        // act
        var result = await systemUnderTest.RequestConsentEmailAsync(applicationId, serviceRequestObject);

        // assert
        Assert.IsInstanceOf<OkObjectResult>(result);
        var castResult = result as OkObjectResult;
        castResult.Should().NotBeNull();
        if (castResult is not null)
        {
            var sendConsentEmailResult = castResult.Value as SendConsentEmailResult;
            sendConsentEmailResult.Should().NotBeNull();
            if (sendConsentEmailResult is not null)
            {
                sendConsentEmailResult.ConsentTokenExpires.Should().Be(consentTokenExpires);
                sendConsentEmailResult.ConsentRequestId.Should().Be(consentRequestId);
                sendConsentEmailResult.IsSuccess.Should().BeTrue();
            }
        }
    }

    [Test]
    public async Task RequestConsentEmailAsync_ShouldReturnBadObjectResult_WhenServiceThrowsBadRequestException()
    {
        // arrange
        var applicationId = Guid.NewGuid();
        var serviceRequestObject = new RequestConsentEmailRequest
        {
            CreatedByUsername = "Test"
        };
        _mockApplicationsService = new Mock<Core.Interfaces.IApplicationsService>(MockBehavior.Strict);
        _mockApplicationsService
            .Setup(m => m.SendConsentEmailAsync(applicationId, It.IsAny<string>()))
            .Throws(new BadRequestException("Test exception text"));

        var systemUnderTest = GenerateSystemUnderTest();

        // act
        var result = await systemUnderTest.RequestConsentEmailAsync(applicationId, serviceRequestObject);

        // assert
        Assert.IsInstanceOf<BadRequestObjectResult>(result);
    }

    [Test]
    public async Task RequestConsentEmailAsync_ShouldReturn500ResultWithServiceObject_WhenServiceReturnsANonSuccessResult()
    {
        // arrange
        var applicationId = Guid.NewGuid();
        var consentRequestId = Guid.NewGuid();
        var consentTokenExpires = DateTime.UtcNow.AddDays(1);
        var serviceReturnObject = new SendConsentEmailResult
        {
            IsSuccess = false,
            ConsentRequestId = consentRequestId,
            ConsentTokenExpires = consentTokenExpires
        };
        var serviceRequestObject = new RequestConsentEmailRequest
        {
            CreatedByUsername = "Test"
        };

        _mockApplicationsService = new Mock<Core.Interfaces.IApplicationsService>(MockBehavior.Strict);
        _mockApplicationsService
            .Setup(x => x.SendConsentEmailAsync(applicationId, It.IsAny<string>()))
            .ReturnsAsync(serviceReturnObject);

        var systemUnderTest = GenerateSystemUnderTest();

        // act
        var result = await systemUnderTest.RequestConsentEmailAsync(applicationId, serviceRequestObject);

        // assert
        Assert.IsInstanceOf<StatusCodeResult>(result);
        var statusCodeResult = result as StatusCodeResult;
        statusCodeResult.Should().NotBeNull();
        if (statusCodeResult is not null)
        {
            statusCodeResult.StatusCode.Should().Be(500);
        }
    }

    [Test]
    public async Task RequestConsentEmailAsync_ShouldReturn500ResultWithServiceObject_WhenServiceReturnsNull()
    {
        // arrange
        var applicationId = Guid.NewGuid();
        var serviceRequestObject = new RequestConsentEmailRequest
        {
            CreatedByUsername = "Test"
        };

        _mockApplicationsService = new Mock<Core.Interfaces.IApplicationsService>(MockBehavior.Strict);
        _mockApplicationsService
            .Setup(x => x.SendConsentEmailAsync(applicationId, It.IsAny<string>()))
            .ReturnsAsync(() => null!);

        var systemUnderTest = GenerateSystemUnderTest();

        // act
        var result = await systemUnderTest.RequestConsentEmailAsync(applicationId, serviceRequestObject);

        // assert
        Assert.IsInstanceOf<StatusCodeResult>(result);
        var statusCodeResult = result as StatusCodeResult;
        statusCodeResult.Should().NotBeNull();
        if (statusCodeResult is not null)
        {
            statusCodeResult.StatusCode.Should().Be(500);
        }
    }
}
