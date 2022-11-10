using FluentAssertions;
using Moq;
using NUnit.Framework;
using Ofgem.API.BUS.Applications.Core.Interfaces;
using Ofgem.API.BUS.Applications.Domain;
using Ofgem.API.BUS.Applications.Domain.Constants;
using Ofgem.API.BUS.Applications.Domain.Entities.CommsObjects;
using Ofgem.API.BUS.Applications.Providers.DataAccess.Interfaces;
using Ofgem.Lib.BUS.APIClient.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ofgem.API.BUS.Applications.Core.UnitTests.ApplicationsServiceTests;

/// <summary>
/// Tests for the ApplicationsService class' implementation of IApplicationsService.CreateApplicationAsync
/// </summary>
public class CreateApplicationAsyncTests : ApplicationsServiceTestsBase
{
    [Test]
    public async Task CreateApplicationAsync_Should_Throw_ArgumentNullException_When_Request_Is_Null()
    {
        // arrange
        var systemUnderTest = GenerateSystemUnderTest();

        // act
        Func<Task> act = () => _ = systemUnderTest.CreateApplicationAsync(null!);

        // assert
        await act
          .Should().ThrowExactlyAsync<ArgumentNullException>()
          .WithParameterName("request")
          .WithMessage("Value cannot be null. (Parameter 'request')");
    }

    [Test]
    public async Task CreateApplicationAsync_Should_Throw_BadRequestException_When_InstallationAddress_Is_Null()
    {
        // arrange
        _mockBusinessAccountsService = new Mock<IBusinessAccountsService>(MockBehavior.Strict);
        Add_GetBusinessAccountNameByIdAsync_ToMockedBusinessAccountsService(ref _mockBusinessAccountsService);

        _mockApplicationsProvider = new Mock<IApplicationsProvider>();
        Add_GetTechTypeOrDefaultByIdAsync_ToMockedApplicationsProvider(ref _mockApplicationsProvider);

        var systemUnderTest = GenerateSystemUnderTest();

        CreateApplicationRequest request = GenerateAValidCreateApplicationRequest();
        request.InstallationAddress = null!;

        // act
        Func<Task> act = () => _ = systemUnderTest.CreateApplicationAsync(request);

        // assert
        await act
          .Should().ThrowExactlyAsync<BadRequestException>()
          .WithMessage(ApplicationsExceptionMessages.NoInstallationAddress);
    }

    [Test]
    public async Task CreateApplicationAsync_Should_Throw_BadRequestException_When_BusinessAccountID_Is_Empty()
    {
        // arrange
        _mockBusinessAccountsService = new Mock<IBusinessAccountsService>(MockBehavior.Strict);
        Add_GetBusinessAccountNameByIdAsync_ToMockedBusinessAccountsService(ref _mockBusinessAccountsService);

        _mockApplicationsProvider = new Mock<IApplicationsProvider>();
        Add_GetTechTypeOrDefaultByIdAsync_ToMockedApplicationsProvider(ref _mockApplicationsProvider);

        CreateApplicationRequest request = GenerateAValidCreateApplicationRequest();
        request.BusinessAccountID = Guid.Empty;

        var systemUnderTest = GenerateSystemUnderTest();

        // act
        Func<Task> act = () => _ = systemUnderTest.CreateApplicationAsync(request);

        // assert
        await act
          .Should().ThrowExactlyAsync<BadRequestException>()
          .WithMessage(ApplicationsExceptionMessages.NoBusinessAccount);
    }

    [Test]
    public async Task CreateApplicationAsync_Should_Throw_BadRequestException_If_Duplicate_Application_Exists_For_BusinessAccount()
    {
        // Arrange
        _mockBusinessAccountsService = new Mock<IBusinessAccountsService>(MockBehavior.Strict);
        Add_GetBusinessAccountNameByIdAsync_ToMockedBusinessAccountsService(ref _mockBusinessAccountsService);

        CreateApplicationRequest request = GenerateAValidCreateApplicationRequest();
        Application existingApplication = TestApplication;
        existingApplication.BusinessAccountId = request.BusinessAccountID;

        _mockApplicationsProvider = new Mock<IApplicationsProvider>();
        Add_GetTechTypeOrDefaultByIdAsync_ToMockedApplicationsProvider(ref _mockApplicationsProvider);
        Add_GetNewApplicationNumberAsync_ToMockedApplicationsProvider(ref _mockApplicationsProvider);

        Add_GetApplicationsByInstallationAddressUprnAsync_Returns(ref _mockApplicationsProvider, new List<Application> { existingApplication });

        var systemUnderTest = GenerateSystemUnderTest();

        // act
        Func<Task> act = () => _ = systemUnderTest.CreateApplicationAsync(request);

        // assert
        await act
          .Should().ThrowExactlyAsync<BadRequestException>()
          .WithMessage("The business already has an application at this installation address");
    }

    private static CreateApplicationRequest GenerateAValidCreateApplicationRequest() => new CreateApplicationRequest()
    {
        BusinessAccountID = businessAccountId,
        TechTypeID = techTypeId,
        InstallationAddress = new CreateApplicationRequestInstallationAddress()
        {
            Line1 = "1 Main Street",
            Postcode = "SW1A 1AA",
            UPRN = "123456123456"
        },
        PropertyOwnerDetail = new CreateApplicationRequestPropertyOwnerDetail()
        {
            Email = "email@example.com",
            FullName = "Chester le Tester"
        },
        QuoteAmount = 12000.00M,
        ApplicationDate = DateTime.Now
    };

}
