using FluentAssertions;
using NUnit.Framework;
using Ofgem.API.BUS.Applications.Api.Controllers;
using System;

namespace Ofgem.API.BUS.Applications.Api.UnitTests.Controllers.TechTypesControllerTests;

/// <summary>
/// Tests for the TechTypesController constructor
/// </summary>
public class ConstructorTests : TechTypesControllerTestsBase
{
    [Test]
    public void Constructor_Throws_ArgumentNullException_If_IApplicationsService_Is_Null()
    {
        // act
        Action act = () => _ = new TechTypesController(null!);

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
