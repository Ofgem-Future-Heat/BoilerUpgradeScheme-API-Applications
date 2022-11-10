using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Moq;
using NUnit.Framework;
using Ofgem.API.BUS.Applications.Domain;
using Ofgem.Lib.BUS.APIClient.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ofgem.API.BUS.Applications.Api.UnitTests.Controllers.ApplicationsControllerTests;

/// <summary>
/// Tests for the ApplicationsController.GetApplicationsByApplicationAddressUprn method
/// </summary>
public class GetApplicationsByBusinessIdAndUprnTests : ApplicationsControllerTestsBase
{
    
    [Test]
    public async Task GetApplicationsByApplicationAddressUprn_ShouldReturnBadObjectResult_WhenServiceThrowsBadRequestException()
    {
        // arrange
        _mockApplicationsService = new Mock<Core.Interfaces.IApplicationsService>(MockBehavior.Strict);
        _mockApplicationsService
            .Setup(m => m.CheckDuplicateApplicationAsync(It.IsAny<string>(), It.IsAny<Guid>()))
            .Throws(new BadRequestException("Test exception text"));

        var systemUnderTest = GenerateSystemUnderTest();

        // act
        var result = await systemUnderTest.CheckDuplicateApplication("000000000001", Guid.NewGuid());

        // assert
        Assert.IsInstanceOf<BadRequestObjectResult>(result);
    }
}
