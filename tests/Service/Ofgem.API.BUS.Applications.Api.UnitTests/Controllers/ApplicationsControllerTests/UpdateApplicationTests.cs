using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using Ofgem.API.BUS.Applications.Domain;
using Ofgem.Lib.BUS.APIClient.Domain.Exceptions;
using System.Threading.Tasks;

namespace Ofgem.API.BUS.Applications.Api.UnitTests.Controllers.ApplicationsControllerTests;

/// <summary>
/// Tests for the ApplicationsController.UpdateEpcApplication method
/// </summary>
public class UpdateApplicationTests : ApplicationsControllerTestsBase
{
    [Test]
    [TestCase(true)]
    [TestCase(false)]
    public async Task UpdateApplication_ShouldReturnOkObjectResultWithServiceObject_WhenServiceReturnsAResult(bool updateApplicationResult)
    {
        // arrange
        var updateApplication = new Application();

        _mockApplicationsService = new Moq.Mock<Core.Interfaces.IApplicationsService>(Moq.MockBehavior.Strict);
        _mockApplicationsService
            .Setup(x => x.UpdateApplicationAsync(updateApplication))
            .ReturnsAsync(updateApplicationResult);

        var systemUnderTest = GenerateSystemUnderTest();

        // act
        var result = await systemUnderTest.UpdateApplication(updateApplication);

        // assert
        Assert.IsInstanceOf<OkObjectResult>(result);
        var castResult = result as OkObjectResult;
        castResult.Should().NotBeNull();
        if (castResult is not null)
        {
            var outcomeBool = castResult.Value as bool?;
            outcomeBool.Should().NotBeNull();
            if (outcomeBool is not null)
            {
                outcomeBool.Should().Be(updateApplicationResult);
            }
        }
    }

    [Test]
    public async Task UpdateApplication_ShouldReturnBadObjectResult_WhenServiceThrowsBadRequestException()
    {
        // arrange
        _mockApplicationsService = new Mock<Core.Interfaces.IApplicationsService>(MockBehavior.Strict);
        _mockApplicationsService
            .Setup(m => m.UpdateApplicationAsync(It.IsAny<Application>()))
            .Throws(new BadRequestException("Test exception text"));

        var systemUnderTest = GenerateSystemUnderTest();

        // act
        var result = await systemUnderTest.UpdateApplication(new Application());

        // assert
        Assert.IsInstanceOf<BadRequestObjectResult>(result);
    }
}
