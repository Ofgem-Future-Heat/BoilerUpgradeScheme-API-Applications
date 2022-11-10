using FluentAssertions;
using NUnit.Framework;
using Ofgem.API.BUS.PropertyConsents.Domain.Models.CommsObjects;
using Ofgem.Lib.BUS.APIClient.Domain.Exceptions;
using System;
using System.Threading.Tasks;

namespace Ofgem.API.BUS.Applications.Core.UnitTests.ApplicationsServiceTests;

/// <summary>
/// Tests for the ApplicationsService class' implementation of IApplicationsService.SendConsentEmailAsync
/// </summary>
public class SendConsentEmailAsyncTests : ApplicationsServiceTestsBase
{
    [Test]
    public async Task SendConsent_Emails_Successfully()
    {
        // arrange
        var sendConsentEmailRequest = Guid.NewGuid();
        var systemUnderTest = GenerateSystemUnderTest();

        Add_GetApplicationOrDefaultWithConsentObjectsByIdAsync_ToBeMocked(ref _mockApplicationsProvider);
        Add_GetApplicationsByInstallationAddressUprnAsync_ToBeMocked(ref _mockApplicationsProvider);
        Add_GetBusinessAccountNameByIdAsync_ToBeMocked(ref _mockBusinessAccountsService);

        var sendConsentEmailResult = new SendConsentEmailResult
        {
            ConsentRequestId = Guid.NewGuid(),
            ConsentTokenExpires = DateTime.Now.AddSeconds(5),
            IsSuccess = true
        };

        Add_SendConsentEmailAsync_ToBeMocked(ref _mockPropertyConsentService, sendConsentEmailResult);

        // act
        var success = await systemUnderTest.SendConsentEmailAsync(sendConsentEmailRequest, "Test");

        // assert
        success.IsSuccess.Should().Be(true);
    }

    [Test]
    public async Task SendConsent_Emails_Failed_To_Send()
    {
        // arrange
        var sendConsentEmailRequest = Guid.NewGuid();
        var systemUnderTest = GenerateSystemUnderTest();

        Add_GetApplicationOrDefaultWithConsentObjectsByIdAsync_ToBeMocked(ref _mockApplicationsProvider);
        Add_GetApplicationsByInstallationAddressUprnAsync_ToBeMocked(ref _mockApplicationsProvider);
        Add_GetBusinessAccountNameByIdAsync_ToBeMocked(ref _mockBusinessAccountsService);

        var sendConsentEmailResult = new SendConsentEmailResult
        {
            ConsentRequestId = Guid.NewGuid(),
            ConsentTokenExpires = DateTime.Now.AddSeconds(5),
            IsSuccess = false
        };

        Add_SendConsentEmailAsync_ToBeMocked(ref _mockPropertyConsentService, sendConsentEmailResult);

        // act
        Func<Task> act = () => _ = systemUnderTest.SendConsentEmailAsync(sendConsentEmailRequest, "Test");

        // assert
        await act
          .Should().ThrowExactlyAsync<BadRequestException>()
          .WithMessage("Cannot issue consent because there is a problem with the property owner's email");
    }
   
}
