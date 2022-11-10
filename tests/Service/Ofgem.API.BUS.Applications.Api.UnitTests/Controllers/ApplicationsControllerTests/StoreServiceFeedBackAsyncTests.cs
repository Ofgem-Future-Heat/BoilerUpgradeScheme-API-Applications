using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using Ofgem.API.BUS.Applications.Domain;
using Ofgem.API.BUS.Applications.Domain.Entities.CommsObjects;
using Ofgem.Lib.BUS.APIClient.Domain.Exceptions;
using System.Threading.Tasks;

namespace Ofgem.API.BUS.Applications.Api.UnitTests.Controllers.ApplicationsControllerTests;

/// <summary>
/// Tests for the ApplicationsController.UpdateVoucher method
/// </summary>
public class StoreServiceFeedBackAsyncTests : ApplicationsControllerTestsBase
{
    [Test]
    public async Task StoreServiceFeedBackAsync_ShouldReturnOkResultWithServiceObject_WhenServiceReturnsAResult()
    {
        // arrange
        var feedback = new StoreServiceFeedbackRequest();
        var response = new StoreServiceFeedbackResult{ IsSuccess = true };

        _mockApplicationsService = new Moq.Mock<Core.Interfaces.IApplicationsService>(Moq.MockBehavior.Strict);
        _mockApplicationsService
            .Setup(x => x.StoreServiceFeedbackAsync(feedback))
            .ReturnsAsync(response);

        var systemUnderTest = GenerateSystemUnderTest();

        // act
        var result = await systemUnderTest.StoreServiceFeedBack(feedback);

        // assert
        Assert.IsInstanceOf<OkObjectResult>(result);
    }

    [Test]
    public async Task StoreServiceFeedBackAsync_ShouldReturnBadObjectResult_WhenServiceThrowsBadRequestException()
    {
        // arrange
        var feedback = new StoreServiceFeedbackRequest();
        var response = new StoreServiceFeedbackResult { IsSuccess = false};

        _mockApplicationsService = new Mock<Core.Interfaces.IApplicationsService>(MockBehavior.Strict);
        _mockApplicationsService
            .Setup(m => m.StoreServiceFeedbackAsync(feedback))
            .ReturnsAsync(response);  

        var systemUnderTest = GenerateSystemUnderTest();

        // act
        var result = await systemUnderTest.StoreServiceFeedBack(feedback);

        // assert
        Assert.IsInstanceOf<BadRequestObjectResult>(result);
    }
}
