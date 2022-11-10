using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using Ofgem.Lib.BUS.APIClient.Domain.Exceptions;
using System;

namespace Ofgem.API.BUS.Applications.Api.UnitTests.Controllers.ApplicationsControllerTests;

/// <summary>
/// Tests for the ApplicationsController.CheckForActiveConsentByApplicationId method
/// </summary>
public class HasLiveApplicationsAgainstExternalUserIdTests : ApplicationsControllerTestsBase
{
    [Test]
    public void HasLiveApplicationsAgainstExternalUserId_ReturnsBadRequest_WhenServiceThrowsBadRequestException()
    {
        // arrange
        _mockApplicationsService = new Moq.Mock<Core.Interfaces.IApplicationsService>(Moq.MockBehavior.Strict);
        _mockApplicationsService
            .Setup(m => m.HasLiveApplicationsAgainstExternalUserId(It.IsAny<Guid>()))
            .Throws(new BadRequestException("Test exception text"));

        var systemUnderTest = GenerateSystemUnderTest();

        // act
        var result = systemUnderTest.HasLiveApplicationsAgainstExternalUserId(Guid.NewGuid());

        // assert
        Assert.IsInstanceOf<BadRequestObjectResult>(result);

    }

    [Test]
    [TestCase(true)]
    [TestCase(false)]
    public void HasLiveApplicationsAgainstExternalUserId_ReturnsServiceResult_WhenValid(bool testCase)
    {
        // arrange
        var applicationId = Guid.NewGuid();

        _mockApplicationsService = new Moq.Mock<Core.Interfaces.IApplicationsService>(Moq.MockBehavior.Strict);
        _mockApplicationsService
            .Setup(m => m.HasLiveApplicationsAgainstExternalUserId(applicationId))
            .Returns(testCase);

        var systemUnderTest = GenerateSystemUnderTest();

        // act
        var result = systemUnderTest.HasLiveApplicationsAgainstExternalUserId(applicationId);

        // assert
        Assert.IsInstanceOf<OkObjectResult>(result);
    }
}

