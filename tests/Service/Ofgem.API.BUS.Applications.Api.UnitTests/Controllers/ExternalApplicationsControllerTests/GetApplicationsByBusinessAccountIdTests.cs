using FluentAssertions;
using NUnit.Framework;
using Ofgem.API.BUS.Applications.Domain;
using Ofgem.API.BUS.Applications.Domain.Constants;
using System;
using System.Threading.Tasks;

namespace Ofgem.API.BUS.Applications.Api.UnitTests.Controllers.ExternalApplicationsControllerTests;

[TestFixture]
public class GetApplicationsByBusinessAccountIdTests : ExternalApplicationsControllerTestsBase
{
    private static readonly Guid businessAccountId = new("52C2DA14-24DA-4C16-3E6C-08DA383A01B2");

    [Test]
    public void GetApplicationsByBusinessAccountIdAsync_When_Successful()
    {
        //Arrange
        var systemUnderTest = GenerateSystemUnderTest();
        var expectedResult = systemUnderTest.Ok();

        //Act
        var result = systemUnderTest.GetApplicationsByBusinessAccountId(businessAccountId);

        //Assert
        result.Should().BeEquivalentTo(expectedResult);
    }

    [Test]
    public void GetApplicationsByBusinessAccountIdAsync_With_Search_Parameter()
    {
        //Arrange
        var systemUnderTest = GenerateSystemUnderTest();
        var expectedResult = systemUnderTest.Ok();

        //Act
        var result = systemUnderTest.GetApplicationsByBusinessAccountId(businessAccountId, "postcode");

        //Assert
        result.Should().BeEquivalentTo(expectedResult);
    }

    [Test]
    public void GetApplicationsByBusinessAccountIdAsync_With_Filter_Parameter()
    {
        //Arrange
        var systemUnderTest = GenerateSystemUnderTest();
        var expectedResult = systemUnderTest.Ok();
        var statusId = new[] { StatusMappings.ApplicationStatus[ApplicationStatus.ApplicationStatusCode.SUB] };

        //Act
        var result = systemUnderTest.GetApplicationsByBusinessAccountId(businessAccountId, statusIds: statusId);

        //Assert
        result.Should().BeEquivalentTo(expectedResult);
    }

    [Test]
    public void GetApplicationsByBusinessAccountIdAsync_With_Search_And_Filter_Parameter()
    {
        //Arrange
        var systemUnderTest = GenerateSystemUnderTest();
        var expectedResult = systemUnderTest.Ok();
        var statusId = new[] { StatusMappings.ApplicationStatus[ApplicationStatus.ApplicationStatusCode.SUB] };

        //Act
        var result = systemUnderTest.GetApplicationsByBusinessAccountId(businessAccountId, "postcode", statusId);

        //Assert
        result.Should().BeEquivalentTo(expectedResult);
    }
}
