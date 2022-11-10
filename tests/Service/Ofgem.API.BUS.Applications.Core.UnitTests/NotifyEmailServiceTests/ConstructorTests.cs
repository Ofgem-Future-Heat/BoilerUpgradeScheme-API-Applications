using FluentAssertions;
using NUnit.Framework;
using System;

namespace Ofgem.API.BUS.Applications.Core.UnitTests.NotifyEmailServiceTests;

[TestFixture]
public class ConstructorTests : NotifyEmailServiceTestsBase
{
    [Test]
    public void Constructor_Throws_ArgumentNullException_If_GovNotifyClient_Is_Null()
    {
        // Act
        var action = () => new NotifyEmailService(null!,
                                                  _govNotifyConfiguration,
                                                  _mockBusinessAccountsService.Object,
                                                  _mockApplicationsService.Object,
                                                  _applicationsApiConfiguration);

        // Assert
        action.Should().Throw<ArgumentNullException>().WithParameterName("govNotifyClient");
    }

    [Test]
    public void Constructor_Throws_ArgumentNullException_If_GovNotifyConfiguration_Is_Null()
    {
        // Act
        var action = () => new NotifyEmailService(_mockNotifyClient.Object,
                                                  null!,
                                                  _mockBusinessAccountsService.Object,
                                                  _mockApplicationsService.Object,
                                                  _applicationsApiConfiguration);

        // Assert
        action.Should().Throw<ArgumentNullException>().WithParameterName("configuration");
    }

    [Test]
    public void Constructor_Throws_ArgumentNullException_If_BusinessAccountService_Is_Null()
    {
        // Act
        var action = () => new NotifyEmailService(_mockNotifyClient.Object,
                                                  _govNotifyConfiguration,
                                                  null!,
                                                  _mockApplicationsService.Object,
                                                  _applicationsApiConfiguration);

        // Assert
        action.Should().Throw<ArgumentNullException>().WithParameterName("businessAccountsService");
    }

    [Test]
    public void Constructor_Throws_ArgumentNullException_If_ApplicationsService_Is_Null()
    {
        // Act
        var action = () => new NotifyEmailService(_mockNotifyClient.Object,
                                                  _govNotifyConfiguration,
                                                  _mockBusinessAccountsService.Object,
                                                  null!,
                                                  _applicationsApiConfiguration);

        // Assert
        action.Should().Throw<ArgumentNullException>().WithParameterName("applicationsService");
    }

    [Test]
    public void Constructor_Throws_ArgumentNullException_If_ApplicationsApiConfiguration_Is_Null()
    {
        // Act
        var action = () => new NotifyEmailService(_mockNotifyClient.Object,
                                                  _govNotifyConfiguration,
                                                  _mockBusinessAccountsService.Object,
                                                  _mockApplicationsService.Object,
                                                  null!);

        // Assert
        action.Should().Throw<ArgumentNullException>().WithParameterName("applicationsApiConfiguration");
    }

    [Test]
    public void Constructor_Should_Instantiate_With_Valid_Parameters()
    {
        // Act
        var systemUnderTest = GenerateSystemUnderTest();

        // Assert
        systemUnderTest.Should().NotBeNull();
    }
}
