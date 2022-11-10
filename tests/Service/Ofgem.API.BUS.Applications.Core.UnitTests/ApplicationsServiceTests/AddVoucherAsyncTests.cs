using FluentAssertions;
using Moq;
using NUnit.Framework;
using Ofgem.API.BUS.Applications.Domain;
using Ofgem.API.BUS.Applications.Domain.Entities.CommsObjects;
using Ofgem.API.BUS.Applications.Providers.DataAccess.Interfaces;
using Ofgem.Lib.BUS.APIClient.Domain.Exceptions;
using System;
using System.Threading.Tasks;

namespace Ofgem.API.BUS.Applications.Core.UnitTests.ApplicationsServiceTests;

/// <summary>
/// Tests for the ApplicationsService class' implementation of IApplicationsService.AddVoucherAsync
/// </summary>
public class AddVoucherAsyncTests : ApplicationsServiceTestsBase
{
    [Test]
    public async Task AddVoucherAsync_Should_Throw_ArgumentNullException_When_Request_Is_Null()
    {
        // arrange
        var systemUnderTest = GenerateSystemUnderTest();

        // act
        Func<Task> act = () => _ = systemUnderTest.AddVoucherAsync(null!);

        // assert
        await act
          .Should().ThrowExactlyAsync<ArgumentNullException>()
          .WithParameterName("addVoucherRequest")
          .WithMessage("Value cannot be null. (Parameter 'addVoucherRequest')");
    }

    [Test]
    public async Task AddVoucherAsync_Should_Throw_ResourceNotFoundException_When_ApplicationID_Cannot_Be_Resolved()
    {
        // arrange
        _mockApplicationsProvider = new Mock<IApplicationsProvider>(MockBehavior.Strict);
        Add_NullReturning_GetApplicationWithConsentObjectsByIdAsync_ToMockedApplicationsProvider(ref _mockApplicationsProvider);

        var systemUnderTest = GenerateSystemUnderTest();

        AddVoucherRequest addVoucherRequest = new()
        {
            ApplicationID = Guid.NewGuid(),
            TechTypeId = Guid.NewGuid()
        };

        // act
        Func<Task> act = () => _ = systemUnderTest.AddVoucherAsync(addVoucherRequest);

        // assert
        await act
          .Should().ThrowExactlyAsync<ResourceNotFoundException>()
          .WithMessage($"Unable to resolve Application ID {addVoucherRequest.ApplicationID}");
    }

    [Test]
    public async Task AddVoucherAsync_Throws_ResourceNotFoundException_When_Grant_Cannot_Be_Resolved()
    {
        // arrange
        _mockApplicationsProvider = new Mock<IApplicationsProvider>(MockBehavior.Strict);
        Add_NullReturning_GetGrantOrDefaultByTechTypeIdAsync_ToMockedApplicationsProvider(ref _mockApplicationsProvider);
        Add_GetApplicationOrDefaultByIdAsync_ToMockedApplicationsProvider(ref _mockApplicationsProvider);
        Add_GetTechTypeOrDefaultByIdAsync_ToMockedApplicationsProvider(ref _mockApplicationsProvider);

        var systemUnderTest = GenerateSystemUnderTest();

        AddVoucherRequest addVoucherRequest = new()
        {
            ApplicationID = applicationId,
            TechTypeId = techTypeId
        };

        // act
        Func<Task> act = () => _ = systemUnderTest.AddVoucherAsync(addVoucherRequest);

        // assert
        await act
          .Should().ThrowExactlyAsync<ResourceNotFoundException>()
          .WithMessage($"Could not find matching grant for Tech Type ID {addVoucherRequest.TechTypeId}");
    }

    [Test]
    public async Task AddVoucherAsync_Throws_ResourceNotFoundException_When_TechTypeID_Cannot_Be_Resolved()
    {
        // arrange
        _mockApplicationsProvider = new Mock<IApplicationsProvider>(MockBehavior.Strict);
        Add_NullReturning_GetTechTypeOrDefaultByIdAsync_ToMockedApplicationsProvider(ref _mockApplicationsProvider);
        Add_GetApplicationOrDefaultByIdAsync_ToMockedApplicationsProvider(ref _mockApplicationsProvider);
        Add_GetGrantOrDefaultByIdAsync_ToMockedApplicationsProvider(ref _mockApplicationsProvider);

        var systemUnderTest = GenerateSystemUnderTest();

        AddVoucherRequest addVoucherRequest = new()
        {
            ApplicationID = applicationId,
            TechTypeId = techTypeId
        };

        // act
        Func<Task> act = () => _ = systemUnderTest.AddVoucherAsync(addVoucherRequest);

        // assert
        await act
          .Should().ThrowExactlyAsync<ResourceNotFoundException>()
          .WithMessage($"Unable to resolve Tech Type ID {addVoucherRequest.TechTypeId}");
    }

    [Test]
    public async Task AddVoucherAsync_Should_Call_Provider_AddVoucherAsync_With_Valid_Voucher_When_Request_Is_Valid()
    {
        // arrange
        var addVoucherRequest = new AddVoucherRequest
        {
            ApplicationID = applicationId,
            TechTypeId = techTypeId
        };

        var addVoucherAsyncProviderResponse = new Voucher
        {
            ApplicationID = applicationId,
            GrantId = grantId
        };

        _mockApplicationsProvider = new Mock<IApplicationsProvider>(MockBehavior.Strict);
        Add_GetGrantOrDefaultByTechTypeIdAsync_ToMockedApplicationsProvider(ref _mockApplicationsProvider);
        Add_GetApplicationOrDefaultByIdAsync_ToMockedApplicationsProvider(ref _mockApplicationsProvider);
        Add_GetTechTypeOrDefaultByIdAsync_ToMockedApplicationsProvider(ref _mockApplicationsProvider);
        Add_AddVoucherAsync_Returns_ToMockedApplicationsProvider(ref _mockApplicationsProvider, addVoucherAsyncProviderResponse);

        _mockMapper
            .Setup(m => m.Map<Voucher>(addVoucherRequest))
            .Returns(new Voucher
            {
                ApplicationID = applicationId,
                GrantId = grantId
            });        

        var systemUnderTest = GenerateSystemUnderTest();

        // act
        var result = await systemUnderTest.AddVoucherAsync(addVoucherRequest);

        // assert
        result.Should().NotBeNull();
        result.ApplicationID.Should().Be(applicationId);
        result.GrantId.Should().Be(grantId);
        result.Should().BeEquivalentTo(addVoucherAsyncProviderResponse);
    }
}