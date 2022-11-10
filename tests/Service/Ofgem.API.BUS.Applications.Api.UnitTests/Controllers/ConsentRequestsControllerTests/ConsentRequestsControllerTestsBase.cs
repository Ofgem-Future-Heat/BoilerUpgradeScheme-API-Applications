using Moq;
using Ofgem.API.BUS.Applications.Api.Controllers;
using Ofgem.API.BUS.Applications.Core.Interfaces;

namespace Ofgem.API.BUS.Applications.Api.UnitTests.Controllers.ConsentRequestsControllerTests;

/// <summary>
/// Base class for all ConsentRequestsController test classes, with helpers, Mocks, etc
/// </summary>
public class ConsentRequestsControllerTestsBase
{
    protected Mock<IApplicationsService> _mockApplicationsService = new();

    public ConsentRequestsController GenerateSystemUnderTest() => new(
        _mockApplicationsService.Object);
}