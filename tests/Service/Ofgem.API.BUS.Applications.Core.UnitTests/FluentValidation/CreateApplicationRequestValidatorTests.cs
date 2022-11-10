using System;
using NUnit.Framework;
using Ofgem.API.BUS.Applications.Core.FluentValidation;
using Ofgem.API.BUS.Applications.Domain.Entities.CommsObjects;
using FluentValidation.TestHelper;

namespace Ofgem.API.BUS.Applications.Core.UnitTests.FluentValidation;

[TestFixture]
public class CreateApplicationRequestValidatorTests
{
    private CreateApplicationRequestValidator validator = new();

    [SetUp]
    public void Setup()
    {
        validator = new CreateApplicationRequestValidator();
    }

    [Test]
    public void Should_Not_Have_Any_Errors()
    {
        var model = new CreateApplicationRequest
        {
            BusinessAccountID = new Guid("B0FD63E0-FAC7-4793-BAC1-CAD1AABB7B36"),
            InstallationAddress = new CreateApplicationRequestInstallationAddress
            {
                Line1 = "Line1",
                Postcode = "Postcode",
                UPRN = "123456789"
            },
            InstallationAddressManualEntry = false
        };

        var result = validator.TestValidate(model);
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Test]
    public void Should_Not_Have_Any_Errors_ManualAddress()
    {
        var model = new CreateApplicationRequest
        {
            BusinessAccountID = new Guid("B0FD63E0-FAC7-4793-BAC1-CAD1AABB7B36"),
            InstallationAddress = new CreateApplicationRequestInstallationAddress
            {
                Line1 = "Line1",
                Postcode = "Postcode",
            },
            InstallationAddressManualEntry = true
        };

        var result = validator.TestValidate(model);
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Test]
    [TestCase(" ")]
    [TestCase(null)]
    [TestCase("ThisIsMoreThanOneHundredAndTwentySevenCharactersLongThisIsMoreThanOneHundredAndTwentySevenCharactersLongThisIsMoreThanOneHundredAndTwentySeven")]
    public void Should_Have_Error_For_Address_Line1(string line1)
    {
        var model = new CreateApplicationRequest
        {
            BusinessAccountID = new Guid("B0FD63E0-FAC7-4793-BAC1-CAD1AABB7B36"),
            InstallationAddress = new CreateApplicationRequestInstallationAddress
            {
                Postcode = "Postcode",
                UPRN = "123456789",
                Line1 = line1
            }
        };

        var result = validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.InstallationAddress.Line1);
    }

    [Test]
    public void Should_Have_Error_For_Address_Line2()
    {
        var model = new CreateApplicationRequest
        {
            BusinessAccountID = new Guid("B0FD63E0-FAC7-4793-BAC1-CAD1AABB7B36"),
            InstallationAddress = new CreateApplicationRequestInstallationAddress
            {
                Line1 = "Line1",
                Line2 = "ThisIsMoreThanOneHundredAndTwentySevenCharactersLongThisIsMoreThanOneHundredAndTwentySevenCharactersLongThisIsMoreThanOneHundredAndTwentySeven",
                Postcode = "Postcode",
                UPRN = "123456789"
            }
        };

        var result = validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.InstallationAddress.Line2);
    }

    [Test]
    public void Should_Have_Error_For_Address_Line3()
    {
        var model = new CreateApplicationRequest
        {
            BusinessAccountID = new Guid("B0FD63E0-FAC7-4793-BAC1-CAD1AABB7B36"),
            InstallationAddress = new CreateApplicationRequestInstallationAddress
            {
                Line1 = "Line1",
                Line2 = "Line2",
                Line3 = "ThisIsMoreThanOneHundredAndTwentySevenCharactersLongThisIsMoreThanOneHundredAndTwentySevenCharactersLongThisIsMoreThanOneHundredAndTwentySeven",
                Postcode = "Postcode",
                UPRN = "123456789"
            }
        };

        var result = validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.InstallationAddress.Line3);
    }

    [Test]
    public void Should_Have_Error_For_Address_County()
    {
        var model = new CreateApplicationRequest
        {
            BusinessAccountID = new Guid("B0FD63E0-FAC7-4793-BAC1-CAD1AABB7B36"),
            InstallationAddress = new CreateApplicationRequestInstallationAddress
            {
                Line1 = "Line1",
                Line2 = "Line2",
                Line3 = "Line3",
                County = "CountyIsTooLongForThisTestToPass",
                Postcode = "Postcode",
                UPRN = "123456789"
            }
        };

        var result = validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.InstallationAddress.County);
    }


    [Test]
    [TestCase(" ")]
    [TestCase(null)]
    public void Should_Have_Error_For_Address_Postcode(string postcode)
    {
        var model = new CreateApplicationRequest
        {
            BusinessAccountID = new Guid("B0FD63E0-FAC7-4793-BAC1-CAD1AABB7B36"),
            InstallationAddress = new CreateApplicationRequestInstallationAddress
            {
                Postcode = postcode,
                UPRN = "123456789"
            }
        };

        var result = validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.InstallationAddress.Postcode);
    }

    [Test]
    [TestCase("1234567890123")]
    [TestCase("UPRN")]
    [TestCase("123 456")]
    [TestCase("123XX456")]
    public void Should_Have_Error_For_UPRN(string? uprn)
    {
        var model = new CreateApplicationRequest
        {
            BusinessAccountID = new Guid("B0FD63E0-FAC7-4793-BAC1-CAD1AABB7B36"),
            InstallationAddress = new CreateApplicationRequestInstallationAddress
            {
                Line1 = "Line1",
                Line2 = "Line2",
                Line3 = "Line3",
                County = "County",
                Postcode = "Postcode",
                UPRN = uprn
            },
            InstallationAddressManualEntry = false
        };

        var result = validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.InstallationAddress.UPRN);
    }
}
