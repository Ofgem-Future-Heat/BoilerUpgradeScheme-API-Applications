using FluentAssertions;
using NUnit.Framework;
using System;

namespace Ofgem.API.BUS.Applications.Core.UnitTests.ApplicationsServiceTests;

/// <summary>
/// Tests for the ApplicationsService class constructor
/// </summary>
public class ConstructorTests : ApplicationsServiceTestsBase
{
    [Test]
    public void Constructor_Throws_ArgumentNullException_If_IMapper_Is_Null()
    {
        // act assert
        Assert.That(
            () =>
            {
                _ = new ApplicationsService(
                    null!,
                    _mockApplicationsProvider.Object,
                    _mockBusinessAccountsService.Object,
                    _mockPropertyConsentService.Object,
                    applicationsApiConfiguration);
            },
            Throws.InstanceOf<ArgumentNullException>()
            .With.Property("ParamName").EqualTo("mapper"));
    }

    [Test]
    public void Constructor_Throws_ArgumentNullException_If_IApplicationsProvider_Is_Null()
    {
        // act assert
        Assert.That(
            () =>
            {
                _ = new ApplicationsService(
                    _mockMapper.Object,
                    null!,
                    _mockBusinessAccountsService.Object,
                    _mockPropertyConsentService.Object,
                    applicationsApiConfiguration);
            },
            Throws.InstanceOf<ArgumentNullException>()
            .With.Property("ParamName").EqualTo("applicationsProvider"));
    }

    [Test]
    public void Constructor_Throws_ArgumentNullException_If_ApplicationsApiConfiguration_Is_Null()
    {
        // act assert
        Assert.That(
            () =>
            {
                _ = new ApplicationsService(
                    _mockMapper.Object,
                    _mockApplicationsProvider.Object,
                    _mockBusinessAccountsService.Object,
                    _mockPropertyConsentService.Object,
                    null!);
            },
            Throws.InstanceOf<ArgumentNullException>()
            .With.Property("ParamName").EqualTo("applicationsApiConfiguration"));
    }

    [Test]
    public void Constructor_Throws_ArgumentNullException_If_IBusinessAccountsService_Is_Null()
    {
        // act assert
        Assert.That(
            () =>
            {
                _ = new ApplicationsService(
                    _mockMapper.Object,
                    _mockApplicationsProvider.Object,
                    null!,
                    _mockPropertyConsentService.Object,
                    applicationsApiConfiguration);
            },
            Throws.InstanceOf<ArgumentNullException>()
            .With.Property("ParamName").EqualTo("businessAccountsService"));
    }

    [Test]
    public void Constructor_Throws_ArgumentNullException_If_IPropertyConsentService_Is_Null()
    {
        // act assert
        Assert.That(
            () =>
            {
                _ = new ApplicationsService(
                    _mockMapper.Object,
                    _mockApplicationsProvider.Object,
                    _mockBusinessAccountsService.Object,
                    null!,
                    applicationsApiConfiguration);
            },
            Throws.InstanceOf<ArgumentNullException>()
            .With.Property("ParamName").EqualTo("propertyConsentService"));
    }

    [Test]
    public void Constructor_Accepts_Non_Null_Parameters()
    {
        // act
        var systemUnderTest = GenerateSystemUnderTest();

        // assert
        systemUnderTest.Should().NotBeNull();
    }
}