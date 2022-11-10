using FluentAssertions;
using NUnit.Framework;
using Ofgem.API.BUS.Applications.Api.Controllers;
using System;

namespace Ofgem.API.BUS.Applications.Api.UnitTests.Controllers.ApplicationsControllerTests;

/// <summary>
/// Tests for the ApplicationsController constructor
/// </summary>
public class ConstructorTests : ApplicationsControllerTestsBase
{
    [Test]
    public void Constructor_Throws_ArgumentNullException_If_IApplicationsService_Is_Null()
    {
        // act
        Action act = () => _ = new ApplicationsController(null);

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
