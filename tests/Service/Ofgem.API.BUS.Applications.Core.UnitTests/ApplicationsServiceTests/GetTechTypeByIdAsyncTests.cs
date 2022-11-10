using FluentAssertions;
using Moq;
using NUnit.Framework;
using Ofgem.API.BUS.Applications.Providers.DataAccess.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace Ofgem.API.BUS.Applications.Core.UnitTests.ApplicationsServiceTests;

[TestFixture]
public class GetTechTypeByIdAsyncTests : ApplicationsServiceTestsBase
{
    [Test]
    public async Task GetTechTypeByIdAsync_Returns_ProviderResult()
    {
        // Arrange
        _mockApplicationsProvider = new Mock<IApplicationsProvider>(MockBehavior.Strict);
        Add_GetTechTypeOrDefaultByIdAsync_ToMockedApplicationsProvider(ref _mockApplicationsProvider);

        var systemUnderTest = GenerateSystemUnderTest();

        // act
        var result = await systemUnderTest.GetTechTypeByIdAsync(techTypeId);

        // assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(techTypesList.FirstOrDefault());
    }
}
