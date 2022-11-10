using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using Ofgem.API.BUS.Applications.Domain;
using Ofgem.Lib.BUS.APIClient.Domain.Exceptions;
using System.Threading.Tasks;

namespace Ofgem.API.BUS.Applications.Api.UnitTests.Controllers.ApplicationsControllerTests;

/// <summary>
/// Tests for the ApplicationsController.GetApplicationByReferenceNumberAsync method
/// </summary>
public class GetApplicationByReferenceNumberAsyncTests : ApplicationsControllerTestsBase
{
    [Test]
    public async Task GetApplicationByReferenceNumberAsync_ShouldReturnOkObjectResultWithServiceObject_WhenReferenceNumberIdIsValid()
    {
        // arrange
        var applicationReferenceNumber = "TESTREF";
        var serviceReturnObject = new Application
        {
            ReferenceNumber = applicationReferenceNumber
        };

        _mockApplicationsService = new Moq.Mock<Core.Interfaces.IApplicationsService>(Moq.MockBehavior.Strict);
        _mockApplicationsService
            .Setup(x => x.GetApplicationByReferenceNumber(applicationReferenceNumber))
            .ReturnsAsync(serviceReturnObject);

        var systemUnderTest = GenerateSystemUnderTest();

        // act
        var result = await systemUnderTest.GetApplicationByReferenceNumberAsync(applicationReferenceNumber);

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
                retrievedApplication.ReferenceNumber.Should().Be(applicationReferenceNumber);
            }
        }
    }

    [Test]
    public async Task GetApplicationByReferenceNumberAsync_ShouldReturnBadObjectResult_WhenServiceThrowsBadRequestException()
    {
        // arrange
        _mockApplicationsService = new Mock<Core.Interfaces.IApplicationsService>(MockBehavior.Strict);
        _mockApplicationsService
            .Setup(m => m.GetApplicationByReferenceNumber(It.IsAny<string>()))
            .Throws(new BadRequestException("Test exception text"));

        var systemUnderTest = GenerateSystemUnderTest();

        // act
        var result = await systemUnderTest.GetApplicationByReferenceNumberAsync("TESTREF");

        // assert
        Assert.IsInstanceOf<BadRequestObjectResult>(result);
    }
}
