using Ofgem.API.BUS.Applications.Domain.Interfaces;

namespace Ofgem.API.BUS.Applications.Domain;

/// <summary>
/// Model for the Application DB object.
/// </summary>
public class Application : ICreateModify
{
    /// <summary>
    /// unique internal ID of an application
    /// </summary>
    public Guid ID { get; set; }

    /// <summary>
    /// Unique reference number of the Application
    /// </summary>
    public string ReferenceNumber { get; set; } = null!;

    /// <summary>
    /// Date that the Application was commenced
    /// </summary>
    public DateTime? ApplicationDate { get; set; }

    /// <summary>
    /// Date that the Application was submitted by the Installer
    /// </summary>
    public DateTime? SubmissionDate { get; set; }

    public DateTime? ExpiryDate { get; set; }

    /// <summary>
    /// Date the application was rejected, used for reporting purposes
    /// </summary>
    public DateTime? DateRejected { get; set; }

    /// <summary>
    /// Date the application was withdrawn, used for reporting purposes
    /// </summary>
    public DateTime? DateWithdrawn { get; set; }

    /// <summary>
    /// Date that the Installer issued the quote
    /// </summary>
    public DateTime? DateOfQuote { get; set; }

    /// <summary>
    /// reference number of the installer issued quote
    /// </summary>
    public string? QuoteReferenceNumber { get; set; }

    /// <summary>
    /// Amount of the installer issued quote
    /// </summary>
    public decimal? QuoteAmount { get; set; }

    /// <summary>
    /// Flag to indicate that the property is a self build property
    /// </summary>
    public bool? IsSelfBuild { get; set; }

    /// <summary>
    /// Flag to indicate that the installer has declared that the Application meets all the scheme requirements
    /// </summary>
    public bool? InstallerDeclaration { get; set; }

    /// <summary>
    /// Date set by Ops for the installer to reply by
    /// </summary>
    public DateTime? InstallerReplyByDate { get; set; }

    /// <summary>
    /// Calculated attribute used for the Applications dashboard to inform the Ops user as to the current state of the review.
    /// </summary>
    private string? _reviewRecommendation;
    public string? ReviewRecommendation
    {
        get => _reviewRecommendation ?? "None";
        set => _reviewRecommendation = value;
    }

    /// <summary>
    /// Calculated attribute used for the Applications dashboard to inform the Ops user as to the current state of PO consent.
    /// </summary>
    public string ConsentState
    {
        get
        {
            var consentState = "Not issued";

            if (this.ConsentRequests is not null && this.ConsentRequests.Any())
            {
                consentState = "Issued";

                var mostRecent = this.ConsentRequests.OrderBy(x => x.ConsentIssuedDate).First();
                if (mostRecent.ConsentReceivedDate is not null)
                {
                    consentState = "Received";
                }
                else if (mostRecent.ConsentExpiryDate < DateTime.UtcNow)
                {
                    consentState = "Expired";
                }
            }

            return consentState;
        }
    } 

    /// <summary>
    /// ID of the External User that has submitted the Application (used for sending Consent confirmation emails to the Installer)
    /// </summary>
    public Guid SubmitterId { get; set; }

    /// <summary>
    /// A flag to indicate that the Property is exempt from EPC checks
    /// </summary>
    public bool? EpcExemption { get; set; }

    /// <summary>
    /// A flag to record that a claim / payment has been made for the property via another scheme.
    /// </summary>
    public bool? IsPreviousMeasure { get; set; }

    /// <summary>
    /// ID of the installation address
    /// </summary>
    public Guid? InstallationAddressID { get; set; }

    /// <summary>
    /// ID of the installation detail
    /// </summary>
    public Guid? InstallationDetailId { get; set; }

    /// <summary>
    /// Type of fuel that is being replaced
    /// </summary>
    public string? PreviousFuelType { get; set; }

    /// <summary>
    /// Text to describe the type of fuel being replaced 
    /// </summary>
    public string? FuelTypeOther { get; set; }

    /// <summary>
    /// What type of property is the installation address
    /// </summary>
    public string? PropertyType { get; set; }

    /// <summary>
    /// Flag to indicate that an EPC exists for the property
    /// </summary>
    public bool? EpcExists { get; set; }

    /// <summary>
    /// Flag to indicate that the property is exempt from the requirement to install loft and cavity insulation
    /// </summary>
    public bool? IsLoftCavityExempt { get; set; }

    /// <summary>
    /// Flag to indicate whether the property is a new build
    /// </summary>
    public bool? IsNewBuild { get; set; }

    /// <summary>
    /// Flag to indicate whether the Application is being audited
    /// </summary>
    public bool? IsBeingAudited { get; set; }

    /// <summary>
    /// Product amount on the quote
    /// </summary>
    public decimal? QuoteProductPrice { get; set; }

    /// <summary>
    /// Reviewer recommendation to QC
    /// </summary>
    public bool? QCRecommendation { get; set; }

    /// <summary>
    /// QC recommendation to DA
    /// </summary>
    public bool? DARecommendation { get; set; }

    public DateTime? ProperlyMadeDate { get; set; }

    /// <summary>
    /// Flag to indicate that the PO Consent has been issued
    /// </summary>
    public bool? PropertyOwnerConsentIssued { get; set; }

    /// <summary>
    /// ID of the application's current sub-status
    /// </summary>
    public Guid? SubStatusId { get; set; }

    /// <summary>
    /// ID of the EPC for the installation address
    /// </summary>
    public Guid? EpcId { get; set; }

    /// <summary>
    /// ID of the selected product for installation
    /// </summary>
    public Guid? ProductId { get; set; }

    /// <summary>
    /// ID of the tech type selected for the installation address
    /// </summary>
    public Guid? TechTypeId { get; set; }

    /// <summary>
    /// ID of the property owner details
    /// </summary>
    public Guid? PropertyOwnerDetailId { get; set; }

    /// <summary>
    /// ID of the External User that will get the Application emails (used for sending Consent confirmation emails to the Installer)
    /// </summary>
    public Guid? CurrentContactId { get; set; }

    /// <summary>
    /// Date and time the record was created
    /// </summary>
    public DateTime CreatedDate { get; set; }

    /// <summary>
    /// Email address of the user who created the record
    /// </summary>
    public string CreatedBy { get; set; } = null!;

    /// <summary>
    /// DateAndTime the record was last updated
    /// </summary>
    public DateTime? LastUpdatedDate { get; set; }

    /// <summary>
    /// Email address of the user who last updated the record
    /// </summary>
    public string? LastUpdatedBy { get; set; }

    public virtual Epc? Epc { get; set; }
    public virtual InstallationAddress? InstallationAddress { get; set; } = null!;
    public virtual PropertyOwnerDetail? PropertyOwnerDetail { get; set; }
    public virtual TechType? TechType { get; set; }
    public virtual List<ConsentRequest> ConsentRequests { get; set; } = new List<ConsentRequest>();
    public virtual Voucher? Voucher { get; set; }
    public virtual ApplicationSubStatus? SubStatus { get; set; } = null!;
    public virtual List<ApplicationStatusHistory> ApplicationStatusHistories { get; set; } = new List<ApplicationStatusHistory>();
    public virtual Product? Product { get; set; }
    public virtual InstallationDetail? InstallationDetail { get; set; }

    /// <summary>
    /// ID of the installer's business account
    /// </summary>
    public Guid BusinessAccountId { get; set; }

}
