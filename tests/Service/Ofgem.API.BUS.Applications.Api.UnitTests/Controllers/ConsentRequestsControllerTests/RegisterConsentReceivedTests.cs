using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using Ofgem.API.BUS.Applications.Domain.Entities.CommsObjects;
using System;
using System.Threading.Tasks;

namespace Ofgem.API.BUS.Applications.Api.UnitTests.Controllers.ConsentRequestsControllerTests;

/// <summary>
/// Tests for the ConsentRequestsController.RegisterConsentReceived method
/// </summary>
public class RegisterConsentReceivedTests : ConsentRequestsControllerTestsBase
{
    [Test]
    public async Task RegisterConsentReceived_ShouldReturn204NoContent_WhenServiceReturnsWithoutError()
    {
        // arrange
        var systemUnderTest = GenerateSystemUnderTest();

        // act
        var result = await systemUnderTest.RegisterConsentReceived(Guid.NewGuid(), new RegisterConsentReceivedRequest { UpdatedByUsername = "test" });

        // assert
        Assert.IsInstanceOf<NoContentResult>(result);
    }
}
