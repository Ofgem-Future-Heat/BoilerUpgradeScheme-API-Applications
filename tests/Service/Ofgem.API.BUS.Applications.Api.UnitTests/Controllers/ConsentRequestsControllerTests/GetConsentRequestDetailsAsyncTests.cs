using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using Ofgem.API.BUS.Applications.Domain.Entities.CommsObjects;
using Ofgem.API.BUS.PropertyConsents.Domain.Models.CommsObjects;
using Ofgem.Lib.BUS.APIClient.Domain.Exceptions;
using System;
using System.Threading.Tasks;

namespace Ofgem.API.BUS.Applications.Api.UnitTests.Controllers.ConsentRequestsControllerTests;

/// <summary>
/// Tests for the ConsentRequestsController.GetConsentRequestDetailsAsync method
/// </summary>
public class GetConsentRequestDetailsAsyncTests : ConsentRequestsControllerTestsBase
{
    [Test]
    public async Task GetConsentRequestDetailsAsync_ShouldReturnBadRequestObjectResult_WhenServiceThrowsBadRequestException()
    {
        // arrange
        var consentRequestId = Guid.NewGuid();
        _mockApplicationsService = new Moq.Mock<Core.Interfaces.IApplicationsService>(Moq.MockBehavior.Strict);
        _mockApplicationsService
            .Setup(m => m.GetConsentRequestDetailsAsync(consentRequestId))
            .Throws(new BadRequestException("Test exception message"));

        var systemUnderTest = GenerateSystemUnderTest();

        // act
        var result = await systemUnderTest.GetConsentRequestDetailsAsync(consentRequestId);

        // assert
        Assert.IsInstanceOf<BadRequestObjectResult>(result);
    }

    [Test]
    public async Task GetConsentRequestDetailsAsync_ShouldReturnOkObjectResultWithServiceObject_WhenConsentRequestIdIsValid()
    {
        // arrange
        var consentRequestId = Guid.NewGuid();
        var applicationReference = "TESTREF";
        var serviceReturnObject = new GetConsentRequestDetailsResult
        {
            IsSuccess = true,
            ConsentRequestSummary = new ConsentRequestSummary
            {
                ApplicationReferenceNumber = applicationReference
            }
        };

        _mockApplicationsService = new Moq.Mock<Core.Interfaces.IApplicationsService>(Moq.MockBehavior.Strict);
        _mockApplicationsService
            .Setup(x => x.GetConsentRequestDetailsAsync(consentRequestId))
            .ReturnsAsync(serviceReturnObject);

        var systemUnderTest = GenerateSystemUnderTest();

        // act
        var result = await systemUnderTest.GetConsentRequestDetailsAsync(consentRequestId);

        // assert
        Assert.IsInstanceOf<OkObjectResult>(result);
        var castResult = result as OkObjectResult;
        castResult.Should().NotBeNull();
        if (castResult is not null)
        {
            var getConsentRequestDetailsResult = castResult.Value as GetConsentRequestDetailsResult;
            getConsentRequestDetailsResult.Should().NotBeNull();
            if (getConsentRequestDetailsResult is not null)
            {
                getConsentRequestDetailsResult.ConsentRequestSummary.Should().NotBeNull();
                if (getConsentRequestDetailsResult.ConsentRequestSummary is not null)
                {
                    getConsentRequestDetailsResult.ConsentRequestSummary.ApplicationReferenceNumber.Should().Be(applicationReference);
                }
            }
        }
    }
}
