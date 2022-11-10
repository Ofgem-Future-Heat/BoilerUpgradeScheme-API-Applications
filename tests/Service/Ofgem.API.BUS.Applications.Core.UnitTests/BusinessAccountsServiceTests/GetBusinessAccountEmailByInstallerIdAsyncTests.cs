using FluentAssertions;
using NUnit.Framework;
using Ofgem.Lib.BUS.APIClient.Domain.Exceptions;
using System;
using System.Threading.Tasks;

namespace Ofgem.API.BUS.Applications.Core.UnitTests.BusinessAccountsServiceTests;

/// <summary>
/// Tests for the IBusinessAccountsService.GetBusinessAccountEmailByInstallerIdAsync implementation in BusinessAccountsService
/// </summary>
public class GetBusinessAccountEmailByInstallerIdAsyncTests : BusinessAccountsServiceTestsBase
{
    [Test]
    public async Task GetBusinessAccountEmailByInstallerIdAsync_Should_Throw_ResourceNotFoundException_When_Client_Returns_Empty_String()
    {
        // arrange
        _mockBusinessAccountsAPIClient = new();
        Add_EmptyStringReturning_GetInstallerEmailByInstallerId_To_BusinessAccountsApiClientMock();

        var installerId = Guid.NewGuid();

        var systemUnderTest = GenerateSystemUnderTest();

        // act
        Func<Task> act = () => _ = systemUnderTest.GetBusinessAccountEmailByInstallerIdAsync(installerId);

        // assert
        await act
          .Should().ThrowExactlyAsync<ResourceNotFoundException>()
          .WithMessage($"No ExternalUserAccount with ID {installerId} was found.");
    }

    [Test]
    public async Task GetBusinessAccountEmailByInstallerIdAsync_Should_Return_Client_Result_When_Not_An_Empty_String()
    {
        // arrange
        _mockBusinessAccountsAPIClient = new();
        Add_GetInstallerEmailByInstallerId_To_BusinessAccountsApiClientMock();

        var systemUnderTest = GenerateSystemUnderTest();

        // act
        var result = await systemUnderTest.GetBusinessAccountEmailByInstallerIdAsync(Guid.NewGuid());

        // assert
        result.Should().Be(_defaultInstallerEmail);
    }
}
