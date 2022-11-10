using FluentAssertions;
using Moq;
using NUnit.Framework;
using Ofgem.API.BUS.Applications.Providers.DataAccess.Interfaces;
using System.Threading.Tasks;

namespace Ofgem.API.BUS.Applications.Core.UnitTests.ApplicationsServiceTests;

/// <summary>
/// Tests for the ApplicationsService class' implementation of IApplicationsService.GetTechTypeListAsync
/// </summary>
public class GetTechTypeListAsyncTests : ApplicationsServiceTestsBase
{
    [Test]
    public async Task GetTechTypeListAsync_Should_Not_Return_Null()
    {
        // arrange
        var systemUnderTest = GenerateSystemUnderTest();

        // act
        var result = await systemUnderTest.GetTechTypeListAsync();

        // assert
        result.Should().NotBeNull();
    }

    [Test]
    public async Task GetTechTypeListAsync_Should_Return_Provider_Response()
    {
        // arrange
        _mockApplicationsProvider = new Mock<IApplicationsProvider>(MockBehavior.Strict);
        Add_GetTechtypeListAsync_ToMockedApplicationsProvider(ref _mockApplicationsProvider);

        var systemUnderTest = GenerateSystemUnderTest();

        // act
        var result = await systemUnderTest.GetTechTypeListAsync();

        // assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(techTypesList);
    }
}
