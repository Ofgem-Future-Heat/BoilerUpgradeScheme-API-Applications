using FluentAssertions;
using Moq;
using NUnit.Framework;
using Ofgem.API.BUS.Applications.Core.Interfaces;
using Ofgem.API.BUS.Applications.Domain;
using Ofgem.API.BUS.Applications.Domain.Entities.CommsObjects;
using Ofgem.API.BUS.Applications.Providers.DataAccess.Interfaces;
using Ofgem.Lib.BUS.APIClient.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ofgem.API.BUS.Applications.Core.UnitTests.ApplicationsServiceTests;

/// <summary>
/// Tests for the ApplicationsService class' implementation of IApplicationsService.CreateApplicationAsync
/// </summary>
public class StoreFeedbackAsyncTests : ApplicationsServiceTestsBase
{
    [Test]
    public async Task StoreFeedbackAsyncTests_Should_Return_False_When_Request_Is_Null()
    {
        // arrange
        var systemUnderTest = GenerateSystemUnderTest();

        // act
        var result = await systemUnderTest.StoreServiceFeedbackAsync(new StoreServiceFeedbackRequest());

        // assert
       result.Should().BeEquivalentTo(new StoreServiceFeedbackResult { IsSuccess = false});
    }

}
