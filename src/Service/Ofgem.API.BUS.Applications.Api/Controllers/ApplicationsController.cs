using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ofgem.API.BUS.Applications.Api.Extensions;
using Ofgem.API.BUS.Applications.Core.Interfaces;
using Ofgem.API.BUS.Applications.Domain;
using Ofgem.API.BUS.Applications.Domain.Constants;
using Ofgem.API.BUS.Applications.Domain.Entities.CommsObjects;
using Ofgem.API.BUS.PropertyConsents.Domain.Models.CommsObjects;
using Ofgem.Lib.BUS.APIClient.Domain.Exceptions;
using Ofgem.Lib.BUS.AuditLogging.Api.Filters;

namespace Ofgem.API.BUS.Applications.Api.Controllers;

/// <summary>
/// Routes all Application operations to the Applications Service
/// </summary>
[ApiController]
[Route("Applications")]
public class ApplicationsController : ControllerBase
{
    private readonly IApplicationsService _applicationsService;

    public ApplicationsController(IApplicationsService applicationsService)
    {
        _applicationsService = applicationsService ?? throw new ArgumentNullException(nameof(applicationsService));
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
    /// Check for active consent by application Id.
    /// </summary>
    /// <param name="applicationId">The application ID to validate consent by.</param>
    /// <returns>IActionResult</returns>
    [HttpGet("CheckForActiveConsentByApplicationId/{applicationId}")]
    [ProducesResponseType(typeof(SendConsentEmailResult), 200)]
    public async Task<IActionResult> CheckForActiveConsentByApplicationId(Guid applicationId)
    {
        bool activeConsent = false;

        try
        {
            if (applicationId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(applicationId));
            }
            activeConsent = await _applicationsService.CheckForActiveConsentByApplicationIdAsync(applicationId);
        }
        catch (BadRequestException ex)
        {
            return this.AsObjectResult(ex);
        }

        return Ok(activeConsent);
    }

    /// <summary>
    /// Check for duplicate application by uprn and bussiness account id.
    /// </summary>
    /// <param name="uprn">address uprn</param>
    /// <param name="businessAccountId">The business ID</param>
    /// <returns>IActionResult</returns>
    [HttpGet("DuplicateApplications/{uprn}/{businessAccountId}")]
    [ProducesResponseType(typeof(bool), 200)]
    public async Task<IActionResult> CheckDuplicateApplication(string uprn, Guid businessAccountId)
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
    /// Adds a voucher to an application
    /// </summary>
    /// <param name="applicationId"></param>
    /// <param name="addVoucherRequest"></param>
    /// <returns></returns>
    [HttpPost("{applicationId}/Vouchers")]
    [ProducesResponseType(typeof(Guid), 200)]
    [AuditLogFilterFactory(Message = "Add voucher")]
    public async Task<IActionResult> AddVoucher(Guid applicationId, AddVoucherRequest addVoucherRequest)
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

    /// <summary>
    /// Update an application status.
    /// </summary>
    [HttpPost("UpdateApplicationStatusAsync/{applicationId}/{applicationStatusId}")]
    [ProducesResponseType(typeof(List<string>), 200)]
    [AuditLogFilterFactory(Message = "Update application status")]
    public async Task<IActionResult> UpdateApplicationStatusAsync(Guid? applicationId, Guid? applicationStatusId)
    {
        try
        {
            if (applicationId is null)
            {
                throw new ArgumentNullException(nameof(applicationId));
            }

            if (applicationStatusId is null)
            {
                throw new ArgumentNullException(nameof(applicationStatusId));
            }

            List<string> listOfErrors = await _applicationsService.UpdateApplicationStatusAsync((Guid)applicationId, (Guid)applicationStatusId);
            return !listOfErrors.Any() ? Ok() : this.AsObjectResult(listOfErrors);
        }
        catch (BadRequestException ex)
        {
            return this.AsObjectResult(ex);
        }
    }

    /// <summary>
    /// Update an vocuher status.
    /// </summary>
    [HttpPost("UpdateEpcReferenceAsync/{applicationId}")]
    [ProducesResponseType(typeof(List<string>), 200)]
    [AuditLogFilterFactory(Message = "Update EPC reference")]
    public async Task<IActionResult> UpdateEpcReferenceAsync(Guid? applicationId)
    {
        try
        {
            if (applicationId is null)
            {
                throw new ArgumentNullException(nameof(applicationId));
            }

            List<string> listOfErrors = await _applicationsService.UpdateEpcReferenceAsync((Guid)applicationId);
            return !listOfErrors.Any() ? Ok() : this.AsObjectResult(listOfErrors);
        }
        catch (BadRequestException ex)
        {
            return this.AsObjectResult(ex);
        }
    }

    /// <summary>
    /// Update an voucher status.
    /// </summary>
    /// <param name="voucherId"> The ID of the Voucher record.</param>
    /// <param name="voucherStatusId">The ID of the Voucher Sub Status record.</param>
    /// <returns>List of strings with error messages if they have occurred.</returns>
    [HttpPost("UpdateVoucherStatusAsync/{voucherId}/{voucherStatusId}")]
    [ProducesResponseType(typeof(List<string>), 200)]
    [AuditLogFilterFactory(Message = "Update voucher status")]
    public async Task<IActionResult> UpdateVoucherStatusAsync(Guid? voucherId, Guid? voucherStatusId)
    {
        try
        {
            if (voucherId is null)
            {
                throw new ArgumentNullException(nameof(voucherId));
            }

            if (voucherStatusId is null)
            {
                throw new ArgumentNullException(nameof(voucherStatusId));
            }

            List<string> listOfErrors = await _applicationsService.UpdateVoucherStatusAsync((Guid)voucherId, (Guid)voucherStatusId);
            return !listOfErrors.Any() ? Ok() : this.AsObjectResult(listOfErrors);
        }
        catch (BadRequestException ex)
        {
            return this.AsObjectResult(ex);
        }
    }

    /// <summary>
    /// Gets - Request of paginated applications.
    /// </summary>
    /// <param name="page">Page number.</param>
    /// <param name="pageSize">Page size - number of items per page.</param>
    /// <param name="sortBy">Column to sort by.</param>
    /// <param name="orderByDescending">orderByDescending - True/False - either sort descending or ascending.</param>
    /// <param name="filterApplicationStatusBy">List of application status code selected to filter the data by.</param>
    /// <param name="filterVoucherStatusBy">List of voucher status code selected to filter the data by.</param>
    /// <param name="isBeingAudited">Display the application that are being audited.</param>
    /// <param name="searchBy">Search data for matching text value - Postcode of business ID.</param>
    /// <returns>IList of data objects.</returns>
    [Route("{page}/{pageSize}/{sortBy}/{orderByDescending}/{filterApplicationStatusBy}/{filterVoucherStatusBy}/{isBeingAudited}/{searchBy?}")]
    [HttpGet]
    public async Task<IActionResult> GetPagedApplications(int page = 1, int pageSize = 20,
        string sortBy = "ApplicationDate", bool orderByDescending = true,
        string? filterApplicationStatusBy = "", string? filterVoucherStatusBy = "",
        string? isBeingAudited = null, string? searchBy = null)
    {
        try
        {
            return Ok(await _applicationsService.GetPagedApplications(page, pageSize, sortBy, orderByDescending,
                    filterApplicationStatusBy, filterVoucherStatusBy, isBeingAudited, searchBy));

        }
        catch (BadRequestException ex)
        {
            return this.AsObjectResult(ex);
        }
    }

    /// <summary>
    /// Gets a list of applications associated with a UPRN.
    /// </summary>
    /// <param name="uprn">An address UPRN.</param>
    /// <returns>A list of applications associated with the UPRN.</returns>
    [Route("{uprn}")]
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<Application>), 200)]
    public async Task<IActionResult> GetApplicationsByApplicationAddressUprn(string uprn)
    {
        try
        {
            if (string.IsNullOrEmpty(uprn))
            {
                throw new ArgumentNullException(nameof(uprn));
            }
            return Ok(await _applicationsService.GetApplicationsByUprnAsync(uprn));
        }
        catch (BadRequestException ex)
        {
            return this.AsObjectResult(ex);
        }
    }

    /// <summary>
    /// Returns the primary email address for the installer id given.
    /// </summary>
    /// <param name="installerId">The ID (GUID) of the installer.</param>
    /// <returns>String of the email address.</returns>
    [Route("GetBusinessAccountEmail/{installerId}")]
    [HttpGet]
    [ProducesResponseType(typeof(string), 200)]
    public async Task<IActionResult> GetBusinessAccountEmailByInstallerId(Guid installerId)
    {
        try
        {
            if (installerId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(installerId));
            }

            return Ok(await _applicationsService.GetBusinessEmailAddressByInstallerId(installerId));
        }
        catch (BadRequestException ex)
        {
            return this.AsObjectResult(ex);
        }
    }

    /// <summary>
    /// UpdateApplication - update application details. 
    /// </summary>
    /// <param name="application">Application entity containing the fields to update.</param>
    /// <returns>Ok - true/Error-false.</returns>
    [HttpPost("UpdateApplication")]
    [ProducesResponseType(typeof(bool), 200)]
    [AuditLogFilterFactory(Message = "Update application")]
    public async Task<IActionResult> UpdateApplication([FromBody] Application application)
    {
        try
        {
            if (application == null)
            {
                throw new ArgumentNullException(nameof(application));
            }

            return Ok(await _applicationsService.UpdateApplicationAsync(application));
        }
        catch (BadRequestException ex)
        {
            return this.AsObjectResult(ex);
        }
    }

    /// <summary>
    /// UpdateVoucher - update voucher details. 
    /// </summary>
    /// <param name="voucher">Voucher entity containing the fields to update.</param>
    /// <returns>Ok - true/Error-false.</returns>
    [HttpPost("UpdateVoucher")]
    [ProducesResponseType(typeof(bool), 200)]
    [AuditLogFilterFactory(Message = "Update voucher")]
    public async Task<IActionResult> UpdateVoucher([FromBody] Voucher voucher)
    {
        try
        {
            if (voucher == null)
            {
                throw new ArgumentNullException(nameof(voucher));
            }

            await _applicationsService.UpdateVoucherAsync(voucher);

            return Ok();
        }
        catch (BadRequestException ex)
        {
            return this.AsObjectResult(ex);
        }
    }

    /// <summary>
    /// Retrieves an individual application by ID.
    /// </summary>
    /// <param name="applicationId">The application ID.</param>
    /// <returns>The application details.</returns>
    [HttpGet("{applicationId}/GetFullApplication")]
    [ProducesResponseType(typeof(Application), 200)]
    public async Task<IActionResult> GetApplicationByIdAsync(Guid applicationId)
    {
        try
        {
            if (applicationId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(applicationId));
            }

            return Ok(await _applicationsService.GetApplicationByIdAsync(applicationId));
        }
        catch (BadRequestException ex)
        {
            return this.AsObjectResult(ex);
        }
    }

    /// <summary>
    /// Retrieves an individual application by reference number.
    /// </summary>
    /// <param name="referenceNumber">The reference number.</param>
    /// <returns>The application details.</returns>
    [HttpGet("{referenceNumber}/GetApplicationByReferenceNumber")]
    [ProducesResponseType(typeof(Application), 200)]
    public async Task<IActionResult> GetApplicationByReferenceNumberAsync(string referenceNumber)
    {
        try
        {
            if (string.IsNullOrEmpty(referenceNumber))
            {
                throw new ArgumentNullException(nameof(referenceNumber));
            }

            var application = await _applicationsService.GetApplicationByReferenceNumber(referenceNumber);

            return Ok(application);
        }
        catch (BadRequestException ex)
        {
            return this.AsObjectResult(ex);
        }
    }

    /// <summary>
    /// Retrieves all the application statuses.
    /// </summary>
    /// <returns>List of Application sub statuses.</returns>
    [HttpGet("GetApplicationSubStatuses")]
    [ProducesResponseType(typeof(ApplicationSubStatus), 200)]
    public async Task<IActionResult> GetApplicationSubStatuses()
    {
        try
        {
            return Ok(await _applicationsService.GetApplicationSubStatusesListAsync());
        }
        catch (BadRequestException ex)
        {
            return this.AsObjectResult(ex);
        }
    }

    /// <summary>
    /// Retrieves all the application and voucher statuses.
    /// </summary>
    /// <returns>List of Application & Voucher Sub Statuses.</returns>
    [HttpGet("GetApplicationVoucherSubStatuses")]
    [ProducesResponseType(typeof(Application), 200)]
    public async Task<IActionResult> GetApplicationVoucherSubStatuses()
    {
        try
        {
            return Ok(await _applicationsService.GetApplicationVoucherSubStatusesListAsync());
        }
        catch (BadRequestException ex)
        {
            return this.AsObjectResult(ex);
        }
    }

    /// <summary>
    /// Retrieves all the voucher statuses.
    /// </summary>
    /// <returns>List of Voucher Sub status.</returns>
    [HttpGet("GetVoucherSubStatuses")]
    [ProducesResponseType(typeof(VoucherSubStatus), 200)]
    public async Task<IActionResult> GetVoucherStatuses()
    {
        try
        {
            return Ok(await _applicationsService.GetVoucherSubStatusesListAsync());
        }
        catch (BadRequestException ex)
        {
            return this.AsObjectResult(ex);
        }
    }

    /// <summary>
    /// Receives feedback from a given service to be stored onto the database.
    /// </summary>
    /// <returns>StoreServiceFeedbackResult notifying whether the operation was successful .</returns>
    [HttpPost("StoreFeedback")]
    [ProducesResponseType(typeof(StoreServiceFeedbackResult), 200)]
    [AuditLogFilterFactory(Message = "Store service feedback from user")]
    public async Task<IActionResult> StoreServiceFeedBack([FromBody] StoreServiceFeedbackRequest feedbackToBeStored)
    {
        try
        {
            // TODO: "ServiceUsed" variable arrives as null for some reason?
            var response = await _applicationsService.StoreServiceFeedbackAsync(feedbackToBeStored);

            if (response.IsSuccess == false)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
        catch (BadRequestException ex)
        {
            return this.AsObjectResult(ex);
        }
    }

    /// <summary>
    /// UpdateContact - update or add CurrentContactId. 
    /// </summary>
    /// <param name="updateContactRequest"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    [HttpPost("UpdateContact")]
    [ProducesResponseType(typeof(bool), 200)]
    [AuditLogFilterFactory(Message = "Update contact")]
    public async Task<IActionResult> UpdateContact([FromBody] UpdateContactRequest updateContactRequest)
    {
        try
        {
            if (updateContactRequest == null)
            {
                throw new ArgumentNullException(nameof(updateContactRequest));
            }

            var result = await _applicationsService.UpdateContactAsync(updateContactRequest);

            return Ok(result);
        }
        catch (BadRequestException ex)
        {
            return this.AsObjectResult(ex);
        }
    }


    /// <summary>
    /// Checks if there are any live applications against an external user.
    /// </summary>
    /// <param name="externalUserId">The External User Id.</param>
    /// <returns>True if there are any appllications against user.</returns>
    [HttpGet("{externalUserId}/HasLiveApplicationsAgainstExternalUserId")]
    [ProducesResponseType(typeof(Application), 200)]
    public IActionResult HasLiveApplicationsAgainstExternalUserId(Guid externalUserId)
    {
        try
        {
            if (externalUserId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(externalUserId));
            }

            var applications = _applicationsService.HasLiveApplicationsAgainstExternalUserId(externalUserId);

            return Ok(applications);
        }
        catch (BadRequestException ex)
        {
            return this.AsObjectResult(ex);
        }
    }
}
