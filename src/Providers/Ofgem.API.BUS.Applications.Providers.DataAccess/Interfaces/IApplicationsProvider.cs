using Ofgem.API.BUS.Applications.Domain;
using Ofgem.API.BUS.Applications.Domain.Entities;
using Ofgem.API.BUS.Applications.Domain.Entities.Enums;
using Ofgem.API.BUS.Applications.Domain.Entities.Views;
using Ofgem.API.BUS.Applications.Domain.Request;

namespace Ofgem.API.BUS.Applications.Providers.DataAccess.Interfaces;

/// <summary>
/// Interfaces CRUD and helper operations for the Applications DB
/// </summary>
public interface IApplicationsProvider
{
    /// <summary>
    /// Gets an Application with no Included objects, by ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public Task<Application> GetApplicationByIdAsync(Guid id);

    /// <summary>
    /// Gets an Application with no Included objects, by ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns>The Application, or NULL if not found</returns>
    public Task<Application?> GetApplicationOrDefaultByIdAsync(Guid id);

    /// <summary>
    /// Gets an Application with included objects, by application reference number. 
    /// </summary>
    /// <param name="referenceNumber">Application by reference by number.</param>
    /// <returns>The Application, or NULL if not found</returns>
    public Task<Application?> GetApplicationOrDefaultByReferenceNumberAsync(string referenceNumber);

    /// <summary>
    /// Adds a new Application to the DB
    /// </summary>
    /// <param name="application"></param>
    /// <returns>The created <see cref="Application"/></returns>
    public Task<Application> AddApplicationAsync(Application application);

    /// <summary>
    /// Gets an application with its consent-related objects e.g. TechType and InstallationAddress , by ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public Task<Application> GetApplicationWithConsentObjectsByIdAsync(Guid id);

    /// <summary>
    /// Gets an application with its consent-related objects e.g. TechType and InstallationAddress , by ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns>The Application, or NULL if not found</returns>
    public Task<Application?> GetApplicationOrDefaultWithConsentObjectsByIdAsync(Guid id);

    /// <summary>
    /// Adds or updates consent request to the Application with the given ID
    /// </summary>
    /// <param name="applicationId"></param>
    /// <param name="consentRequest"></param>
    /// <returns></returns>
    public Task UpsertConsentRequestAsync(Guid applicationId, ConsentRequest consentRequest);

    /// <summary>
    /// Gets a tech type by ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public Task<TechType> GetTechTypeByIdAsync(Guid id);

    /// <summary>
    /// Gets a tech type by ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns>The Tech Type, or NULL if not found</returns>
    public Task<TechType?> GetTechTypeOrDefaultByIdAsync(Guid id);

    /// <summary>
    /// Gets a Grant by ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public Task<Grant> GetGrantByIdAsync(Guid id);

    /// <summary>
    /// Gets a Grant by ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns>The Grant, or NULL if not found</returns>
    public Task<Grant?> GetGrantOrDefaultByIdAsync(Guid id);

    /// <summary>
    /// Adds a new Voucher to the db
    /// </summary>
    /// <param name="voucher"></param>
    /// <returns>The created Voucher's ID</returns>
    public Task<Voucher> AddVoucherAsync(Voucher voucher);

    /// <summary>
    /// Gets a ConsentRequest by ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public Task<ConsentRequest> GetConsentRequestByIdAsync(Guid id);

    /// <summary>
    /// Gets a ConsentRequest by ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns>The ConsentRequest, or NULL if not found</returns>
    public Task<ConsentRequest?> GetConsentRequestOrDefaultByIdAsync(Guid id);

    /// <summary>
    /// Adds the current date and time to the ConsentReceived field of a consent request
    /// </summary>
    /// <param name="id">The consent request ID.</param>
    /// <param name="updatedByUser">The username of the user updating the consent request.</param>
    /// <returns></returns>
    public Task RegisterConsentReceivedByIdAsync(Guid id, string updatedByUser);

    /// <summary>
    /// Get - Applications with pagination. 
    /// </summary>
    /// <param name="page">Page number.</param>
    /// <param name="pageSize">Page size - number of items per page.</param>
    /// <param name="sortBy">Column to sort by.</param>
    /// <param name="orderByDescending">orderByDescending - True/False - either sort descending or ascending.</param>
    /// <param name="filterApplicationStatusBy">List of application status code selected to filter the data by.</param>
    /// <param name="filterVoucherStatusBy">List of voucher status code selected to filter the data by.</param>
    /// <param name="isBeingAudited">Display applications that are being auditeds.</param>
    /// <param name="searchBy">Search data for matching text value - Postcode of business ID.</param>
    /// <returns>IList of data objects.</returns>
    public Task<PagedResult<ApplicationDashboard>> GetPagedApplications(int page = 1, int pageSize = 20,
        string sortBy = "ApplicationDate", bool orderByDescending = true,
        List<string>? filterApplicationStatusBy = null,
        List<string>? filterVoucherStatusBy = null,
        string? isBeingAudited = null, string? searchBy = null);

    /// <summary>
    /// Gets applications by installation address UPRN.
    /// </summary>
    /// <param name="uprn">A UPRN number.</param>
    /// <returns>A list of <see cref="Application"/>.</returns>
    public Task<List<Application>> GetApplicationsByInstallationAddressUprnAsync(string uprn);

    /// <summary>
    /// Adds a new row to the <see cref="GlobalSetting"/> table and returns the new NextApplicationReferenceNumber
    /// </summary>
    /// <returns>A NextApplicationReferenceNumber to use for Application creation</returns>
    public Task<int> GetNewApplicationNumberAsync();

    /// <summary>
    /// Gets TechTypes
    /// </summary>
    /// <returns>A collection of <see cref="TechType"/> objects</returns>
    Task<IEnumerable<TechType>> GetTechTypeListAsync();

    /// <summary>
    /// Gets Application Sub Statuses and Voucher Sub Statuses list of.
    /// </summary>
    /// <returns>A collection of Application and Voucher statuses.</returns>
    public Task<IEnumerable<ApplicationVoucherSubStatus>> GetApplicationVoucherSubStatusesListAsync();
    public Task<IEnumerable<ApplicationSubStatus>> GetApplicationSubStatusesListAsync();
    public Task<IEnumerable<VoucherSubStatus>> GetVoucherSubStatusesListAsync();

    /// <summary>
    /// UpdateApplication - update the application table in the DB.
    /// </summary>
    /// <param name="application">Application entity containing the fields to update.</param>
    /// <returns>True/False state of success or failure.</returns>
    public Task<bool> UpdateApplicationAsync(Application application);

    /// <summary>
    /// UpdateVocuher - update the voucher table in the DB.
    /// </summary>
    /// <param name="application">Vocuher entity containing the fields to update.</param>
    /// <returns>True/False state of success or failure.</returns>
    public Task<bool> UpdateVoucherAsync(Voucher voucher);

    /// <summary>
    /// Records an application status change. Previous status change histories are updated to show that they have been succeeded.
    /// This method <b>should</b> be called from inside an existing transaction.
    /// </summary>
    /// <param name="applicationId">The ID of the application being updated.</param>
    /// <param name="applicationSubStatusId">The ID of the new application status.</param>
    /// <returns><c>true</c> if any database changes were made. <c>false</c> if no database changes were made.</returns>
    public Task<bool> UpsertApplicationStatusHistory(Guid applicationId, Guid applicationSubStatusId);
    public Task<bool> UpsertVoucherStatusHistory(Guid voucherId, Guid voucherSubStatusId);

    /// <summary>
    /// UpdateApplicationStatusAsync - updates application status.
    /// </summary>
    /// <returns>List of errors, if such a scenario occurs.</returns>
    public Task<List<string>> UpdateApplicationStatusAsync(Guid applicationId, Guid applicationStatusId);

    /// <summary>
    /// UpdateVoucherStatusAsync - updates voucher status.
    /// </summary>
    /// <returns>List of errors, if such a scenario occurs.</returns>
    public Task<List<string>> UpdateVoucherStatusAsync(Guid voucherId, Guid voucherStatusId);

    /// <summary>
    /// UpdateEpcReferenceAsync - updates application EPC ID and deletes the Epc data.
    /// </summary>
    /// <returns>List of errors, if such a scenario occurs.</returns>
    public Task<List<string>> UpdateEpcReferenceAsync(Guid applicationId);

    /// <summary>
    /// Gets all grants from the database
    /// </summary>
    /// <returns>A collection of type <see cref="Grant"/>.</returns>
    Task<IEnumerable<Grant>> GetGrantsListAsync();

    /// <summary>
    /// Gets a grant with a corresponding tech type ID from the database.
    /// </summary>
    /// <param name="techTypeId">The tech type ID.</param>
    /// <returns>A <see cref="Grant"/> with a matching tech type ID.</returns>
    Task<Grant?> GetGrantOrDefaultByTechTypeIdAsync(Guid techTypeId);

    /// <summary>
    /// Stores a users service feedback to the database
    /// </summary>
    /// <param name="applicationId"></param>
    /// <param name="feedbackNarrative"></param>
    /// <param name="surveyOption"></param>
    /// <returns></returns>
    public Task<bool> StoreServiceFeedbackAsync(Guid applicationId, string feedbackNarrative, int surveyOption, string serviceUsed);

    /// <summary>
    /// Gets a list of application data for a business account.
    /// </summary>
    /// <param name="businessAccountId">The business account ID.</param>
    /// <param name="searchBy">Optional search term.</param>
    /// <param name="statusIds">Optional application or voucher status IDs to filter on.</param>
    /// <param name="consentState">Optional consent state filter.</param>
    /// <returns>A collection of <see cref="ExternalPortalDashboardApplication"/> entities.</returns>
    IEnumerable<ExternalPortalDashboardApplication> GetExternalDashboardApplicationsByBusinessAccountId(Guid businessAccountId, string? searchBy, IEnumerable<Guid>? statusIds, ConsentState? consentState);

    /// <summary>
    /// Updates the CurrentContactID in the Application table
    /// </summary>
    /// <param name="oldContactId"></param>
    /// <param name="newContactId"></param>
    /// <returns>True/False state of success or failure.</returns>
    Task<bool> UpdateContactAsync(Guid oldContactId, Guid newContactId);

    /// <summary>
    /// Checks if there are any live applications against an external user.
    /// </summary>
    /// <param name="externalUserId">The External User Id.</param>
    /// <returns>True if there are any live appllications against user.</returns>
    bool HasLiveApplicationsAgainstExternalUserId(Guid externalUserId);
}
