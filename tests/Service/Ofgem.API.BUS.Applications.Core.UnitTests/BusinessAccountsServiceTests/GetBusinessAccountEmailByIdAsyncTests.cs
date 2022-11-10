using FluentAssertions;
using NUnit.Framework;
using Ofgem.Lib.BUS.APIClient.Domain.Exceptions;
using System;
using System.Threading.Tasks;

namespace Ofgem.API.BUS.Applications.Core.UnitTests.BusinessAccountsServiceTests;

/// <summary>
/// Tests for the IBusinessAccountsService.GetBusinessAccountEmailByIdAsync implementation in BusinessAccountsService
/// </summary>
public class GetBusinessAccountEmailByIdAsyncTests : BusinessAccountsServiceTestsBase
{
    [Test]
    public async Task GetBusinessAccountEmailByIdAsync_Should_Throw_ResourceNotFoundException_When_Client_Returns_Empty_String()
    {
        // arrange
        _mockBusinessAccountsAPIClient = new();
        Add_EmptyStringReturning_GetBusinessAccountEmailById_To_BusinessAccountsApiClientMock();

        var businessAccountId = Guid.NewGuid();
        var systemUnderTest = GenerateSystemUnderTest();

        // act
        Func<Task> act = () => _ = systemUnderTest.GetBusinessAccountEmailByIdAsync(businessAccountId);

        // assert
        await act
          .Should().ThrowExactlyAsync<ResourceNotFoundException>()
          .WithMessage($"No Business Account with ID { businessAccountId} was found");
    }

    [Test]
    public async Task GetBusinessAccountEmailByIdAsync_Should_Return_Client_Result_When_Not_An_Empty_String()
    {
        // arrange
        _mockBusinessAccountsAPIClient = new();
        Add_GetInstallerEmailById_To_BusinessAccountsApiClientMock();

        var systemUnderTest = GenerateSystemUnderTest();

        // act
        var result = await systemUnderTest.GetBusinessAccountEmailByIdAsync(Guid.NewGuid());

        // assert
        result.Should().Be(_defaultBusinessAccountEmail);
    }
}
