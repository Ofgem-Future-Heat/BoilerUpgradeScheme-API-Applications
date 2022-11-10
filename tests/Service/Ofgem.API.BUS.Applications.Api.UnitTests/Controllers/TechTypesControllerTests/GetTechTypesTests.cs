using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using Ofgem.API.BUS.Applications.Domain;
using Ofgem.Lib.BUS.APIClient.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ofgem.API.BUS.Applications.Api.UnitTests.Controllers.TechTypesControllerTests;

/// <summary>
/// Tests for the TechTypesController.GetTechTypes method
/// </summary>
public class GetTechTypesTests : TechTypesControllerTestsBase
{
    [Test]
    public async Task GetTechTypes_ShouldReturnBadRequestObjectResult_WhenServiceThrowsBadRequestException()
    {
        // arrange
        _mockApplicationsService = new Moq.Mock<Core.Interfaces.IApplicationsService>(Moq.MockBehavior.Strict);
        _mockApplicationsService
            .Setup(m => m.GetTechTypeListAsync())
            .Throws(new BadRequestException("Test exception message"));

        var systemUnderTest = GenerateSystemUnderTest();

        // act
        var result = await systemUnderTest.GetTechTypes();

        // assert
        Assert.IsInstanceOf<BadRequestObjectResult>(result);
    }

    [Test]
    public async Task GetTechTypes_ShouldReturnOkObjectResultWithServiceObject_WhenServiceResponseIsValid()
    {
        // arrange
        var serviceReturnObject = new List<TechType>
        {
            new TechType(),
            new TechType()
        };

        _mockApplicationsService = new Moq.Mock<Core.Interfaces.IApplicationsService>(Moq.MockBehavior.Strict);
        _mockApplicationsService
            .Setup(x => x.GetTechTypeListAsync())
            .ReturnsAsync(serviceReturnObject);

        var systemUnderTest = GenerateSystemUnderTest();

        // act
        var result = await systemUnderTest.GetTechTypes();

        // assert
        Assert.IsInstanceOf<OkObjectResult>(result);
        var castResult = result as OkObjectResult;
        castResult.Should().NotBeNull();
        if (castResult is not null)
        {
            var techTypesList = castResult.Value as List<TechType>;
            techTypesList.Should().NotBeNull();
            if (techTypesList is not null)
            {
                techTypesList.Count.Should().Be(2);
            }
        }
    }
}
