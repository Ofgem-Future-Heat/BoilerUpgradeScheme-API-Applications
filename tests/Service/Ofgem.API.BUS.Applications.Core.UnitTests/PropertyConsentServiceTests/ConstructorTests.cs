using FluentAssertions;
using NUnit.Framework;
using System;

namespace Ofgem.API.BUS.Applications.Core.UnitTests.PropertyConsentServiceTests;

/// <summary>
/// Tests for the PropertyConsentService constructor
/// </summary>
public class ConstructorTests : PropertyConsentServiceTestsBase
{
    [Test]
    public void Constructor_Throws_ArgumentNullException_If_IPropertyConsentAPIClient_Is_Null()
    {
        // act
        Action act = () => _ = new PropertyConsentService(null!);

        //assert
        act
          .Should().ThrowExactly<ArgumentNullException>()
          .WithParameterName("propertyConsentAPIClient")
          .WithMessage("Value cannot be null. (Parameter 'propertyConsentAPIClient')");
    }

    [Test]
    public void Constructor_Allows_Non_Null_IPropertyConsentAPIClient()
    {
        // act
        var systemUnderTest = GenerateSystemUnderTest();

        // assert
        systemUnderTest.Should().NotBeNull();
    }

}
