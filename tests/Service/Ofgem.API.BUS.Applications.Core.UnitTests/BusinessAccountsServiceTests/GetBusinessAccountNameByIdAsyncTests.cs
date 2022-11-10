using FluentAssertions;
using NUnit.Framework;
using Ofgem.Lib.BUS.APIClient.Domain.Exceptions;
using System;
using System.Threading.Tasks;

namespace Ofgem.API.BUS.Applications.Core.UnitTests.BusinessAccountsServiceTests;

/// <summary>
/// Tests for the IBusinessAccountsService.GetBusinessAccountNameByIdAsync implementation in BusinessAccountsService
/// </summary>
public class GetBusinessAccountNameByIdAsyncTests : BusinessAccountsServiceTestsBase
{
    [Test]
    public async Task GetBusinessAccountNameByIdAsync_Should_Throw_ResourceNotFoundException_When_Client_Returns_Empty_String()
    {
        // arrange
        _mockBusinessAccountsAPIClient = new();
        Add_NullReturning_GetBusinessAccountAsync_To_BusinessAccountsApiClientMock();

        var businessAccountId = Guid.NewGuid();
        var systemUnderTest = GenerateSystemUnderTest();

        // act
        Func<Task> act = () => _ = systemUnderTest.GetBusinessAccountNameByIdAsync(businessAccountId);

        // assert
        await act
          .Should().ThrowExactlyAsync<ResourceNotFoundException>()
          .WithMessage($"No Business Account with ID { businessAccountId} was found");
    }


    [Test]
    public async Task GetBusinessAccountNameByIdAsync_Should_Return_Client_Results_BusinessName_When_Client_Returns_A_BusinessAccount()
    {
        // arrange
        _mockBusinessAccountsAPIClient = new();
        Add_GetBusinessAccountAsync_To_BusinessAccountsApiClientMock();

        var systemUnderTest = GenerateSystemUnderTest();

        // act
        var result = await systemUnderTest.GetBusinessAccountNameByIdAsync(Guid.NewGuid());

        // assert
        result.Should().Be(_defaultBusinessAccountBusinessName);
    }

}
