using FluentAssertions;
using Moq;
using NUnit.Framework;
using Ofgem.API.BUS.Applications.Core.Interfaces;
using Ofgem.API.BUS.BusinessAccounts.Domain.Exceptions;
using System;
using System.Threading.Tasks;

namespace Ofgem.API.BUS.Applications.Core.UnitTests.ApplicationsServiceTests;

/// <summary>
/// Tests for the ApplicationsService class' implementation of IApplicationsService.GetBusinessEmailAddressByInstallerId
/// </summary>
public class GetBusinessEmailAddressByInstallerIdTests : ApplicationsServiceTestsBase
{
    [Test]
    [Ignore("The is not the current behaviour")]
    public async Task GetBusinessEmailAddressByInstallerId_Should_Throw_ArgumentException_When_Uprn_Is_Empty_Guid()
    {
        // arrange
        var systemUnderTest = GenerateSystemUnderTest();

        // act
        Func<Task> act = () => _ = systemUnderTest.GetBusinessEmailAddressByInstallerId(Guid.Empty);

        // assert
        await act
          .Should().ThrowExactlyAsync<ArgumentException>()
          .WithParameterName("installerId")
          .WithMessage("Value cannot be an empty GUID. (Parameter 'installerId')");
    }

    [Test]
    [Ignore("The is not the current behaviour")]
    public async Task GetBusinessEmailAddressByInstallerId_Should_Throw_ResourceNotFoundException_When_BusinessService_Cannot_Resolve_InstallerId()
    {
        // arrange
        _mockBusinessAccountsService = new Mock<IBusinessAccountsService>(MockBehavior.Strict);
        Add_NullReturning_GetBusinessEmailAddressByInstallerId_ToMockedBusinessAccountsService(ref _mockBusinessAccountsService);

        var systemUnderTest = GenerateSystemUnderTest();

        // act
        Func<Task> act = () => _ = systemUnderTest.GetBusinessEmailAddressByInstallerId(installerId);

        // assert
        await act
          .Should().ThrowExactlyAsync<Ofgem.Lib.BUS.APIClient.Domain.Exceptions.ResourceNotFoundException>()
          .WithMessage($"ExternalUserAccount with ID {installerId} not found");
    }

    /// <summary>
    /// TODO: Correct the service behaviour and replace this test ASAP after code freeze is lifted
    /// Issue is: error message mentions 'consent request' but this is a lookup of ExternalUserAccount
    /// </summary>
    [Test]
    public async Task GetBusinessEmailAddressByInstallerId_Should_Throw_ResourceNotFoundException_When_BusinessService_Cannot_Resolve_InstallerId_Incorrect_Message_Text_Version()
    {
        // arrange
        _mockBusinessAccountsService = new Mock<IBusinessAccountsService>(MockBehavior.Strict);
        Add_NullReturning_GetBusinessEmailAddressByInstallerId_ToMockedBusinessAccountsService(ref _mockBusinessAccountsService);

        var systemUnderTest = GenerateSystemUnderTest();

        // act
        Func<Task> act = () => _ = systemUnderTest.GetBusinessEmailAddressByInstallerId(installerId);

        // assert
        await act
          .Should().ThrowExactlyAsync<Ofgem.Lib.BUS.APIClient.Domain.Exceptions.ResourceNotFoundException>()
          .WithMessage($"Consent request {installerId} not found");
    }

}
