using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using Ofgem.Lib.BUS.APIClient.Domain.Exceptions;
using System.Threading.Tasks;
using Ofgem.API.BUS.Applications.Domain.Entities.CommsObjects;

namespace Ofgem.API.BUS.Applications.Api.UnitTests.Controllers.ApplicationsControllerTests;

/// <summary>
/// Tests for the ApplicationsController.UpdateContact method
/// </summary>
public class UpdateContactTests : ApplicationsControllerTestsBase
{
    [Test]
    [TestCase(true)]
    [TestCase(false)]
    public async Task UpdateContact_ShouldReturnOkResultWithServiceObject_WhenServiceReturnsAResult(bool updateContactResult)
    {
        // arrange
        var updateContact = new UpdateContactRequest();

        _mockApplicationsService = new Moq.Mock<Core.Interfaces.IApplicationsService>(Moq.MockBehavior.Strict);
        _mockApplicationsService
            .Setup(x => x.UpdateContactAsync(updateContact))
            .ReturnsAsync(updateContactResult);

        var systemUnderTest = GenerateSystemUnderTest();

        // act
        var result = await systemUnderTest.UpdateContact(updateContact);

        // assert
        Assert.IsInstanceOf<OkObjectResult>(result);
    }

    [Test]
    public async Task UpdateContact_ShouldReturnBadObjectResult_WhenServiceThrowsBadRequestException()
    {
        // arrange
        _mockApplicationsService = new Mock<Core.Interfaces.IApplicationsService>(MockBehavior.Strict);
        _mockApplicationsService
            .Setup(m => m.UpdateContactAsync(It.IsAny<UpdateContactRequest>()))
            .Throws(new BadRequestException("Test exception text"));

        var systemUnderTest = GenerateSystemUnderTest();

        // act
        var result = await systemUnderTest.UpdateContact(new UpdateContactRequest());

        // assert
        Assert.IsInstanceOf<BadRequestObjectResult>(result);
    }
}
