using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using Ofgem.API.BUS.Applications.Core.Interfaces;
using Ofgem.API.BUS.ExternalApplications.Api.Controllers;

namespace Ofgem.API.BUS.Applications.Api.UnitTests.Controllers.ExternalApplicationsControllerTests;

/// <summary>
/// Base class for all ExternalApplicationsController test classes, with helpers, Mocks, etc
/// </summary>
public abstract class ExternalApplicationsControllerTestsBase
{
    protected Mock<IApplicationsService> _mockApplicationsService = new();
    protected Mock<IEmailService> _mockEmailService = new();
    protected Mock<ILogger<ExternalApplicationsController>> _mockLogger = new();

    [SetUp]
    public void TestCaseSetUp()
    {
        _mockApplicationsService = new();
        _mockEmailService = new();
        _mockLogger = new();
    }

    public ExternalApplicationsController GenerateSystemUnderTest() => new(
        _mockApplicationsService.Object, _mockEmailService.Object, _mockLogger.Object)
    {
        ControllerContext = new ControllerContext { HttpContext = new DefaultHttpContext() }
    };
}
