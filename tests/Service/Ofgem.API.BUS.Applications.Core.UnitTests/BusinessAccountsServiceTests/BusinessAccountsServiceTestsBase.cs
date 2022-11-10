using Moq;
using NUnit.Framework;
using Ofgem.API.BUS.BusinessAccounts.Client.Interfaces;
using Ofgem.API.BUS.BusinessAccounts.Domain.Entities;
using System;

namespace Ofgem.API.BUS.Applications.Core.UnitTests.BusinessAccountsServiceTests;

/// <summary>
/// Base class for all BusinessAccountsService test classes, with helpers, Mocks, etc
/// </summary>
public class BusinessAccountsServiceTestsBase
{
    protected Mock<IBusinessAccountsAPIClient> _mockBusinessAccountsAPIClient = new();

    [SetUp]
    public void Setup()
    {
        _mockBusinessAccountsAPIClient = new();
    }

    protected BusinessAccountsService GenerateSystemUnderTest() => new(
        _mockBusinessAccountsAPIClient.Object);

    #region Mock Setup Helpers

    #region BusinessAccountRequestsClient.GetInstallerEmailByInstallerId

    protected const string _defaultInstallerEmail = "testInstaler@example.com";
    protected void Add_EmptyStringReturning_GetInstallerEmailByInstallerId_To_BusinessAccountsApiClientMock(Guid? requestGuid = null)
    {
        _mockBusinessAccountsAPIClient
            .Setup(m => m.BusinessAccountRequestsClient.GetInstallerEmailByInstallerId(requestGuid ?? It.IsAny<Guid>()))
            .ReturnsAsync(string.Empty);
    }

    protected void Add_GetInstallerEmailByInstallerId_To_BusinessAccountsApiClientMock(Guid? requestGuid = null, string? returnValue = null)
    {
        if (requestGuid is null)
        {
            _mockBusinessAccountsAPIClient
            .Setup(m => m.BusinessAccountRequestsClient.GetInstallerEmailByInstallerId(It.IsAny<Guid>()))
            .ReturnsAsync(returnValue ?? _defaultInstallerEmail);
        }
        else
        {
            _mockBusinessAccountsAPIClient
               .Setup(m => m.BusinessAccountRequestsClient.GetInstallerEmailByInstallerId(requestGuid.Value))
               .ReturnsAsync(returnValue ?? _defaultInstallerEmail);
        }
    }

    #endregion

    #region GetBusinessAccountEmailByIdAsync


    protected const string _defaultBusinessAccountEmail = "testBusinessAccount@example.com";

    protected void Add_EmptyStringReturning_GetBusinessAccountEmailById_To_BusinessAccountsApiClientMock(Guid? requestGuid = null)
    {
        _mockBusinessAccountsAPIClient
            .Setup(m => m.BusinessAccountRequestsClient.GetBusinessAccountEmailById(requestGuid ?? It.IsAny<Guid>()))
            .ReturnsAsync(string.Empty);
    }

    protected void Add_GetInstallerEmailById_To_BusinessAccountsApiClientMock(Guid? requestGuid = null, string? returnValue = null)
    {
        if (requestGuid is null)
        {
            _mockBusinessAccountsAPIClient
            .Setup(m => m.BusinessAccountRequestsClient.GetBusinessAccountEmailById(It.IsAny<Guid>()))
            .ReturnsAsync(returnValue ?? _defaultBusinessAccountEmail);
        }
        else
        {
            _mockBusinessAccountsAPIClient
               .Setup(m => m.BusinessAccountRequestsClient.GetBusinessAccountEmailById(requestGuid.Value))
               .ReturnsAsync(returnValue ?? _defaultBusinessAccountEmail);
        }
    }

    #endregion


    #region GetBusinessAccountAsync

    protected const string _defaultBusinessAccountBusinessName = "Test BusinessName";
    protected BusinessAccount _defaultBusinessAccount => new()
    {
        BusinessName = _defaultBusinessAccountBusinessName
    };

    protected void Add_NullReturning_GetBusinessAccountAsync_To_BusinessAccountsApiClientMock(Guid? requestGuid = null)
    {
        _mockBusinessAccountsAPIClient
            .Setup(m => m.BusinessAccountRequestsClient.GetBusinessAccountAsync(requestGuid ?? It.IsAny<Guid>()))
            .ReturnsAsync((BusinessAccount)null!);
    }

    protected void Add_GetBusinessAccountAsync_To_BusinessAccountsApiClientMock(Guid? requestGuid = null, BusinessAccount? returnValue = null)
    {
        if (requestGuid is null)
        {
            _mockBusinessAccountsAPIClient
            .Setup(m => m.BusinessAccountRequestsClient.GetBusinessAccountAsync(It.IsAny<Guid>()))
            .ReturnsAsync(returnValue ?? _defaultBusinessAccount);
        }
        else
        {
            _mockBusinessAccountsAPIClient
               .Setup(m => m.BusinessAccountRequestsClient.GetBusinessAccountAsync(requestGuid.Value))
               .ReturnsAsync(returnValue ?? _defaultBusinessAccount);
        }
    }

    #endregion

    #endregion

}
