using Moq;
using Notify.Interfaces;
using Notify.Models.Responses;
using NUnit.Framework;
using Ofgem.API.BUS.Applications.Core.Configuration;
using Ofgem.API.BUS.Applications.Core.Interfaces;
using Ofgem.API.BUS.Applications.Domain;
using System;
using System.Collections.Generic;

namespace Ofgem.API.BUS.Applications.Core.UnitTests.NotifyEmailServiceTests;

public abstract class NotifyEmailServiceTestsBase
{
    protected Mock<IAsyncNotificationClient> _mockNotifyClient = new();
    protected Mock<IApplicationsService> _mockApplicationsService = new();
    protected Mock<IBusinessAccountsService> _mockBusinessAccountsService = new();

    protected static Guid ApplicationId = Guid.NewGuid();
    protected static Guid TechTypeId = Guid.NewGuid();

    protected static Application TestApplication => new()
    {
        ID = ApplicationId,
        SubmitterId = Guid.NewGuid(),
        ReferenceNumber = "GID1234",
        BusinessAccountId = Guid.NewGuid(),
        TechTypeId = TechTypeId,
        QuoteAmount = 7500,
        SubStatus = new ApplicationSubStatus()
        {
            Code = ApplicationSubStatus.ApplicationSubStatusCode.SUB
        },
        PropertyOwnerDetail = new PropertyOwnerDetail
        {
            FullName = "Property Owner",
            Email = "propertyowner@email.com"
        },
        InstallationAddress = new InstallationAddress
        {
            AddressLine1 = "Installation 1",
            AddressLine2 = "Installation 2",
            AddressLine3 = "Installation 3",
            County = "Installation County",
            Postcode = "Installation Postcode",
            UPRN = "123456"
        },
        TechType = new TechType
        { 
            TechTypeDescription = "Heat Pump",
            ID = TechTypeId
        },
        ConsentRequests = new List<ConsentRequest>
        {
            new ConsentRequest
            {
                ID = Guid.NewGuid(),
                ApplicationID = ApplicationId,
                ConsentIssuedDate = DateTime.Now,
                ConsentExpiryDate = DateTime.Now.AddDays(14),
                ConsentReceivedDate = null
            }
        }
    };

    protected static readonly GovNotifyConfiguration _govNotifyConfiguration = new()
    {
        ApiKey = "12345",
        InstallerPostApplicationEmailReplyToId = Guid.NewGuid(),
        InstallerPostApplicationTemplates = new InstallerPostApplicationTemplatesConfiguration
        {
            EligibleNewBuild = Guid.NewGuid(),
            EpcExemptions = Guid.NewGuid(),
            NoEvidenceRequired = Guid.NewGuid()
        }
    };

    protected static readonly ApplicationsApiConfiguration _applicationsApiConfiguration = new()
    {
        ExternalPortalBaseAddress = new Uri("http://www.externalportal.com")
    };

    [SetUp]
    public virtual void TestCaseSetUp()
    {
        _mockNotifyClient = new();
        _mockApplicationsService = new();
        _mockBusinessAccountsService = new();
    }

    protected NotifyEmailService GenerateSystemUnderTest() => new(
        _mockNotifyClient.Object, _govNotifyConfiguration, _mockBusinessAccountsService.Object, _mockApplicationsService.Object, _applicationsApiConfiguration);

    protected void Add_GetBusinessAccountEmailByInstallerIdAsync_To_BusinessAccountsService(ref Mock<IBusinessAccountsService> businessAccountsService)
    {
        businessAccountsService.Setup(m => m.GetBusinessAccountEmailByInstallerIdAsync(It.IsAny<Guid>()))
                               .ReturnsAsync("installer@email.com");
    }

    protected void Add_GetBusinessAccountNameByIdAsync_To_BusinessAccountsService(ref Mock<IBusinessAccountsService> businessAccountsService)
    {
        businessAccountsService.Setup(m => m.GetBusinessAccountNameByIdAsync(It.IsAny<Guid>()))
                               .ReturnsAsync("Installer Name");
    }

    protected void Add_SendEmailAsync_To_AsyncNotificationClient(ref Mock<IAsyncNotificationClient> notificationClient) =>
        notificationClient.Setup(m => m.SendEmailAsync(It.IsAny<string>(),
                                                       It.IsAny<string>(),
                                                       It.IsAny<Dictionary<string, object>>(),
                                                       It.IsAny<string>(),
                                                       It.IsAny<string>())).ReturnsAsync(new EmailNotificationResponse
                                                       {
                                                           id = "12345"
                                                       });
}
