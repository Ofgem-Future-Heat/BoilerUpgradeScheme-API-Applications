using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using Ofgem.Lib.BUS.APIClient.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ofgem.API.BUS.Applications.Api.UnitTests.Controllers.ApplicationsControllerTests;

/// <summary>
/// Tests for the ApplicationsController.UpdateApplicationStatusAsync method
/// </summary>
public class UpdateApplicationStatusAsyncTests : ApplicationsControllerTestsBase
{
    [Test]
    public async Task UpdateApplicationStatusAsync_ShouldThrowArgumentNullException_WhenApplicationIdIsNull()
    {
        // arrange
        var systemUnderTest = GenerateSystemUnderTest();

        // act
        Func<Task> act = () => _ = systemUnderTest.UpdateApplicationStatusAsync(null, Guid.NewGuid());

        // assert
        await act
          .Should().ThrowExactlyAsync<ArgumentNullException>()
          .WithParameterName("applicationId")
          .WithMessage("Value cannot be null. (Parameter 'applicationId')");
    }

    [Test]
    public async Task UpdateApplicationStatusAsync_ShouldThrowArgumentNullException_WhenApplicationStatusIdIsNull()
    {
        // arrange
        var systemUnderTest = GenerateSystemUnderTest();

        // act
        Func<Task> act = () => _ = systemUnderTest.UpdateApplicationStatusAsync(Guid.NewGuid(), null);

        // assert
        await act
          .Should().ThrowExactlyAsync<ArgumentNullException>()
          .WithParameterName("applicationStatusId")
          .WithMessage("Value cannot be null. (Parameter 'applicationStatusId')");
    }

    [Test]
    public async Task UpdateApplicationStatusAsync_ShouldReturnBadRequest_WhenServiceThrowsABadRequestException()
    {
        // arrange
        _mockApplicationsService = new Moq.Mock<Core.Interfaces.IApplicationsService>(Moq.MockBehavior.Strict);
        _mockApplicationsService
            .Setup(m => m.UpdateApplicationStatusAsync(It.IsAny<Guid>(), It.IsAny<Guid>()))
            .Throws(new BadRequestException("Test exception message"));

        var systemUnderTest = GenerateSystemUnderTest();

        // act
        var result = await systemUnderTest.UpdateApplicationStatusAsync(Guid.NewGuid(), Guid.NewGuid());

        // assert
        Assert.IsInstanceOf<BadRequestObjectResult>(result);
    }

    [Test]
    public async Task UpdateApplicationStatusAsync_ShouldReturnOk_WhenServiceReturnsEmptyListOfErrors()
    {
        // arrange
        _mockApplicationsService = new Moq.Mock<Core.Interfaces.IApplicationsService>(Moq.MockBehavior.Strict);
        _mockApplicationsService
            .Setup(m => m.UpdateApplicationStatusAsync(It.IsAny<Guid>(), It.IsAny<Guid>()))
            .ReturnsAsync(new List<string>());

        var systemUnderTest = GenerateSystemUnderTest();

        // act
        var result = await systemUnderTest.UpdateApplicationStatusAsync(Guid.NewGuid(), Guid.NewGuid());

        // assert
        Assert.IsInstanceOf<OkResult>(result);
    }

    [Test]
    public async Task UpdateApplicationStatusAsync_ShouldReturnConflictObjectResult_WhenServiceReturnsPopulatedListOfErrors()
    {
        // arrange
        _mockApplicationsService = new Moq.Mock<Core.Interfaces.IApplicationsService>(Moq.MockBehavior.Strict);
        _mockApplicationsService
            .Setup(m => m.UpdateApplicationStatusAsync(It.IsAny<Guid>(), It.IsAny<Guid>()))
            .ReturnsAsync(new List<string> { "Test error"});

        var systemUnderTest = GenerateSystemUnderTest();

        // act
        var result = await systemUnderTest.UpdateApplicationStatusAsync(Guid.NewGuid(), Guid.NewGuid());

        // assert
        Assert.IsInstanceOf<ConflictObjectResult>(result);
        result.Should().NotBeNull();
        if (result is not null)
        {
            var castResult = result as ConflictObjectResult;
            castResult.Should().NotBeNull();
            if (castResult is not null)
            {
                var value = castResult.Value as string;
                value.Should().NotBeNull();
            }
        }
    }
}
