using FluentAssertions;
using Moq;
using NUnit.Framework;
using Ofgem.Lib.BUS.APIClient.Domain.Exceptions;
using System;
using System.Threading.Tasks;

namespace Ofgem.API.BUS.Applications.Api.UnitTests.Controllers.ExternalApplicationsControllerTests;

[TestFixture]
public class GetApplicationByReferenceNumberTests : ExternalApplicationsControllerTestsBase
{
    [Test]
    public async Task GetApplicationByReferenceNumberAsync_Returns_OkResult()
    {
        // Arrange
        var systemUnderTest = GenerateSystemUnderTest();
        var expectedResult = systemUnderTest.Ok();

        // Act
        var result = await systemUnderTest.GetApplicationByReferenceNumberAsync("12345");

        // Assert
        result.Should().BeEquivalentTo(expectedResult);
    }

    [Test]
    public async Task GetApplicationByReferenceNumberAsync_Returns_NotFound_When_Application_NotFound()
    {
        // Arrange
        _mockApplicationsService.Setup(m => m.GetApplicationByReferenceNumber(It.IsAny<string>()).Result).Throws(new ResourceNotFoundException("It wasn't found"));
        var systemUnderTest = GenerateSystemUnderTest();
        var expectedResult = systemUnderTest.NotFound();

        // Act
        var result = await systemUnderTest.GetApplicationByReferenceNumberAsync("12345");

        // Assert
        result.Should().BeEquivalentTo(expectedResult);
    }

    [Test]
    public async Task GetApplicationByReferenceNumberAsync_Returns_BadRequest_When_Exception_Thrown()
    {
        // Arrange
        _mockApplicationsService.Setup(m => m.GetApplicationByReferenceNumber(It.IsAny<string>()).Result).Throws<Exception>();
        var systemUnderTest = GenerateSystemUnderTest();
        var expectedResult = systemUnderTest.BadRequest();

        // Act
        var result = await systemUnderTest.GetApplicationByReferenceNumberAsync("12345");

        // Assert
        result.Should().BeEquivalentTo(expectedResult);
    }
}
