using FluentAssertions;
using FluentAssertions.Execution;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using Ofgem.API.BUS.Applications.Domain;
using Ofgem.API.BUS.Applications.Domain.Concrete;
using Ofgem.API.BUS.Applications.Domain.Entities.CommsObjects;
using Ofgem.Lib.BUS.APIClient.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ofgem.API.BUS.Applications.Api.UnitTests.Controllers.ExternalApplicationsControllerTests;

/// <summary>
/// Tests for the ApplicationsController.CreateApplicationAsync method
/// </summary>
public class CreateApplicationAsyncTests : ExternalApplicationsControllerTestsBase
{
    [Test]
    public async Task CreateApplicationAsync_ShouldReturnBadRequest_WhenModelStateIsInvalid()
    {
        var systemUnderTest = GenerateSystemUnderTest();
        systemUnderTest.ModelState.AddModelError("businessAccountId", "Required");
        systemUnderTest.ModelState.AddModelError("TechTypeId", "Required");

        // act
        var result = await systemUnderTest.CreateApplicationAsync(new CreateApplicationRequest());

        // assert
        Assert.IsInstanceOf<BadRequestObjectResult>(result);

        var castResult = result as ObjectResult;
        castResult.Should().NotBeNull();
        if (castResult is not null)
        {
            castResult.StatusCode.Should().Be(StatusCodes.Status400BadRequest);


            var data = castResult.Value as Dictionary<string, object>;
            data.Should().NotBeNull();
            if (data is not null)
            {
                data.Keys.Count.Should().Be(2);
                data.Values.Count.Should().Be(2);
            }
        }
    }

    [Test]
    public async Task CreateApplicationAsync_ShouldReturnBadRequestResult_WhenServiceThrowsBadRequestException()
    {
        // arrange
        _mockApplicationsService = new Mock<Core.Interfaces.IApplicationsService>(MockBehavior.Strict);
        _mockApplicationsService
            .Setup(m => m.CreateApplicationAsync(It.IsAny<CreateApplicationRequest>()))
            .Throws(new BadRequestException("Test exception text"));

        var systemUnderTest = GenerateSystemUnderTest();

        // act
        var result = await systemUnderTest.CreateApplicationAsync(new CreateApplicationRequest());

        // assert
        Assert.IsInstanceOf<BadRequestObjectResult>(result);
    }

    [Test]
    public async Task CreateApplicationAsync_ReturnsServiceResult_WhenValid()
    {
        // Arrange
        var newApplicationId = Guid.NewGuid();

        _mockApplicationsService = new Mock<Core.Interfaces.IApplicationsService>(MockBehavior.Strict);
        _mockApplicationsService
            .Setup(m => m.CreateApplicationAsync(It.IsAny<CreateApplicationRequest>()))
            .ReturnsAsync(new Application { ID = newApplicationId });

        _mockEmailService.Setup(m => m.SendInstallerPostApplicationEmailAsync(It.IsAny<Application>()).Result)
                         .Returns(new SendEmailResult { IsSuccess = true });

        var systemUnderTest = GenerateSystemUnderTest();

        // Act
        var result = await systemUnderTest.CreateApplicationAsync(new CreateApplicationRequest());

        // Assert
        using (new AssertionScope())
        {
            result.Should().BeOfType<OkObjectResult>();

            var castResult = result as OkObjectResult;
            castResult!.Value.Should().NotBeNull().And.BeOfType<Application>();

            var createdApplication = castResult.Value as Application;
            createdApplication!.ID.Should().Be(newApplicationId);
        }
    }
}
