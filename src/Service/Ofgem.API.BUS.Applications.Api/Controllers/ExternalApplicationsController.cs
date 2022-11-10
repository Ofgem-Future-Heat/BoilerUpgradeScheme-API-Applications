using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Ofgem.API.BUS.Applications.Api.Extensions;
using Ofgem.API.BUS.Applications.Core.Interfaces;
using Ofgem.API.BUS.Applications.Domain;
using Ofgem.API.BUS.Applications.Domain.Constants;
using Ofgem.API.BUS.Applications.Domain.Entities.CommsObjects;
using Ofgem.API.BUS.Applications.Domain.Entities.Views;
using Ofgem.API.BUS.PropertyConsents.Domain.Models.CommsObjects;
using Ofgem.Lib.BUS.APIClient.Domain.Exceptions;
using Ofgem.Lib.BUS.AuditLogging.Api.Filters;

namespace Ofgem.API.BUS.ExternalApplications.Api.Controllers;

/// <summary>
/// Routes all External Application operations to the Applications Service
/// </summary>
[ApiController]
[Route("[controller]")]
public class ExternalApplicationsController : ControllerBase
{
    private readonly IApplicationsService _applicationsService;
    private readonly IEmailService _emailService;
    private readonly ILogger<ExternalApplicationsController> _logger;

    public ExternalApplicationsController(IApplicationsService applicationsService, IEmailService emailService, ILogger<ExternalApplicationsController> logger)
    {
        _applicationsService = applicationsService ?? throw new ArgumentNullException(nameof(applicationsService));
        _emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
        _logger = logger;
    }

    [HttpGet]
    [Route("{businessAccountId}/Dashboard")]
    [ProducesResponseType(typeof(IEnumerable<ExternalPortalDashboardApplication>), 200)]
    [ProducesResponseType(400)]
    public IActionResult GetApplicationsByBusinessAccountId([FromRoute]Guid businessAccountId, [FromQuery]string? searchBy = null, [FromQuery]Guid[]? statusIds = null, [FromQuery]string? consentState = null)
    {
        try
        {
            var dashboardApplications = _applicationsService.GetExternalDashboardApplicationsByBusinessAccountId(businessAccountId, searchBy, statusIds, consentState);

            return Ok(dashboardApplications);
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Could not get applications for external dashboard");
            return BadRequest();
        }
    }

    [HttpGet]
    [Route("{referenceNumber}")]
    [ProducesResponseType(typeof(Application), 200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> GetApplicationByReferenceNumberAsync([FromRoute]string referenceNumber)
    {
        try
        {
            var application = await _applicationsService.GetApplicationByReferenceNumber(referenceNumber);

            return Ok(application);
        }
        catch (ResourceNotFoundException ex)
        {
            _logger.LogWarning(ex, "Could not find application with reference {ReferenceNumber}", referenceNumber);
            return NotFound();
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Could not get application");
            return BadRequest();
        }
    }

    /// <summary>
    /// Tries to create a new <see cref="Application"/> based on the posted request, via the Applications Service
    /// </summary>
    /// <param name="createApplicationRequest"></param>
    /// <returns>The created <see cref="Application"/></returns>
    [HttpPost("")]
    [ProducesResponseType(typeof(Application), 200)]
    [AuditLogFilterFactory(Message = "Create application")]
    public async Task<IActionResult> CreateApplicationAsync(CreateApplicationRequest createApplicationRequest)
    {
        try
        {
            if (createApplicationRequest is null)
            {
                throw new ArgumentNullException(nameof(createApplicationRequest));
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createApplicationResult = await _applicationsService.CreateApplicationAsync(createApplicationRequest);

            if (createApplicationResult != null)
            {
                var emailResult = await _emailService.SendInstallerPostApplicationEmailAsync(createApplicationResult);

                if (!emailResult.IsSuccess)
                {
                    _logger.LogWarning("Could not send post-application email to installer - {ErrorMessage}", emailResult.ErrorMessage);
                }

                HttpContext.Items.Add(AuditLogAttribute.EntityIdHttpContextKey, createApplicationResult.ID);
                return Ok(createApplicationResult);
            }

            return Problem("Could not create application", statusCode: 500);
        }
        catch (BadRequestException ex)
        {
            return this.AsObjectResult(ex);
        }
    }

    /// <summary>
    /// Check for duplicate application by uprn and bussiness account id.
    /// </summary>
    /// <param name="uprn">address uprn</param>
    /// <param name="businessAccountId">The business ID</param>
    /// <returns>IActionResult</returns>
    [HttpGet("DuplicateApplications/{uprn}/{businessAccountId}")]
    [ProducesResponseType(typeof(bool), 200)]
    public async Task<IActionResult> CheckDuplicateApplicationAsync(string uprn, Guid businessAccountId)
    {
        bool duplicateApplication = false;

        try
        {
            if (string.IsNullOrEmpty(uprn))
            {
                throw new ArgumentNullException(nameof(uprn));
            }

            if (businessAccountId == Guid.Empty)
            {
                throw new BadRequestException(ApplicationsExceptionMessages.NoBusinessAccount);
            }

            duplicateApplication = await _applicationsService.CheckDuplicateApplicationAsync(uprn, businessAccountId);
        }
        catch (BadRequestException ex)
        {
            return this.AsObjectResult(ex);
        }

        return Ok(duplicateApplication);
    }

    /// <summary>
    /// Requests raising of an email to the Property Owner with a token link to the application in the consent portal
    /// </summary>
    /// <param name="applicationId"></param>
    /// <returns></returns>
    [HttpPost("{applicationId}/ConsentEmail")]
    [ProducesResponseType(typeof(SendConsentEmailResult), 200)]
    [AuditLogFilterFactory(Message = "Request consent email")]
    public async Task<IActionResult> RequestConsentEmailAsync(Guid applicationId, RequestConsentEmailRequest requestConsentEmailRequest)
    {
        SendConsentEmailResult? sendConsentEmailResult;
        try
        {
            if (applicationId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(applicationId));
            }

            sendConsentEmailResult = await _applicationsService.SendConsentEmailAsync(applicationId, requestConsentEmailRequest.CreatedByUsername);
        }
        catch (BadRequestException ex)
        {
            return this.AsObjectResult(ex);
        }

        if (sendConsentEmailResult == null || !sendConsentEmailResult.IsSuccess)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        return Ok(sendConsentEmailResult);
    }

    /// <summary>
    /// Adds a voucher to an application
    /// </summary>
    /// <param name="applicationId"></param>
    /// <param name="addVoucherRequest"></param>
    /// <returns></returns>
    [HttpPost("{applicationId}/Vouchers")]
    [ProducesResponseType(typeof(Voucher), 200)]
    [AuditLogFilterFactory(Message = "Add voucher")]
    public async Task<IActionResult> AddVoucherAsync(Guid applicationId, AddVoucherRequest addVoucherRequest)
    {
        try
        {
            if (applicationId != addVoucherRequest.ApplicationID)
            {
                return BadRequest("Application ID in body must match Application ID in route");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(await _applicationsService.AddVoucherAsync(addVoucherRequest));
        }
        catch (BadRequestException ex)
        {
            return this.AsObjectResult(ex);
        }
    }
}
