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
/// Tests for the ApplicationsController.GetApplicationsByApplicationAddressUprn method
/// </summary>
public class GetApplicationsByApplicationAddressUprnTests : ApplicationsControllerTestsBase
{
    [Test]
    public async Task GetApplicationsByApplicationAddressUprn_ShouldReturnOkObjectResultWithServiceObject_WhenUprnIsValid()
    {
        // arrange
        var testUprn = "000000000001";
        var serviceReturnObject = new List<Application>
        {
            new Application()
        };

        _mockApplicationsService = new Moq.Mock<Core.Interfaces.IApplicationsService>(Moq.MockBehavior.Strict);
        _mockApplicationsService
            .Setup(x => x.GetApplicationsByUprnAsync(testUprn))
            .ReturnsAsync(serviceReturnObject);

        var systemUnderTest = GenerateSystemUnderTest();

        // act
        var result = await systemUnderTest.GetApplicationsByApplicationAddressUprn(testUprn);

        // assert
        Assert.IsInstanceOf<OkObjectResult>(result);
        var castResult = result as OkObjectResult;
        castResult.Should().NotBeNull();
        if (castResult is not null)
        {
            var retrievedApplications = castResult.Value as List<Application>;
            retrievedApplications.Should().NotBeNull();
            if (retrievedApplications is not null)
            {
                retrievedApplications.Count.Should().Be(1);
            }
        }
    }

    [Test]
    public async Task GetApplicationsByApplicationAddressUprn_ShouldReturnBadObjectResult_WhenServiceThrowsBadRequestException()
    {
        // arrange
        _mockApplicationsService = new Mock<Core.Interfaces.IApplicationsService>(MockBehavior.Strict);
        _mockApplicationsService
            .Setup(m => m.GetApplicationsByUprnAsync(It.IsAny<string>()))
            .Throws(new BadRequestException("Test exception text"));

        var systemUnderTest = GenerateSystemUnderTest();

        // act
        var result = await systemUnderTest.GetApplicationsByApplicationAddressUprn("000000000001");

        // assert
        Assert.IsInstanceOf<BadRequestObjectResult>(result);
    }
}
