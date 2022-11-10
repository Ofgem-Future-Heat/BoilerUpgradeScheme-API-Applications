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
/// Tests for the ApplicationsController.UpdateVoucherStatusAsync method
/// </summary>
public class UpdateVoucherStatusAsyncTests : ApplicationsControllerTestsBase
{
    [Test]
    public async Task UpdateVoucherStatusAsync_ShouldThrowArgumentNullException_WhenVoucherIdIsNull()
    {
        // arrange
        var systemUnderTest = GenerateSystemUnderTest();

        // act
        Func<Task> act = () => _ = systemUnderTest.UpdateVoucherStatusAsync(null, Guid.NewGuid());

        // assert
        await act
          .Should().ThrowExactlyAsync<ArgumentNullException>()
          .WithParameterName("voucherId")
          .WithMessage("Value cannot be null. (Parameter 'voucherId')");
    }

    [Test]
    public async Task UpdateVoucherStatusAsync_ShouldThrowArgumentNullException_WhenVoucherStatusIdIsNull()
    {
        // arrange
        var systemUnderTest = GenerateSystemUnderTest();

        // act
        Func<Task> act = () => _ = systemUnderTest.UpdateVoucherStatusAsync(Guid.NewGuid(), null);

        // assert
        await act
          .Should().ThrowExactlyAsync<ArgumentNullException>()
          .WithParameterName("voucherStatusId")
          .WithMessage("Value cannot be null. (Parameter 'voucherStatusId')");
    }

    [Test]
    public async Task UpdateVoucherStatusAsync_ShouldReturnBadRequest_WhenServiceThrowsABadRequestException()
    {
        // arrange
        _mockApplicationsService = new Moq.Mock<Core.Interfaces.IApplicationsService>(Moq.MockBehavior.Strict);
        _mockApplicationsService
            .Setup(m => m.UpdateVoucherStatusAsync(It.IsAny<Guid>(), It.IsAny<Guid>()))
            .Throws(new BadRequestException("Test exception message"));

        var systemUnderTest = GenerateSystemUnderTest();

        // act
        var result = await systemUnderTest.UpdateVoucherStatusAsync(Guid.NewGuid(), Guid.NewGuid());

        // assert
        Assert.IsInstanceOf<BadRequestObjectResult>(result);
    }

    [Test]
    public async Task UpdateVoucherStatusAsync_ShouldReturnOk_WhenServiceReturnsEmptyListOfErrors()
    {
        // arrange
        _mockApplicationsService = new Moq.Mock<Core.Interfaces.IApplicationsService>(Moq.MockBehavior.Strict);
        _mockApplicationsService
            .Setup(m => m.UpdateVoucherStatusAsync(It.IsAny<Guid>(), It.IsAny<Guid>()))
            .ReturnsAsync(new List<string>());

        var systemUnderTest = GenerateSystemUnderTest();

        // act
        var result = await systemUnderTest.UpdateVoucherStatusAsync(Guid.NewGuid(), Guid.NewGuid());

        // assert
        Assert.IsInstanceOf<OkResult>(result);
    }

    [Test]
    public async Task UpdateVoucherStatusAsync_ShouldReturnConflictObjectResult_WhenServiceReturnsPopulatedListOfErrors()
    {
        // arrange
        _mockApplicationsService = new Moq.Mock<Core.Interfaces.IApplicationsService>(Moq.MockBehavior.Strict);
        _mockApplicationsService
            .Setup(m => m.UpdateVoucherStatusAsync(It.IsAny<Guid>(), It.IsAny<Guid>()))
            .ReturnsAsync(new List<string> { "Test error" });

        var systemUnderTest = GenerateSystemUnderTest();

        // act
        var result = await systemUnderTest.UpdateVoucherStatusAsync(Guid.NewGuid(), Guid.NewGuid());

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
