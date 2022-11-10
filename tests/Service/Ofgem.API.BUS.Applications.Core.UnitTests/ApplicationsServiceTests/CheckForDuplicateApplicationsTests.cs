using FluentAssertions;
using NUnit.Framework;
using Ofgem.API.BUS.Applications.Domain;
using Ofgem.API.BUS.Applications.Domain.Constants;
using System;
using System.Collections.Generic;
using static Ofgem.API.BUS.Applications.Domain.ApplicationSubStatus;

namespace Ofgem.API.BUS.Applications.Core.UnitTests.ApplicationsServiceTests;

/// <summary>
/// Tests for the ApplicationsService class' implementation of IApplicationsService.CheckForDuplicateApplications
/// </summary>
public class CheckForDuplicateApplicationsTests : ApplicationsServiceTestsBase
{
    [Test]
    public void CheckForDuplicateApplications_Should_Return_True_If_Duplicate_Applications_Exist()
    {
        // Arrange
        var existingApplication = TestApplication;

        var systemUnderTest = GenerateSystemUnderTest();

        // Act
        var hasDuplicates = systemUnderTest.CheckForDuplicateApplications(businessAccountId, new List<Application> { existingApplication });

        // Assert
        hasDuplicates.Should().BeTrue();
    }

    [TestCase(ApplicationSubStatusCode.CNTRD)]
    [TestCase(ApplicationSubStatusCode.WITHDRAWN)]
    [TestCase(ApplicationSubStatusCode.VEXPD)]
    [TestCase(ApplicationSubStatusCode.REJECTED)]
    [TestCase(ApplicationSubStatusCode.RPEND)]
    public void CheckForDuplicateApplications_Should_Return_False_If_Business_Has_Rejected_Or_Cancelled_Applications(ApplicationSubStatusCode subStatus)
    {
        // Arrange
        var systemUnderTest = GenerateSystemUnderTest();

        var application = new Application
        {
            ID = Guid.NewGuid(),
            BusinessAccountId = Guid.NewGuid(),
            InstallationAddress = new InstallationAddress
            {
                AddressLine1 = "Address 1",
                UPRN = "123456123456"
            },
            SubStatusId = StatusMappings.ApplicationSubStatus[subStatus],
            SubStatus = new ApplicationSubStatus
            {
                ApplicationStatusId = StatusMappings.ApplicationSubStatus[subStatus],
                Code = subStatus
            }
        };

        // Act
        var hasDuplicates = systemUnderTest.CheckForDuplicateApplications(businessAccountId, new List<Application> { application });

        // Assert
        hasDuplicates.Should().BeFalse();
    }

    [Test]
    public void CheckForDuplicateApplications_Should_Return_False_If_No_Duplicate_Application_Exists()
    {
        // Arrange
        var businessAccountId = Guid.NewGuid();
        var installationUprn = "123456123456";

        var existingApplications = new List<Application>
            {
                new Application
                {
                    ID = Guid.NewGuid(),
                    BusinessAccountId = Guid.NewGuid(),
                    InstallationAddress = new InstallationAddress
                    {
                        AddressLine1 = "Address 1",
                        UPRN = installationUprn
                    },
                    SubStatusId = StatusMappings.ApplicationSubStatus[ApplicationSubStatusCode.SUB],
                    SubStatus = new ApplicationSubStatus
                    {
                        ApplicationStatusId = StatusMappings.ApplicationSubStatus[ApplicationSubStatusCode.SUB],
                        Code = ApplicationSubStatusCode.SUB
                    }
                },
                new Application
                {
                    ID = Guid.NewGuid(),
                    BusinessAccountId = businessAccountId,
                    InstallationAddress = new InstallationAddress
                    {
                        AddressLine1 = "Address 1",
                        UPRN = "A random UPRN"
                    },
                    SubStatusId = StatusMappings.ApplicationSubStatus[ApplicationSubStatusCode.SUB],
                    SubStatus = new ApplicationSubStatus
                    {
                        ApplicationStatusId = StatusMappings.ApplicationSubStatus[ApplicationSubStatusCode.SUB],
                        Code = ApplicationSubStatusCode.SUB
                    }
                }
            };

        var systemUnderTest = GenerateSystemUnderTest();

        // Act
        var hasDuplicates = systemUnderTest.CheckForDuplicateApplications(businessAccountId, existingApplications);

        // Assert
        hasDuplicates.Should().BeTrue();
    }
}
