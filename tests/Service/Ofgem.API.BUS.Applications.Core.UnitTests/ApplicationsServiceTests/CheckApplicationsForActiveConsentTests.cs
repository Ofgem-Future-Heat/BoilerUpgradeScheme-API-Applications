using FluentAssertions;
using NUnit.Framework;
using Ofgem.API.BUS.Applications.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ofgem.API.BUS.Applications.Core.UnitTests.ApplicationsServiceTests;

/// <summary>
/// Tests for the ApplicationsService class' implementation of IApplicationsService.CheckApplicationsForActiveConsent
/// </summary>
public class CheckApplicationsForActiveConsentTests : ApplicationsServiceTestsBase
{
    [Test]
    public void CheckApplicationsForActiveConsent_Should_Return_False_For_Single_Application_With_No_Consent_Request()
    {
        // Arrange
        // Given a single application in the list, and that the property owner has not yet given consent...
        var application = TestApplication;
        application.ConsentRequests = new List<ConsentRequest>();

        var applications = new List<Application> { application };

        var systemUnderTest = GenerateSystemUnderTest();

        // Act
        // ... when we check for active consent...
        var consentedApplicationExists = systemUnderTest.CheckApplicationsForActiveConsent(applications);

        // Assert
        // ... the check returns false
        consentedApplicationExists.Should().BeFalse();
    }

    [Test]
    public void CheckApplicationsForActiveConsent_Should_Return_False_For_Single_Application_With_Single_Unconsented_Consent_Request()
    {
        // Arrange
        // Given a single application, and that the property owner has not yet given consent...
        var applications = new List<Application> { TestApplication };

        var systemUnderTest = GenerateSystemUnderTest();

        // Act
        // ... when we check for active consent...
        var consentedApplicationExists = systemUnderTest.CheckApplicationsForActiveConsent(applications);

        // Assert
        // ... the check returns false
        consentedApplicationExists.Should().BeFalse();
    }

    [Test]
    public void CheckApplicationsForActiveConsent_Should_Return_False_For_Multiple_Applications_With_Unconsented_Consent_Requests()
    {
        // Arrange
        // Given multiple applications with one consent request, all unconsented...
        var application1 = TestApplication;
        var application2 = TestApplication;
        var applications = new List<Application> { application1, application2 };

        var systemUnderTest = GenerateSystemUnderTest();

        // Act
        // ... when we check for active consent...
        var consentedApplicationExists = systemUnderTest.CheckApplicationsForActiveConsent(applications);

        // Assert
        // ... the check returns false
        consentedApplicationExists.Should().BeFalse();
    }

    [Test]
    public void CheckApplicationsForActiveConsent_Should_Return_False_For_Rejected_Applications_Despite_Accepted_Consent_Requests()
    {
        // Arrange
        // Given multiple applications, all with some sort of Rejected status...
        var application1 = TestApplication;
        application1.ConsentRequests.First().ConsentReceivedDate = DateTime.Now;
        application1.SubStatus!.Code = ApplicationSubStatus.ApplicationSubStatusCode.CNTRD;

        var application2 = TestApplication;
        application2.ConsentRequests.First().ConsentReceivedDate = DateTime.Now;
        application2.SubStatus!.Code = ApplicationSubStatus.ApplicationSubStatusCode.REJECTED;

        var application3 = TestApplication;

        var applications = new List<Application> { application1, application2, application3 };

        var systemUnderTest = GenerateSystemUnderTest();

        // Act
        // ... when we check for active consent...
        var consentedApplicationExists = systemUnderTest.CheckApplicationsForActiveConsent(applications);

        // Assert
        // ... the check returns false
        consentedApplicationExists.Should().BeFalse();
    }

    [Test]
    public void CheckApplicationsForActiveConsent_Should_Return_True_For_Application_With_Consented_Consent_Request()
    {
        // Arrange
        // Given a single application, and that the property owner has already given consent...
        var application = TestApplication;
        application.ConsentRequests.First().ConsentReceivedDate = DateTime.Now;
        application.SubStatus!.Code = ApplicationSubStatus.ApplicationSubStatusCode.INRW;

        var applications = new List<Application> { application };

        var systemUnderTest = GenerateSystemUnderTest();

        // Act
        // ... when we check for active consent...
        var consentedApplicationExists = systemUnderTest.CheckApplicationsForActiveConsent(applications);

        // Assert
        consentedApplicationExists.Should().BeTrue();
    }

    [Test]
    public void CheckApplicationsForActiveConsent_Should_Return_True_For_Application_With_Multiple_Consented_Consent_Requests()
    {
        // Arrange
        // Given multiple applications, and that the property owner has already consented to one of them...

        var application1 = TestApplication;
        application1.ConsentRequests.First().ConsentReceivedDate = DateTime.Now;
        application1.SubStatus!.Code = ApplicationSubStatus.ApplicationSubStatusCode.INRW;

        var application2 = TestApplication;

        var applications = new List<Application> { application1, application2 };

        var systemUnderTest = GenerateSystemUnderTest();

        // Act
        // ... when we check for active consent...
        var consentedApplicationExists = systemUnderTest.CheckApplicationsForActiveConsent(applications);

        // Assert
        //... the check returns true.
        consentedApplicationExists.Should().BeTrue();
    }
}
