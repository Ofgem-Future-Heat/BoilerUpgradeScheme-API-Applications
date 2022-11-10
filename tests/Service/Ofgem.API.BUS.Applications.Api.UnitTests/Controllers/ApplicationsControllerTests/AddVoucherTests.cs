using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using Ofgem.API.BUS.Applications.Domain;
using Ofgem.API.BUS.Applications.Domain.Entities.CommsObjects;
using Ofgem.Lib.BUS.APIClient.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ofgem.API.BUS.Applications.Api.UnitTests.Controllers.ApplicationsControllerTests;

/// <summary>
/// Tests for the ApplicationsController.AddVoucher method
/// </summary>
public class AddVoucherTests : ApplicationsControllerTestsBase
{
    [Test]
    public async Task AddVoucher_ShouldReturnOkResultAndVoucher_WhenParametersAreValid()
    {
        // arrange
        var applicationId = Guid.NewGuid();
        var addVoucherRequest = new AddVoucherRequest
        {
            ApplicationID = applicationId,
            TechTypeId = Guid.NewGuid()
        };
        var returnVoucher = new Voucher
        {
            ID = Guid.NewGuid(),
            ApplicationID = applicationId
        };

        _mockApplicationsService = new Moq.Mock<Core.Interfaces.IApplicationsService>(Moq.MockBehavior.Strict);
        _mockApplicationsService.Setup(m => m.AddVoucherAsync(addVoucherRequest)).ReturnsAsync(returnVoucher);

        var systemUnderTest = GenerateSystemUnderTest();

        // act
        var result = await systemUnderTest.AddVoucher(applicationId, addVoucherRequest);

        // Assert
        Assert.IsInstanceOf<OkObjectResult>(result);
        var resultInstance = result as OkObjectResult;
        resultInstance.Should().NotBeNull();
        if (resultInstance is not null)
        {
            resultInstance.StatusCode.Should().Be(StatusCodes.Status200OK);
            Assert.IsInstanceOf<Voucher>(resultInstance.Value);
            var resultVoucher = resultInstance.Value as Voucher;
            resultVoucher.Should().NotBeNull();
            if (resultVoucher is not null)
            {
                resultVoucher.ID.Should().Be(returnVoucher.ID);
                resultVoucher.ApplicationID.Should().Be(returnVoucher.ApplicationID);
            }
        }
    }

    [Test]
    public async Task AddVoucher_ShouldReturnBadRequestResult_WhenApplicationIDsDoNotMatch()
    {
        // arrange
        var applicationId = Guid.NewGuid();
        var addVoucherRequest = new AddVoucherRequest
        {
            ApplicationID = Guid.NewGuid(),
            TechTypeId = Guid.NewGuid()
        };

        var systemUnderTest = GenerateSystemUnderTest();

        // act
        var result = await systemUnderTest.AddVoucher(applicationId, addVoucherRequest);

        // Assert
        Assert.IsInstanceOf<BadRequestObjectResult>(result);
        var objectResult = result as ObjectResult;
        objectResult.Should().NotBeNull();
        if (objectResult is not null)
        {
            objectResult.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
            objectResult.Value.Should().Be("Application ID in body must match Application ID in route");
        }
    }

    [Test]
    public async Task AddVoucher_ShouldReturnBadRequestResult_WhenModelStateIsNotValid()
    {
        // arrange
        var applicationId = Guid.NewGuid();
        var addVoucherRequest = new AddVoucherRequest
        {
            ApplicationID = applicationId,
            TechTypeId = Guid.NewGuid()
        };

        var systemUnderTest = GenerateSystemUnderTest();
        systemUnderTest.ModelState.AddModelError("ApplicationID", "Required");
        systemUnderTest.ModelState.AddModelError("TechTypeId", "Required");

        // act
        var result = await systemUnderTest.AddVoucher(applicationId, addVoucherRequest);

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
    public async Task AddVoucher_ShouldReturnBadRequestResult_WhenServiceThrowsBadRequestException()
    {
        // arrange
        var applicationId = Guid.NewGuid();
        var addVoucherRequest = new AddVoucherRequest
        {
            ApplicationID = applicationId,
            TechTypeId = Guid.NewGuid()
        };

        _mockApplicationsService = new Mock<Core.Interfaces.IApplicationsService>(MockBehavior.Strict);
        _mockApplicationsService
            .Setup(m => m.AddVoucherAsync(addVoucherRequest))
            .Throws(new BadRequestException("Test exception text"));

        var systemUnderTest = GenerateSystemUnderTest();

        // act
        var result = await systemUnderTest.AddVoucher(applicationId, addVoucherRequest);

        // assert
        Assert.IsInstanceOf<BadRequestObjectResult>(result);
    }
}
