using Ofgem.API.BUS.Applications.Domain;
using Ofgem.API.BUS.Applications.Domain.Entities;
using Ofgem.API.BUS.Applications.Domain.Entities.CommsObjects;
using Ofgem.API.BUS.Applications.Domain.Entities.Views;
using Ofgem.API.BUS.Applications.Domain.Request;
using Ofgem.API.BUS.PropertyConsents.Domain.Models.CommsObjects;

namespace Ofgem.API.BUS.Applications.Core.Interfaces;

/// <summary>
/// Interface for Application functions
/// </summary>
public interface IApplicationsService
{
    /// <summary>
    /// Attempts to create a BUS application with the details populated as per the provided request
    /// </summary>
    /// <param name="request"></param>
    /// <returns>The created <see cref="Application"/></returns>
    public Task<Application> CreateApplicationAsync(CreateApplicationRequest request);

    /// <summary>
    ///Check if duplicate application 
    /// </summary>
    /// <param name="uprn"></param>
    /// <param name="businessAccountId"></param>

    /// <returns>The created <see cref="Application"/></returns>
    public Task<bool> CheckDuplicateApplicationAsync(string uprn, Guid businessAccountId);

    /// <summary>
    /// Contacts the OwnerConsent API, requesting an owner consent email be raised, saving consent request data on successful send
    /// </summary>
    /// <param name="applicationId">The ID of the application associated with the consent request.</param>
    /// <param name="createdByUser">The username of the user creating the consent request.</param>
    /// <returns>A <see cref="SendConsentEmailResult"/> containing the status of the email.</returns>
    public Task<SendConsentEmailResult> SendConsentEmailAsync(Guid applicationId, string createdByUser);

    /// <summary>
    /// Check For Active Consent By Application Id.
    /// </summary>
    /// <param name="applicationId">The application ID to search by.</param>
    /// <returns>True/False state.</returns>
    public Task<bool> CheckForActiveConsentByApplicationIdAsync(Guid applicationId);

    /// <summary>
    /// Adds a voucher to the application
    /// </summary>
    /// <param name="addVoucherRequest"></param>
    /// <returns>The ID of the created voucher</returns>
    public Task<Voucher> AddVoucherAsync(AddVoucherRequest addVoucherRequest);

    /// <summary>
    /// Gets the application details associated with a ConsentRequest
    /// </summary>
    /// <param name="consentRequestId"></param>
    /// <returns></returns>
    public Task<GetConsentRequestDetailsResult> GetConsentRequestDetailsAsync(Guid consentRequestId);

    /// <summary>
    /// Adds a timestamp for Consent Received to the ConsentRequest
    /// </summary>
    /// <param name="consentRequestId">The consent request ID.</param>
    /// <param name="updatedByUser">The username of the user updating the consent request.</param>
    /// <returns></returns>
    public Task RegisterConsentAsync(Guid consentRequestId, string updatedByUser);

    /// <summary>
    /// Paginate through the applications.
    /// </summary>
    /// <param name="page">Page number giving the page to start at, default to 1.</param>
    /// <param name="pageSize">Page size number of items per page requested by the calling program.</param>
    /// <param name="sortBy">Column to sort by.</param>
    /// <param name="orderByDescending">orderByDescending - True/False - either sort descending or ascending.</param>
    /// <param name="filterApplicationStatusBy">List of application status code selected to filter the data by.</param>
    /// <param name="filterVoucherStatusBy">List of voucher status code selected to filter the data by.</param>
    /// <param name="isBeingAudited">Display the application that are being audited.</param>
    /// <param name="searchBy">Search data for matching text value - Postcode of business ID.</param>
    /// <returns>Returns an IList of Applications wrapped in a Paged result object.</returns>
    public Task<PagedResult<ApplicationDashboard>> GetPagedApplications(int page = 1, int pageSize = 20,
        string sortBy = "ApplicationDate", bool orderByDescending = true,
        string? filterApplicationStatusBy = "", string? filterVoucherStatusBy = "",
        string? isBeingAudited = null, string? searchBy = null);

    /// <summary>
    /// Returns a collection of applications associated with a UPRN number.
    /// </summary>
    /// <param name="uprn">An address UPRN.</param>
    /// <returns>An IEnumerable of <see cref="Application"/> objects associated with the UPRN.</returns>
    public Task<IEnumerable<Application>> GetApplicationsByUprnAsync(string uprn);

    /// <summary>
    /// Returns a Business Account Email related to the Installer Id passed.
    /// </summary>
    /// <returns></returns>
    Task<string> GetBusinessEmailAddressByInstallerId(Guid installerId);

    /// <summary>
    /// Gets all of the Tech Types
    /// </summary>
    /// <returns>A collection of all available <see cref="TechType"/> objects</returns>
    Task<IEnumerable<TechType>> GetTechTypeListAsync();

    /// <summary>
    /// UpdateApplication - update the application table in the DB.
    /// </summary>
    /// <param name="application">Application entity containing the fields to update.</param>
    /// <returns>True/False state of success or failure.</returns>
    public Task<bool> UpdateApplicationAsync(Application application);

    /// <summary>
    /// UpdateVoucherAsync - update the voucher table in the DB.
    /// </summary>
    /// <param name="application">Voucher entity containing the fields to update.</param>
    /// <returns>True/False state of success or failure.</returns>

    public Task<bool> UpdateVoucherAsync(Voucher voucher);

    /// <summary>
    /// UpdateApplicationStatusAsync - edit of an application saved to the DB.
    /// </summary>
    /// <returns>List of string values errors. If the save/validation fails.</returns>
    public Task<List<string>> UpdateApplicationStatusAsync(Guid applicationId, Guid applicationStatusId);

    /// <summary>
    /// UpdateVocuherStatusAsync - edit of an application saved to the DB.
    /// </summary>
    /// <returns>List of string values errors. If the save/validation fails.</returns>
    public Task<List<string>> UpdateVoucherStatusAsync(Guid voucherId, Guid voucherStatusId);

    /// <summary>
    /// UpdateEpcReferenceAsync - updates application EPC ID and deletes the Epc data.
    /// </summary>
    /// <returns>List of errors, if such a scenario occurs.</returns>
    public Task<List<string>> UpdateEpcReferenceAsync(Guid applicationId);

    /// <summary>
    /// Checks for an application containing an active/valid consent within a collection of applications.
    /// </summary>
    /// <param name="applications">The list of applications to check.</param>
    /// <returns><c>true</c> if any of the applications contain an active/valid consent.</returns>
    bool CheckApplicationsForActiveConsent(IEnumerable<Application> applications);

    /// <summary>
    /// Checks if an installer has already made an application for a particular address.
    /// </summary>
    /// <param name="businessAccountId">The business account ID.</param>
    /// <param name="applications">A collection of other applications to check.</param>
    /// <returns><c>true</c> if duplicate applications exist.</returns>
    bool CheckForDuplicateApplications(Guid businessAccountId, IEnumerable<Application> applications);

    /// <summary>
    /// Gets an individual application by ID.
    /// </summary>
    /// <param name="id">The application ID.</param>
    /// <returns>A matching <see cref="Application"/>.</returns>
    Task<Application> GetApplicationByIdAsync(Guid id);

    /// <summary>
    /// Gets an individual application by reference number.
    /// </summary>
    /// <param name="referenceNumber">The reference number.</param>
    /// <returns>A matching <see cref="Application"/>.</returns>
    Task<Application> GetApplicationByReferenceNumber(string referenceNumber);

    /// <summary>
    /// Gets all of the Application and Voucher statuses.
    /// </summary>
    /// <returns>A collection of all available Application and Voucher statuses.</returns>
    Task<IEnumerable<ApplicationVoucherSubStatus>> GetApplicationVoucherSubStatusesListAsync();
    Task<IEnumerable<ApplicationSubStatus>> GetApplicationSubStatusesListAsync();
    Task<IEnumerable<VoucherSubStatus>> GetVoucherSubStatusesListAsync();

    /// <summary>
    /// Gets all of the Grants
    /// </summary>
    /// <returns>A collection of all available <see cref="Grant"/> objects</returns>
    Task<IEnumerable<Grant>> GetGrantsListAsync();

    /// <summary>
    /// Store service feedback in the database.
    /// </summary>
    /// <param name="serviceFeedback"></param>
    /// <returns></returns>
    public Task<StoreServiceFeedbackResult> StoreServiceFeedbackAsync(StoreServiceFeedbackRequest serviceFeedback);

    /// <summary>
    /// Gets a tech type by ID.
    /// </summary>
    /// <param name="techTypeId">The tech type ID.</param>
    /// <returns>The matching <see cref="TechType"/> or null if the tech type cannot be found.</returns>
    Task<TechType?> GetTechTypeByIdAsync(Guid techTypeId);

    /// <summary>
    /// Gets a list of application data for a business account.
    /// </summary>
    /// <param name="businessAccountId">The business account ID.</param>
    /// <param name="searchBy">Optional search term.</param>
    /// <param name="statusIds">Optional application or voucher status IDs to filter on.</param>
    /// <param name="consentState">Optional consent state filter.</param>
    /// <returns>A collection of <see cref="ExternalPortalDashboardApplication"/> entities.</returns>
    IEnumerable<ExternalPortalDashboardApplication> GetExternalDashboardApplicationsByBusinessAccountId(Guid businessAccountId, string? searchBy, IEnumerable<Guid>? statusIds, string? consentState);

    /// <summary>
    /// UpdateContactAsync - update the current contact ID in application table in the DB.
    /// </summary>
    /// <param name="updateContactRequest">Entity containing the fields to update.</param>
    /// <returns>True/False state of success or failure.</returns>

    public Task<bool> UpdateContactAsync(UpdateContactRequest updateContactRequest);

    /// <summary>
    /// Checks if there are any live applications against an external user.
    /// </summary>
    /// <param name="externalUserId">The External User Id.</param>
    /// <returns>True if there are any live appllications against user.</returns>
    bool HasLiveApplicationsAgainstExternalUserId(Guid externalUserId);
}
