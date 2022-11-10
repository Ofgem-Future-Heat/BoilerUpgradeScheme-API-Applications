using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Ofgem.API.BUS.Applications.Api.Controllers;
using Ofgem.API.BUS.Applications.Core.Interfaces;

namespace Ofgem.API.BUS.Applications.Api.UnitTests.Controllers.ApplicationsControllerTests;

/// <summary>
/// Base class for all ApplicationsController test classes, with helpers, Mocks, etc
/// </summary>
public class ApplicationsControllerTestsBase
{
    protected Mock<IApplicationsService> _mockApplicationsService = new();

    public ApplicationsController GenerateSystemUnderTest() => new(
        _mockApplicationsService.Object)
    {
        ControllerContext = new ControllerContext { HttpContext = new DefaultHttpContext() }
    };
}
