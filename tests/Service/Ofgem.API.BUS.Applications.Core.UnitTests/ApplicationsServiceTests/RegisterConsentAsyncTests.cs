using FluentAssertions;
using Moq;
using NUnit.Framework;
using Ofgem.API.BUS.Applications.Domain;
using Ofgem.Lib.BUS.APIClient.Domain.Exceptions;
using System;
using System.Threading.Tasks;

namespace Ofgem.API.BUS.Applications.Core.UnitTests.ApplicationsServiceTests;

/// <summary>
/// Tests for the ApplicationsService class' implementation of IApplicationsService.RegisterConsentAsync
/// </summary>
public class RegisterConsentAsyncTests : ApplicationsServiceTestsBase
{
    [Test]
    public async Task RegisterConsentAsync_Calls_ApplicationsProvider_RegisterConsentReceivedByIdAsync_Once()
    {
        // arrange
        var consentRequestId = Guid.NewGuid();
        _mockApplicationsProvider.Setup(m => m.GetConsentRequestOrDefaultByIdAsync(consentRequestId)).ReturnsAsync(new ConsentRequest());
        _mockApplicationsProvider.Setup(m => m.RegisterConsentReceivedByIdAsync(consentRequestId, It.IsAny<string>())).Returns(Task.CompletedTask);
        _mockApplicationsProvider.Setup(m => m.RegisterConsentReceivedByIdAsync(consentRequestId, It.IsAny<string>())).Verifiable();

        var systemUnderTest = GenerateSystemUnderTest();

        // act
        await systemUnderTest.RegisterConsentAsync(consentRequestId, "test");

        // assert
        _mockApplicationsProvider.Verify(mock => mock.RegisterConsentReceivedByIdAsync(consentRequestId, It.IsAny<string>()), Times.Once);
    }

    [Test]
    public async Task RegisterConsentAsync_Throws_ResourceNotFoundException_When_Provider_GetConsentRequestOrDefaultByIdAsync_Returns_Null()
    {
        // arrange
        var consentRequestId = Guid.NewGuid();
        _mockApplicationsProvider = new(MockBehavior.Strict);
        _mockApplicationsProvider.Setup(m => m.GetConsentRequestOrDefaultByIdAsync(consentRequestId)).ReturnsAsync((ConsentRequest?)null);

        var systemUnderTest = GenerateSystemUnderTest();

        // act
        Func<Task> act = () => _ = systemUnderTest.RegisterConsentAsync(consentRequestId, "test");

        // assert
        await act
          .Should().ThrowExactlyAsync<ResourceNotFoundException>()
          .WithMessage($"Consent request {consentRequestId} not found");
    }
}
