using FluentAssertions;
using NUnit.Framework;
using Ofgem.API.BUS.Applications.Api.Controllers;
using System;

namespace Ofgem.API.BUS.Applications.Api.UnitTests.Controllers.ConsentRequestsControllerTests;

/// <summary>
/// Tests for the ConsentRequestsController constructor
/// </summary>
public class ConstructorTests : ConsentRequestsControllerTestsBase
{
    [Test]
    public void Constructor_Throws_ArgumentNullException_If_IApplicationsService_Is_Null()
    {
        // act
        Action act = () => _ = new ConsentRequestsController(null!);

        //assert
        act
          .Should().ThrowExactly<ArgumentNullException>()
          .WithParameterName("applicationsService")
          .WithMessage("Value cannot be null. (Parameter 'applicationsService')");
    }

    [Test]
    public void Constructor_Allows_Non_Null_IApplicationsService()
    {
        // act
        var systemUnderTest = GenerateSystemUnderTest();

        // assert
        systemUnderTest.Should().NotBeNull();
    }
}
