using FluentAssertions;
using Moq;
using NUnit.Framework;
using Ofgem.API.BUS.PropertyConsents.Domain.Models.CommsObjects;
using System;
using System.Threading.Tasks;

namespace Ofgem.API.BUS.Applications.Core.UnitTests.PropertyConsentServiceTests;

/// <summary>
/// Tests for the IPropertyConsentService.SendConsentEmailAsync implementation in PropertyConsentService
/// </summary>
public class SendConsentEmailAsyncTests : PropertyConsentServiceTestsBase
{
    [Test]
    public async Task SendConsentEmailAsync_Passes_Parameter_To_APIClient()
    {
        // arrange
        SendConsentEmailRequest sendConsentEmailRequest = new() 
        { 
            ConsentRequestId = Guid.NewGuid()
        };

        _mockPropertyConsentAPIClient = new Moq.Mock<PropertyConsents.Client.Interfaces.IPropertyConsentAPIClient>(Moq.MockBehavior.Strict);
        _mockPropertyConsentAPIClient
            .Setup(m => m.PropertyConsentRequestsClient.SendConsentEmailAsync(sendConsentEmailRequest))
            .ReturnsAsync(new SendConsentEmailResult())
            .Verifiable();

        var systemUnderTest = GenerateSystemUnderTest();

        // act
        var result = await systemUnderTest.SendConsentEmailAsync(sendConsentEmailRequest);

        // assert
        _mockPropertyConsentAPIClient.Verify(mock => mock.PropertyConsentRequestsClient.SendConsentEmailAsync(sendConsentEmailRequest), Times.Once());
    }

    [Test]
    public async Task SendConsentEmailAsync_Returns_Response_Of_APIClient()
    {
        // arrange
        SendConsentEmailRequest sendConsentEmailRequest = new()
        {
            ConsentRequestId = Guid.NewGuid()
        };

        SendConsentEmailResult sendConsentEmailResult = new()
        {
            IsSuccess = true,
            ConsentRequestId = Guid.NewGuid(),
            ConsentTokenExpires = DateTime.UtcNow
        };

        _mockPropertyConsentAPIClient = new Moq.Mock<PropertyConsents.Client.Interfaces.IPropertyConsentAPIClient>(Moq.MockBehavior.Strict);
        _mockPropertyConsentAPIClient
            .Setup(m => m.PropertyConsentRequestsClient.SendConsentEmailAsync(It.IsAny<SendConsentEmailRequest>()))
            .ReturnsAsync(sendConsentEmailResult)
            .Verifiable();

        var systemUnderTest = GenerateSystemUnderTest();

        // act
        var result = await systemUnderTest.SendConsentEmailAsync(sendConsentEmailRequest);

        // assert
        result.Should().Be(sendConsentEmailResult);

    }
}
