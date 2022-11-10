using FluentAssertions;
using NUnit.Framework;
using System;

namespace Ofgem.API.BUS.Applications.Core.UnitTests.BusinessAccountsServiceTests;

/// <summary>
/// Tests for the BusinessAccountsService constructor
/// </summary>
public class ConstructorTests : BusinessAccountsServiceTestsBase
{
    [Test]
    public void Constructor_Throws_ArgumentNullException_If_IBusinessAccountsAPIClient_Is_Null()
    {
        // act
        Action act = () => _ = new BusinessAccountsService(null!);

        //assert
        act
          .Should().ThrowExactly<ArgumentNullException>()
          .WithParameterName("businessAccountsAPIClient")
          .WithMessage("Value cannot be null. (Parameter 'businessAccountsAPIClient')");
    }

    [Test]
    public void Constructor_Allows_Non_Null_IBusinessAccountsAPIClient()
    {
        // act
        var systemUnderTest = GenerateSystemUnderTest();

        // assert
        systemUnderTest.Should().NotBeNull();
    }

}
