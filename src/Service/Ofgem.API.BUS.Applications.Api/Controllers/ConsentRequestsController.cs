using Microsoft.AspNetCore.Mvc;
using Ofgem.API.BUS.Applications.Api.Extensions;
using Ofgem.API.BUS.Applications.Core.Interfaces;
using Ofgem.API.BUS.Applications.Domain.Entities.CommsObjects;
using Ofgem.Lib.BUS.APIClient.Domain.Exceptions;
using Ofgem.Lib.BUS.AuditLogging.Api.Filters;

namespace Ofgem.API.BUS.Applications.Api.Controllers;

/// <summary>
/// Routes Consent Request oprations to the Applications Service
/// </summary>
[ApiController]
[Route("ConsentRequests")]
public class ConsentRequestsController : ControllerBase
{
    private readonly IApplicationsService _applicationsService;
    public ConsentRequestsController(IApplicationsService applicationsService)
    {
        _applicationsService = applicationsService ?? throw new ArgumentNullException(nameof(applicationsService));
    }

    /// <summary>
    /// Gets a summary of a ConsentRequest's details, if it exists and has not expired
    /// </summary>
    /// <param name="consentRequestId"></param>
    /// <returns></returns>
    [HttpGet("{consentRequestId}")]
    [ProducesResponseType(typeof(GetConsentRequestDetailsResult), 200)]
    public async Task<IActionResult> GetConsentRequestDetailsAsync(Guid consentRequestId)
    {
        GetConsentRequestDetailsResult getConsentRequestDetailsResult;
        try
        {
            getConsentRequestDetailsResult = await _applicationsService.GetConsentRequestDetailsAsync(consentRequestId);
        }
        catch (BadRequestException ex)
        {
            return this.AsObjectResult(ex);
        }

        return Ok(getConsentRequestDetailsResult);
    }

    /// <summary>
    /// Marks the Consent request as approved
    /// </summary>
    /// <param name="consentRequestId"></param>
    /// <returns></returns>
    [HttpPost("{consentRequestId}")]
    [ProducesResponseType(204)]
    [AuditLogFilterFactory(Message = "Consent received")]
    public async Task<IActionResult> RegisterConsentReceived(Guid consentRequestId, RegisterConsentReceivedRequest registerConsentReceivedRequest)
    {
        await _applicationsService.RegisterConsentAsync(consentRequestId, registerConsentReceivedRequest.UpdatedByUsername);
        return NoContent();
    }
}
