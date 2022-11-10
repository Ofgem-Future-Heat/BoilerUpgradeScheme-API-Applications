using FluentAssertions;
using FluentAssertions.Execution;
using Moq;
using Notify.Exceptions;
using NUnit.Framework;
using Ofgem.Lib.BUS.APIClient.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ofgem.API.BUS.Applications.Core.UnitTests.NotifyEmailServiceTests;

[TestFixture]
public class SendInstallerPostApplicationEmailAsyncTests : NotifyEmailServiceTestsBase
{
    [Test]
    public async Task SendInstallerPostApplicationEmailAsync_Returns_Success_When_Email_Sent()
    {
        // Arrange
        Add_GetBusinessAccountEmailByInstallerIdAsync_To_BusinessAccountsService(ref _mockBusinessAccountsService);
        Add_GetBusinessAccountNameByIdAsync_To_BusinessAccountsService(ref _mockBusinessAccountsService);
        Add_SendEmailAsync_To_AsyncNotificationClient(ref _mockNotifyClient);

        var systemUnderTest = GenerateSystemUnderTest();

        // Act
        var sendEmailResult = await systemUnderTest.SendInstallerPostApplicationEmailAsync(TestApplication);

        // Assert
        using (new AssertionScope())
        {
            sendEmailResult.Should().NotBeNull();
            sendEmailResult.IsSuccess.Should().BeTrue();
            sendEmailResult.ErrorMessage.Should().BeNullOrEmpty();
        }
    }

    [Test]
    public async Task SendInstallerPostApplicationEmailAsync_Handles_Notify_Api_Error()
    {
        // Arrange
        Add_GetBusinessAccountEmailByInstallerIdAsync_To_BusinessAccountsService(ref _mockBusinessAccountsService);
        Add_GetBusinessAccountNameByIdAsync_To_BusinessAccountsService(ref _mockBusinessAccountsService);

        var exception = new NotifyClientException("Something went wrong")
        {
            HResult = 1234
        };

        _mockNotifyClient.Setup(m => m.SendEmailAsync(It.IsAny<string>(),
                                                      It.IsAny<string>(),
                                                      It.IsAny<Dictionary<string, object>>(),
                                                      It.IsAny<string>(),
                                                      It.IsAny<string>())).ThrowsAsync(exception);

        var systemUnderTest = GenerateSystemUnderTest();

        // Act
        var sendEmailResult = await systemUnderTest.SendInstallerPostApplicationEmailAsync(TestApplication);

        // Assert
        using (new AssertionScope())
        {
            sendEmailResult.Should().NotBeNull();
            sendEmailResult.IsSuccess.Should().BeFalse();
            sendEmailResult.ErrorMessage.Should().NotBeNullOrEmpty().And.Contain(exception.HResult.ToString()).And.Contain(exception.Message);
        }
    }

    [Test]
    public async Task SendInstallerPostApplicationEmailAsync_Handles_Other_Exceptions()
    {
        // Arrange
        var exception = new BadRequestException("something went wrong");
        _mockBusinessAccountsService.Setup(m => m.GetBusinessAccountEmailByInstallerIdAsync(It.IsAny<Guid>())).ThrowsAsync(exception);
        Add_GetBusinessAccountNameByIdAsync_To_BusinessAccountsService(ref _mockBusinessAccountsService);
        Add_SendEmailAsync_To_AsyncNotificationClient(ref _mockNotifyClient);

        var systemUnderTest = GenerateSystemUnderTest();

        // Act
        var sendEmailResult = await systemUnderTest.SendInstallerPostApplicationEmailAsync(TestApplication);

        // Assert
        using (new AssertionScope())
        {
            sendEmailResult.Should().NotBeNull();
            sendEmailResult.IsSuccess.Should().BeFalse();
            sendEmailResult.ErrorMessage.Should().NotBeNullOrEmpty().And.Contain(exception.Message);
        }
    }
}
