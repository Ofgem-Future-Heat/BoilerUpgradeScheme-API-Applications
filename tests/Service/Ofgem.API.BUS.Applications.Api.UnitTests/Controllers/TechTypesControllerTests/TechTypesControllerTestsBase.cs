using Moq;
using Ofgem.API.BUS.Applications.Api.Controllers;
using Ofgem.API.BUS.Applications.Core.Interfaces;

namespace Ofgem.API.BUS.Applications.Api.UnitTests.Controllers.TechTypesControllerTests;

/// <summary>
/// Base class for all TechTypesController test classes, with helpers, Mocks, etc
/// </summary>
public class TechTypesControllerTestsBase
{
    protected Mock<IApplicationsService> _mockApplicationsService = new();

    public TechTypesController GenerateSystemUnderTest() => new(
        _mockApplicationsService.Object);
}
