using Moq;
using NUnit.Framework;
using Ofgem.API.BUS.PropertyConsents.Client.Interfaces;

namespace Ofgem.API.BUS.Applications.Core.UnitTests.PropertyConsentServiceTests;

/// <summary>
/// Base class for all PropertyConsentService test classes, with helpers, Mocks, etc
/// </summary>
public class PropertyConsentServiceTestsBase
{
    protected Mock<IPropertyConsentAPIClient> _mockPropertyConsentAPIClient = new();

    [SetUp]
    public void Setup()
    {
        _mockPropertyConsentAPIClient = new();
    }

    protected PropertyConsentService GenerateSystemUnderTest() => new(
        _mockPropertyConsentAPIClient.Object);
}
