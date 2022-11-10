using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using Ofgem.Lib.BUS.APIClient.Domain.Exceptions;
using System;
using System.Threading.Tasks;

namespace Ofgem.API.BUS.Applications.Api.UnitTests.Controllers.ApplicationsControllerTests;

/// <summary>
/// Tests for the ApplicationsController.CheckForActiveConsentByApplicationId method
/// </summary>
public class CheckForActiveConsentByApplicationIdTests : ApplicationsControllerTestsBase
{
    [Test]
    public async Task CheckForActiveConsentByApplicationId_ReturnsBadRequest_WhenServiceThrowsBadRequestException()
    {
        // arrange
        _mockApplicationsService = new Moq.Mock<Core.Interfaces.IApplicationsService>(Moq.MockBehavior.Strict);
        _mockApplicationsService
            .Setup(m => m.CheckForActiveConsentByApplicationIdAsync(It.IsAny<Guid>()))
            .Throws(new BadRequestException("Test exception text"));

        var systemUnderTest = GenerateSystemUnderTest();

        // act
        var result = await systemUnderTest.CheckForActiveConsentByApplicationId(Guid.NewGuid());

        // assert
        Assert.IsInstanceOf<BadRequestObjectResult>(result);

    }

    [Test]
    [TestCase(true)]
    [TestCase(false)]
    public async Task CheckForActiveConsentByApplicationId_ReturnsServiceResult_WhenValid(bool testCase)
    {
        // arrange
        var applicationId = Guid.NewGuid();

        _mockApplicationsService = new Moq.Mock<Core.Interfaces.IApplicationsService>(Moq.MockBehavior.Strict);
        _mockApplicationsService
            .Setup(m => m.CheckForActiveConsentByApplicationIdAsync(applicationId))
            .ReturnsAsync(testCase);

        var systemUnderTest = GenerateSystemUnderTest();

        // act
        var result = await systemUnderTest.CheckForActiveConsentByApplicationId(applicationId);

        // assert
        Assert.IsInstanceOf<OkObjectResult>(result);
        var castResult = result as OkObjectResult;
        castResult.Should().NotBeNull();
        if (castResult is not null)
        {
            castResult.Value.Should().Be(testCase);
        }
    }
}
