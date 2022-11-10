using FluentAssertions;
using Moq;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace Ofgem.API.BUS.Applications.Core.UnitTests.ApplicationsServiceTests;

/// <summary>
/// Tests for the ApplicationsService class' implementation of IApplicationsService.GetApplicationsByUprnAsync
/// </summary>
public class GetApplicationsByUprnAsyncTests : ApplicationsServiceTestsBase
{
    [Test]
    [Ignore("The is not the current behaviour")]
    public async Task GetApplicationsByUprnAsync_Throws_ArgumentNullException_When_Uprn_Is_Null()
    {
        // arrange
        var systemUnderTest = GenerateSystemUnderTest();

        // act
        Func<Task> act = () => _ = systemUnderTest.GetApplicationsByUprnAsync(null!);

        // assert
        await act
          .Should().ThrowExactlyAsync<ArgumentNullException>()
          .WithParameterName("uprn")
          .WithMessage("Value cannot be null. (Parameter 'uprn')");
    }

    [Test]
    [Ignore("The is not the current behaviour")]
    [TestCase("")]
    [TestCase("ABCABCABCABC")]
    [TestCase("1")]
    [TestCase("22")]
    [TestCase("333")]
    [TestCase("1010101010")]
    [TestCase("11111111111")]
    [TestCase("1111111111e1")]
    [TestCase("11111 111111")]
    [TestCase("11111.111111")]
    public async Task GetApplicationsByUprnAsync_Throws_ArgumentException_When_Uprn_Is_Not_Twelve_Digits(string uprn)
    {
        // arrange
        var systemUnderTest = GenerateSystemUnderTest();

        // act
        Func<Task> act = () => _ = systemUnderTest.GetApplicationsByUprnAsync(uprn);

        // assert
        await act
          .Should().ThrowExactlyAsync<ArgumentException>()
          .WithParameterName("uprn")
          .WithMessage("Value must be 12 digits. (Parameter 'uprn')");
    }

    [Test]
    [TestCase("")]
    [TestCase(" ")]
    [TestCase(null)]
    [TestCase("1010101010")]
    [TestCase("CanBeCharacters")]
    [TestCase("121212121212")]
    [TestCase("121212121212121212121212")]
    public async Task GetApplicationsByUprnAsync_Is_Simple_Passthrough_To_ProviderMethod(string uprn)
    {
        // arrange
        var systemUnderTest = GenerateSystemUnderTest();

        // act
        _ = await systemUnderTest.GetApplicationsByUprnAsync(uprn);

        // assert
        _mockApplicationsProvider.Verify(m => m.GetApplicationsByInstallationAddressUprnAsync(uprn), Times.Once);
    }
}
