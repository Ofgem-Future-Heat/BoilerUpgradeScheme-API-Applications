using AutoMapper;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Moq;
using NUnit.Framework;
using Ofgem.API.BUS.Applications.Core.Configuration;
using Ofgem.API.BUS.Applications.Core.Interfaces;
using Ofgem.API.BUS.Applications.Domain;
using Ofgem.API.BUS.Applications.Domain.Entities;
using Ofgem.API.BUS.Applications.Domain.Entities.CommsObjects;
using Ofgem.API.BUS.Applications.Domain.Request;
using Ofgem.API.BUS.Applications.Providers.DataAccess.Interfaces;
using Ofgem.API.BUS.PropertyConsents.Domain.Models.CommsObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ofgem.API.BUS.Applications.Core.UnitTests.ApplicationsServiceTests;

/// <summary>
/// Base class for all ApplicationsService test classes, with helpers, Mocks, etc
/// </summary>
public abstract class ApplicationsServiceTestsBase
{
    protected Mock<IMapper> _mockMapper = new();
    protected Mock<IBusinessAccountsService> _mockBusinessAccountsService = new();
    protected Mock<IPropertyConsentService> _mockPropertyConsentService = new();
    protected Mock<IApplicationsProvider> _mockApplicationsProvider = new();

    [SetUp]
    public void Setup()
    {
        _mockMapper = new();
        _mockBusinessAccountsService = new();
        _mockPropertyConsentService = new();
        _mockApplicationsProvider = new();
    }

    public static readonly Guid consentRequestId = Guid.NewGuid();
    public static readonly Guid applicationId = Guid.NewGuid();
    public static readonly Guid businessAccountId = Guid.NewGuid();
    public static readonly Guid installerId = Guid.NewGuid();
    public static readonly Guid currentContactId = Guid.NewGuid();
    public static readonly Guid techTypeId = Guid.NewGuid();
    public static readonly Guid grantId = Guid.NewGuid();
    public static readonly decimal quoteAmount = 22222.22M;
    public static readonly string propertyOwnerFullName = "Test Property Owner Full Name";
    public static readonly string applicationReferenceNumber = "GID10000000";
    public static readonly string installerBusinessName = "Test Installer Business Name";
    public static readonly string installerEmailAddress = "installer@test.com";
    public static readonly string currentContactEmailAddress = "contact@test.com";
    public static readonly string propertyOwnerEmailAddress = "propertyowner@test.com";
    public static readonly string installationPropertyAddressL1 = "Test Address Line 1";
    public static readonly string installationPropertyAddressL2 = "Test Address Line 2";
    public static readonly string installationPropertyAddressL3 = "Test Address Line 3";
    public static readonly string installationPropertyAddressCounty = "Test County";
    public static readonly string installationPropertyAddressPostcode = "Postcode";
    public static readonly string installationPropertyAddressUPRN = "123456123456";
    public static readonly string techTypeDescription = "Test Technology Type";
    public static readonly DateTime consentIssuedDate = DateTime.Now;
    public static readonly DateTime consentExpiryDate = consentIssuedDate.AddDays(14);
    public static readonly List<TechType> techTypesList = new()
    {
        new TechType
        {
            ID = Guid.NewGuid(),
            TechTypeDescription = "Test Tech Type 1",
            ExpiryIntervalMonths = 3,
            MCSTechTypeId = Guid.NewGuid()
        },
        new TechType
        {
            ID = Guid.NewGuid(),
            TechTypeDescription = "Test Tech Type 2",
            ExpiryIntervalMonths = 6,
            MCSTechTypeId = Guid.NewGuid()
        }
    };

    public static readonly List<Grant> grantsList = new()
    {
        new Grant
        {
            ID = Guid.NewGuid(),
        },
        new Grant
        {
            ID = Guid.NewGuid(),
        }
    };

    protected static Application TestApplication => new()
    {
        ID = applicationId,
        SubmitterId = installerId,
        ReferenceNumber = applicationReferenceNumber,
        BusinessAccountId = businessAccountId,
        TechTypeId = techTypeId,
        QuoteAmount = quoteAmount,
        SubStatus = new ApplicationSubStatus()
        {
            Code = ApplicationSubStatus.ApplicationSubStatusCode.SUB
        },
        PropertyOwnerDetail = new PropertyOwnerDetail
        {
            FullName = propertyOwnerFullName,
            Email = propertyOwnerEmailAddress
        },
        InstallationAddress = new InstallationAddress
        {
            AddressLine1 = installationPropertyAddressL1,
            AddressLine2 = installationPropertyAddressL2,
            AddressLine3 = installationPropertyAddressL3,
            County = installationPropertyAddressCounty,
            Postcode = installationPropertyAddressPostcode,
            UPRN = installationPropertyAddressUPRN
        },
        TechType = new TechType
        {
            TechTypeDescription = techTypeDescription
        },
        ConsentRequests = new List<ConsentRequest>
        {
            new ConsentRequest
            {
                ID = consentRequestId,
                ApplicationID = applicationId,
                ConsentIssuedDate = consentIssuedDate,
                ConsentExpiryDate = consentExpiryDate,
                ConsentReceivedDate = null
            }
        }
    };

    protected static List<Application> TestListOfApplications => new()
    {
        new Application
        {
            ID = applicationId
        },
        new Application
        {
            ID = applicationId
        },
        new Application
        {
            ID = applicationId
        },
        new Application
        {
            ID = applicationId
        }
    };

    protected static List<ApplicationSubStatus> TestApplicationSubStatusList => new()
    {
        new ApplicationSubStatus
        {
            Id = Guid.NewGuid()
        },
        new ApplicationSubStatus
        {
            Id = Guid.NewGuid()
        }
    };

    protected static List<ApplicationVoucherSubStatus> TestApplicationVoucherSubStatusList => new()
    {
        new ApplicationVoucherSubStatus
        {
            Id = Guid.NewGuid()
        },
        new ApplicationVoucherSubStatus
        {
            Id = Guid.NewGuid()
        }
    };

    protected static List<VoucherSubStatus> TestVoucherSubStatusList => new()
    {
        new VoucherSubStatus
        {
            Id = Guid.NewGuid()
        },
        new VoucherSubStatus
        {
            Id = Guid.NewGuid()
        }
    };

    protected static PagedResult<ApplicationDashboard> PagesApplicationResults => new()
    {
        CurrentPage = 1,
        PageSize = 1,
        PageCount = 1,
        RowCount = 1,
        Results = new List<ApplicationDashboard>() { new ApplicationDashboard { ApplicationDate = "2020-06-01" } }
    };

    protected static List<Grant> TestGrantList => new()
    {
        new Grant
        {
            ID = grantId,
            TechTypeID = techTypeId
        }
    };

    protected static StoreServiceFeedbackRequest TestFeedback => new()
    {
        ApplicationID = Guid.NewGuid(),
        FeedbackNarrative = "This is feedback",
        ServiceUsed = "Consent",
        SurveyOption = 1
    };

    public static readonly ApplicationsApiConfiguration applicationsApiConfiguration = new()
    {
        ConsentEmailExpiryDays = 14,
        ConsentPortalBaseAddress = new Uri("https://consentportal.com"),
        ExternalPortalBaseAddress = new Uri("https://externalportal.com")
    };

    protected ApplicationsService GenerateSystemUnderTest() => new(
        _mockMapper.Object,
        _mockApplicationsProvider.Object,
        _mockBusinessAccountsService.Object,
        _mockPropertyConsentService.Object,
        applicationsApiConfiguration);

    #region Mock Setup Helpers

    #region Add_Get_ToMockedApplicationsProvider


    protected static void Add_GetPagedApplications_Return_Collection(ref Mock<IApplicationsProvider> mock, PagedResult<ApplicationDashboard> pagedResult)
    {
        mock
            .Setup(m => m.GetPagedApplications(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<bool>(),
            It.IsAny<List<string>?>(),
            It.IsAny<List<string>>(),
            It.IsAny<string?>(),
            It.IsAny<string?>())).ReturnsAsync(pagedResult);
    }
    protected static void Add_GetPagedApplications_Return_Collection(ref Mock<IApplicationsProvider> mock)
    {
        mock
            .Setup(m => m.GetPagedApplications(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<bool>(),
            It.IsAny<List<string>?>(),
            It.IsAny<List<string>>(),
            It.IsAny<string?>(),
            It.IsAny<string?>())).ReturnsAsync(PagesApplicationResults);
    }

    protected static void Add_AddVoucherAsync_Returns_ToMockedApplicationsProvider(ref Mock<IApplicationsProvider> mock, Voucher voucherToReturn)
    {
        mock
            .Setup(m => m.AddVoucherAsync(It.IsAny<Voucher>()))
            .ReturnsAsync(voucherToReturn);
    }

    protected static void Add_GetApplicationsByInstallationAddressUprnAsync_Returns(ref Mock<IApplicationsProvider> mock, List<Application> returnsApplications)
    {
        mock
            .Setup(m => m.GetApplicationsByInstallationAddressUprnAsync(It.IsAny<string>()))
            .ReturnsAsync(returnsApplications);
    }

    protected static void Add_GetApplicationWithConsentObjectsByIdAsync_ToMockedApplicationsProvider(ref Mock<IApplicationsProvider> mock)
    {
        mock
            .Setup(m => m.GetApplicationWithConsentObjectsByIdAsync(applicationId))
            .ReturnsAsync(TestApplication);
    }

    protected static void Add_GetApplicationOrDefaultByIdAsync_ToMockedApplicationsProvider(ref Mock<IApplicationsProvider> mock)
    {
        mock
            .Setup(m => m.GetApplicationOrDefaultByIdAsync(applicationId))
            .ReturnsAsync(TestApplication);
    }

    protected static void Add_GetApplicationOrDefaultWithConsentObjectsByIdAsync_ToMockedApplicationsProvider(ref Mock<IApplicationsProvider> mock, Application? returnObject = null)
    {
        mock
            .Setup(m => m.GetApplicationOrDefaultWithConsentObjectsByIdAsync(applicationId))
            .ReturnsAsync(returnObject ?? TestApplication);
    }

    protected static void Add_GetApplicationSubStatusesListAsync_ToMockedApplicationsProvider(ref Mock<IApplicationsProvider> mock, List<ApplicationSubStatus>? returnList = null)
    {
        mock
            .Setup(m => m.GetApplicationSubStatusesListAsync())
            .ReturnsAsync(returnList ?? TestApplicationSubStatusList);
    }

    protected static void Add_GetApplicationVoucherSubStatusesListAsync_ToMockedApplicationsProvider(ref Mock<IApplicationsProvider> mock, List<ApplicationVoucherSubStatus>? returnList = null)
    {
        mock
            .Setup(m => m.GetApplicationVoucherSubStatusesListAsync())
            .ReturnsAsync(returnList ?? TestApplicationVoucherSubStatusList);
    }

    protected static void Add_GetGrantsListAsync_ToMockedApplicationsProvider(ref Mock<IApplicationsProvider> mock, List<Grant>? returnList = null)
    {
        mock
            .Setup(m => m.GetGrantsListAsync())
            .ReturnsAsync(returnList ?? TestGrantList);
    }

    protected static void Add_GetVoucherSubStatusesListAsync_ToMockedApplicationsProvider(ref Mock<IApplicationsProvider> mock, List<VoucherSubStatus>? returnList = null)
    {
        mock
            .Setup(m => m.GetVoucherSubStatusesListAsync())
            .ReturnsAsync(returnList ?? TestVoucherSubStatusList);
    }

    protected static void Add_GetConsentRequestOrDefaultByIdAsync_ToMockedApplicationsProvider(ref Mock<IApplicationsProvider> mock)
    {
        mock
            .Setup(m => m.GetConsentRequestOrDefaultByIdAsync(consentRequestId))
            .ReturnsAsync(new ConsentRequest
            {
                ID = consentRequestId,
                ApplicationID = applicationId,
                ConsentReceivedDate = null,
                ConsentExpiryDate = consentExpiryDate
            });
    }

    protected static void Add_GetGrantOrDefaultByIdAsync_ToMockedApplicationsProvider(ref Mock<IApplicationsProvider> mock)
    {
        mock
            .Setup(m => m.GetGrantOrDefaultByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(new Grant());
    }

    protected static void Add_GetGrantOrDefaultByTechTypeIdAsync_ToMockedApplicationsProvider(ref Mock<IApplicationsProvider> mock)
    {
        mock
            .Setup(m => m.GetGrantOrDefaultByTechTypeIdAsync(techTypeId))
            .ReturnsAsync(new Grant
            {
                ID = grantId,
                TechTypeID = techTypeId,
                StartDate = DateTime.Now.AddDays(-1),
                EndDate = DateTime.Now.AddDays(30),
                Amount = 5000.00M
            });
    }

    private static int _applicationNumber = 1000000;
    protected static void Add_GetNewApplicationNumberAsync_ToMockedApplicationsProvider(ref Mock<IApplicationsProvider> mock)
    {
        mock
            .Setup(m => m.GetNewApplicationNumberAsync().Result)
            .Returns(_applicationNumber++);
    }

    protected static void Add_GetTechtypeListAsync_ToMockedApplicationsProvider(ref Mock<IApplicationsProvider> mock)
    {
        mock
            .Setup(m => m.GetTechTypeListAsync())
            .ReturnsAsync(techTypesList);
    }

    protected static void Add_GetGrantListAsync_ToMockedApplicationsProvider(ref Mock<IApplicationsProvider> mock)
    {
        mock
            .Setup(m => m.GetGrantsListAsync())
            .ReturnsAsync(grantsList);
    }

    protected static void Add_GetTechTypeOrDefaultByIdAsync_ToMockedApplicationsProvider(ref Mock<IApplicationsProvider> mock)
    {
        mock
            .Setup(m => m.GetTechTypeOrDefaultByIdAsync(techTypeId))
            .ReturnsAsync(techTypesList.FirstOrDefault());
    }

    #endregion

    #region Add_NullReturning_ToMockedApplicationsProvider
    protected static void Add_NullReturning_GetApplicationOrDefaultWithConsentObjectsByIdAsync_ToMockedApplicationsProvider(ref Mock<IApplicationsProvider> mock)
    {
        mock
              .Setup(m => m.GetApplicationOrDefaultWithConsentObjectsByIdAsync(applicationId))
              .ReturnsAsync((Application?)null);
    }
    protected static void Add_NullReturning_GetApplicationOrDefaultByReferenceNumberAsync_ToMockedApplicationsProvider(ref Mock<IApplicationsProvider> mock)
    {
        mock
            .Setup(m => m.GetApplicationOrDefaultByReferenceNumberAsync(applicationReferenceNumber))
            .ReturnsAsync((Application?)null);
    }

    protected static void Add_NullReturning_GetApplicationWithConsentObjectsByIdAsync_ToMockedApplicationsProvider(ref Mock<IApplicationsProvider> mock)
    {
        mock
            .Setup(m => m.GetApplicationOrDefaultByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync((Application?)null);
    }

    protected static void Add_NullReturning_GetGrantOrDefaultByTechTypeIdAsync_ToMockedApplicationsProvider(ref Mock<IApplicationsProvider> mock)
    {
        mock
            .Setup(m => m.GetGrantOrDefaultByTechTypeIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync((Grant?)null);
    }

    protected static void Add_NullReturning_GetTechTypeOrDefaultByIdAsync_ToMockedApplicationsProvider(ref Mock<IApplicationsProvider> mock)
    {
        mock
            .Setup(m => m.GetTechTypeOrDefaultByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync((TechType?)null);
    }

    #endregion

    #region Add_Get_ToMockedBusinessAccountsService
    protected static void Add_GetBusinessAccountEmailByInstallerIdAsync_ToMockedBusinessAccountsService(ref Mock<IBusinessAccountsService> mock)
    {
        mock
            .Setup(m => m.GetBusinessAccountEmailByInstallerIdAsync(installerId))
            .ReturnsAsync(installerEmailAddress);
    }

    protected static void Add_GetApplicationOrDefaultWithConsentObjectsByIdAsync_ToBeMocked(ref Mock<IApplicationsProvider> mock)
    {
        mock
            .Setup(m => m.GetApplicationOrDefaultWithConsentObjectsByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(TestApplication);
    }

    protected static void Add_GetApplicationsByInstallationAddressUprnAsync_ToBeMocked(ref Mock<IApplicationsProvider> mock)
    {
        mock
            .Setup(m => m.GetApplicationsByInstallationAddressUprnAsync(It.IsAny<string>()))
            .ReturnsAsync(TestListOfApplications);
    }

    protected static void Add_GetBusinessAccountNameByIdAsync_ToBeMocked(ref Mock<IBusinessAccountsService> mock)
    {
        mock
            .Setup(m => m.GetBusinessAccountNameByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(installerBusinessName);
    }
    protected static void Add_SendConsentEmailAsync_ToBeMocked(ref Mock<IPropertyConsentService> mock, SendConsentEmailResult sendConsentEmailResult)
    {
        mock
            .Setup(m => m.SendConsentEmailAsync(It.IsAny<SendConsentEmailRequest>()))
            .ReturnsAsync(sendConsentEmailResult);
    }

    protected static void Add_GetBusinessAccountNameByIdAsync_ToMockedBusinessAccountsService(ref Mock<IBusinessAccountsService> mock)
    {
        mock
            .Setup(m => m.GetBusinessAccountNameByIdAsync(businessAccountId))
            .ReturnsAsync(installerBusinessName);
    }
    #endregion

    
    protected static void Add_NullReturning_GetBusinessEmailAddressByInstallerId_ToMockedBusinessAccountsService(ref Mock<IBusinessAccountsService> mock)
    {
        mock
            .Setup(m => m.GetBusinessAccountEmailByInstallerIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(() => null!);
    }


    #endregion
}
