using FluentAssertions;
using Moq;
using NUnit.Framework;
using Ofgem.API.BUS.Applications.Domain;
using Ofgem.Lib.BUS.APIClient.Domain.Exceptions;
using System;
using System.Threading.Tasks;

namespace Ofgem.API.BUS.Applications.Core.UnitTests.ApplicationsServiceTests;

/// <summary>
/// Tests for the ApplicationsService class' implementation of IApplicationsService.GetConsentRequestDetailsAsync
/// </summary>
public class GetConsentRequestDetailsAsyncTests : ApplicationsServiceTestsBase
{
    [Test]
    public async Task GetConsentRequestDetailsAsync_Should_Return_Failure_Result_When_ConsentRequestId_Is_Empty_Guid()
    {
        // arrange
        var systemUnderTest = GenerateSystemUnderTest();

        // act
        var result = await systemUnderTest.GetConsentRequestDetailsAsync(Guid.Empty);

        // assert
        result.Should().NotBeNull();
        result.IsSuccess.Should().BeFalse();
    }

    [Test]
    public async Task GetConsentRequestDetailsAsync_Should_Return_Failure_Result_When_ConsentRequestId_Not_Found()
    {
        // arrange
        var systemUnderTest = GenerateSystemUnderTest();

        // act
        var result = await systemUnderTest.GetConsentRequestDetailsAsync(Guid.Empty);

        // assert
        result.Should().NotBeNull();
        result.IsSuccess.Should().BeFalse();
    }

    [Test]
    public async Task GetConsentRequestDetailsAsync_Should_Return_Success_And_Populated_Object_When_ConsentRequestId_Is_Valid()
    {
        // arrange
        _mockApplicationsProvider = new Mock<Providers.DataAccess.Interfaces.IApplicationsProvider>(MockBehavior.Strict);
        Add_GetConsentRequestOrDefaultByIdAsync_ToMockedApplicationsProvider(ref _mockApplicationsProvider);
        Add_GetApplicationWithConsentObjectsByIdAsync_ToMockedApplicationsProvider(ref _mockApplicationsProvider);

        _mockBusinessAccountsService = new Mock<Interfaces.IBusinessAccountsService>(MockBehavior.Strict);
        Add_GetBusinessAccountNameByIdAsync_ToMockedBusinessAccountsService(ref _mockBusinessAccountsService);
        Add_GetBusinessAccountEmailByInstallerIdAsync_ToMockedBusinessAccountsService(ref _mockBusinessAccountsService);

        var systemUnderTest = GenerateSystemUnderTest();

        // act
        var details = await systemUnderTest.GetConsentRequestDetailsAsync(consentRequestId);

        // assert
        details.IsSuccess.Should().BeTrue();
        details.ConsentRequestSummary.Should().NotBeNull();

        if (details.ConsentRequestSummary is not null)
        {
            details.ConsentRequestSummary.QuoteAmount.Should().Be(quoteAmount);
            details.ConsentRequestSummary.OwnerFullName.Should().Be(propertyOwnerFullName);
            details.ConsentRequestSummary.ApplicationReferenceNumber.Should().Be(applicationReferenceNumber);
            details.ConsentRequestSummary.InstallerName.Should().Be(installerBusinessName);
            details.ConsentRequestSummary.InstallerEmailId.Should().Be(installerEmailAddress);
            details.ConsentRequestSummary.OwnerEmailId.Should().Be(propertyOwnerEmailAddress);
            details.ConsentRequestSummary.InstallationAddressLine1.Should().Be(installationPropertyAddressL1);
            details.ConsentRequestSummary.InstallationAddressLine2.Should().Be(installationPropertyAddressL2);
            details.ConsentRequestSummary.InstallationAddressLine3.Should().Be(installationPropertyAddressL3);
            details.ConsentRequestSummary.InstallationAddressCounty.Should().Be(installationPropertyAddressCounty);
            details.ConsentRequestSummary.InstallationAddressPostcode.Should().Be(installationPropertyAddressPostcode);
            details.ConsentRequestSummary.HasConsented.Should().BeNull();
            details.ConsentRequestSummary.ExpiryDate.Should().Be(consentExpiryDate);
            details.ConsentRequestSummary.ServiceLevelAgreementDate.Should().Be(consentExpiryDate);
            details.ConsentRequestSummary.TechnologyType.Should().Be(techTypeDescription);
        }
    }


    [Test]
    [Ignore("This is how I think it should work")] 
    public async Task GetConsentRequestDetailsAsync_Should_Throw_BadRequestException_When_ConsentRequestId_Is_Empty_Guid()
    {
        // arrange
        // may require additional setup when implemented
        var systemUnderTest = GenerateSystemUnderTest();

        // act
        Func<Task> act = () => _ = systemUnderTest.GetConsentRequestDetailsAsync(Guid.Empty);

        // assert
        await act
          .Should().ThrowExactlyAsync<BadRequestException>()
          .WithMessage($"ConsentRequestId cannot be an empty GUID");
    }

    [Test]
    [Ignore("This is how I think it should work")]
    public async Task GetConsentRequestDetailsAsync_Should_Throw_ResourceNotFoundException_When_ConsentRequest_Is_Not_Found()
    {
        // arrange
        var badConsentRequestId = Guid.NewGuid();

        // may require additional setup when implemented
        var systemUnderTest = GenerateSystemUnderTest();

        // act
        Func<Task> act = () => _ = systemUnderTest.GetConsentRequestDetailsAsync(badConsentRequestId);

        // assert
        await act
          .Should().ThrowExactlyAsync<BadRequestException>()
          .WithMessage($"ConsentRequest with ID {badConsentRequestId} not found");
    }

    [Test]
    [Ignore("This is how I think it should work")]
    public async Task GetConsentRequestDetailsAsync_Should_Return_Populated_Object_When_ConsentRequestId_Is_Valid()
    {
        // arrange
        _mockApplicationsProvider = new Mock<Providers.DataAccess.Interfaces.IApplicationsProvider>(MockBehavior.Strict);
        Add_GetConsentRequestOrDefaultByIdAsync_ToMockedApplicationsProvider(ref _mockApplicationsProvider);
        Add_GetApplicationWithConsentObjectsByIdAsync_ToMockedApplicationsProvider(ref _mockApplicationsProvider);

        _mockBusinessAccountsService = new Mock<Interfaces.IBusinessAccountsService>(MockBehavior.Strict);
        Add_GetBusinessAccountNameByIdAsync_ToMockedBusinessAccountsService(ref _mockBusinessAccountsService);
        Add_GetBusinessAccountEmailByInstallerIdAsync_ToMockedBusinessAccountsService(ref _mockBusinessAccountsService);

        var systemUnderTest = GenerateSystemUnderTest();

        // act
        var result = await systemUnderTest.GetConsentRequestDetailsAsync(consentRequestId);

        // assert
        result.Should().NotBeNull();

        // result should be what is currently the sub-object in what is returned,
        // //so these asserts need to be commented out comlpetely to compile
        if (result is not null)
        {
            //result.QuoteAmount.Should().Be(quoteAmount);
            //result.OwnerFullName.Should().Be(propertyOwnerFullName);
            //result.ApplicationReferenceNumber.Should().Be(applicationReferenceNumber);
            //result.InstallerName.Should().Be(installerBusinessName);
            //result.InstallerEmailId.Should().Be(installerEmailAddress);
            //result.OwnerEmailId.Should().Be(propertyOwnerEmailAddress);
            //result.InstallationAddressLine1.Should().Be(installationPropertyAddressL1);
            //result.InstallationAddressLine2.Should().Be(installationPropertyAddressL2);
            //result.InstallationAddressLine3.Should().Be(installationPropertyAddressL3);
            //result.InstallationAddressCounty.Should().Be(installationPropertyAddressCounty);
            //result.InstallationAddressPostcode.Should().Be(installationPropertyAddressPostcode);
            //result.HasConsented.Should().BeNull();
            //result.ExpiryDate.Should().Be(consentExpiryDate);
            //result.ServiceLevelAgreementDate.Should().Be(consentExpiryDate);
            //result.TechnologyType.Should().Be(techTypeDescription);
        }
    }
}