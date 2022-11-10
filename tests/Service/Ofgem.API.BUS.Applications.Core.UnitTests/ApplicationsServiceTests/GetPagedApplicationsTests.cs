using FluentAssertions;
using Moq;
using NUnit.Framework;
using Ofgem.API.BUS.Applications.Domain;
using Ofgem.API.BUS.Applications.Domain.Request;
using Ofgem.API.BUS.Applications.Providers.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ofgem.API.BUS.Applications.Core.UnitTests.ApplicationsServiceTests;

/// <summary>
/// Tests for the ApplicationsService class' implementation of IApplicationsService.GetPagedApplications
/// </summary>
public class GetPagedApplicationsTests : ApplicationsServiceTestsBase
{
    [Test]
    public async Task GetPagedApplications_With_Default_Parameters()
    {
        //Arrange
        var mockOfData = new Mock<PagedResult<ApplicationDashboard>>();
        Add_GetPagedApplications_Return_Collection(ref _mockApplicationsProvider, mockOfData.Object);

        var systemUnderTest = GenerateSystemUnderTest();

        _mockApplicationsProvider
            .Setup(m => m.GetPagedApplications(1, 20, "ApplicationDate", true, null, null,"0", null))
            .ReturnsAsync(mockOfData.Object);

        //Act
        PagedResult<ApplicationDashboard> actual = await systemUnderTest.GetPagedApplications(1, 20, "ApplicationDate", true).ConfigureAwait(false);

        //Assert
        actual.Results.Count.Should().Be(0);
    }

    [Test]
    public async Task GetPagedApplications_With_Application_Statuses()
    {
        //Arrange
        var mockOfData = new Mock<PagedResult<ApplicationDashboard>>();
        Add_GetPagedApplications_Return_Collection(ref _mockApplicationsProvider, mockOfData.Object);

        var mockOfApplicationStatus = new Mock<List<string>>();

        var systemUnderTest = GenerateSystemUnderTest();

        _mockApplicationsProvider
            .Setup(m => m.GetPagedApplications(1, 20, "ApplicationDate", true, mockOfApplicationStatus.Object, null,"0", null))
            .ReturnsAsync(mockOfData.Object);

        //Act
        PagedResult<ApplicationDashboard> actual = await systemUnderTest.GetPagedApplications(1, 20, "ApplicationDate", true, "SUB,WITH", null, null).ConfigureAwait(false);

        //Assert
        actual.Results.Count.Should().Be(0);
    }

    [Test]
    public async Task GetPagedApplications_With_Voucher_Statuses()
    {
        //Arrange
        var mockOfData = new Mock<PagedResult<ApplicationDashboard>>();
        Add_GetPagedApplications_Return_Collection(ref _mockApplicationsProvider, mockOfData.Object);

        var mockOfVoucherStatus = new Mock<List<string>>();

        var systemUnderTest = GenerateSystemUnderTest();

        _mockApplicationsProvider
            .Setup(m => m.GetPagedApplications(1, 20, "ApplicationDate", true, null, mockOfVoucherStatus.Object, "0",null))
            .ReturnsAsync(mockOfData.Object);

        //Act
        PagedResult<ApplicationDashboard> actual = await systemUnderTest.GetPagedApplications(1, 20, "ApplicationDate", true, null, "SUB,WITHIN", null).ConfigureAwait(false);

        //Assert
        actual.Results.Count.Should().Be(0);
    }

    [Test]
    public async Task GetPagedApplications_With_Application_And_Voucher_Statuses()
    {
        //Arrange
        var mockOfData = new Mock<PagedResult<ApplicationDashboard>>();
        Add_GetPagedApplications_Return_Collection(ref _mockApplicationsProvider, mockOfData.Object);

        var mockOfApplicationStatus = new Mock<List<string>>();
        var mockOfVoucherStatus = new Mock<List<string>>();

        var systemUnderTest = GenerateSystemUnderTest();

        _mockApplicationsProvider
            .Setup(m => m.GetPagedApplications(1, 20, "ApplicationDate", true, mockOfApplicationStatus.Object, mockOfVoucherStatus.Object,"0", null))
            .ReturnsAsync(mockOfData.Object);

        //Act
        PagedResult<ApplicationDashboard> actual = await systemUnderTest.GetPagedApplications(1, 20, "ApplicationDate", true, "SUB,WITH", "SUB,WITHIN", null).ConfigureAwait(false);

        //Assert
        actual.Results.Count.Should().Be(0);
    }

    [Test]
    public async Task GetPagedApplications_With_Search_String()
    {
        //Arrange
        var mockOfData = new Mock<PagedResult<ApplicationDashboard>>();
        Add_GetPagedApplications_Return_Collection(ref _mockApplicationsProvider, mockOfData.Object);

        var systemUnderTest = GenerateSystemUnderTest();

        _mockApplicationsProvider
            .Setup(m => m.GetPagedApplications(1, 20, "ApplicationDate", true, null, null, It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(mockOfData.Object);

        //Act
        PagedResult<ApplicationDashboard> actual = await systemUnderTest.GetPagedApplications(1, 20, "ApplicationDate", true, null, null,"0", "LU6 1AD").ConfigureAwait(false);

        //Assert
        actual.Results.Count.Should().Be(0);
    }

    [Test]
    public async Task GetPagedApplications_With_Invalid_Search_String()
    {
        //Arrange
        var mockOfData = new Mock<PagedResult<ApplicationDashboard>>();
        Add_GetPagedApplications_Return_Collection(ref _mockApplicationsProvider, mockOfData.Object);

        var systemUnderTest = GenerateSystemUnderTest();

        _mockApplicationsProvider
            .Setup(m => m.GetPagedApplications(1, 20, "ApplicationDate", true, null, null, It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(mockOfData.Object);

        //Act
        PagedResult<ApplicationDashboard> actual = await systemUnderTest.GetPagedApplications(1, 20, "ApplicationDate", true, null, null,"0", "LU").ConfigureAwait(false);

        //Assert
        actual.Results.Count.Should().Be(0);
    }

    [Test]
    public async Task GetPagedApplications_With_Application_And_Voucher_Statuses_And_With_Invalid_Search_String()
    {
        //Arrange
        var mockOfData = new Mock<PagedResult<ApplicationDashboard>>();
        Add_GetPagedApplications_Return_Collection(ref _mockApplicationsProvider, mockOfData.Object);

        var mockOfApplicationStatus = new Mock<List<string>>();
        var mockOfVoucherStatus = new Mock<List<string>>();

        var systemUnderTest = GenerateSystemUnderTest();

        _mockApplicationsProvider
            .Setup(m => m.GetPagedApplications(1, 20, "ApplicationDate", true, mockOfApplicationStatus.Object, mockOfVoucherStatus.Object,"0", null))
            .ReturnsAsync(mockOfData.Object);

        //Act
        PagedResult<ApplicationDashboard> actual = await systemUnderTest.GetPagedApplications(1, 20, "ApplicationDate", true, "SUB,WITH", "SUB,WITHIN", "XX").ConfigureAwait(false);

        //Assert
        actual.Results.Count.Should().Be(0);
    }

    [Test]
    public async Task GetPagedApplications_Return_Collection_Of_PagedResults()
    {
        //Arrange
        var mockOfData = new Mock<PagedResult<ApplicationDashboard>>();
        _mockApplicationsProvider = new Mock<IApplicationsProvider>(MockBehavior.Strict);
        Add_GetPagedApplications_Return_Collection(ref _mockApplicationsProvider);

        var systemUnderTest = GenerateSystemUnderTest();

        _mockApplicationsProvider
            .Setup(m => m.GetPagedApplications(1, 20, "ApplicationDate", true, null, null,"0", ""))
            .ReturnsAsync(new PagedResult<ApplicationDashboard>());

        //Act
        PagedResult<ApplicationDashboard> actual = await systemUnderTest.GetPagedApplications(1, 20, "ApplicationDate", true).ConfigureAwait(false);

        //Assert
        actual.Results.Should().NotBeNull();
    }
}