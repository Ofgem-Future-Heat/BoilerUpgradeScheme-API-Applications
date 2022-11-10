namespace Ofgem.API.BUS.Applications.Domain.Entities.CommsObjects;

/// <summary>
/// Dto containing the data required to add a new Application
/// </summary>
public class CreateApplicationRequest
{

    #region RequiredForBaseSave

    public Guid BusinessAccountID { get; set; }
    public DateTime ApplicationDate { get; set; } = DateTime.UtcNow.Date;

    public bool? InstallationAddressManualEntry { get; set; }
    public CreateApplicationRequestInstallationAddress InstallationAddress { get; set; } = null!;
    public Guid InstallerUserAccountId { get; set; }

    /// <summary>
    /// The logon name (email address) of the user who created the request
    /// </summary>
    public string CreatedBy { get; set; } = null!;
    #endregion

    #region Not required for base save
    public bool? IsBeingAudited { get; set; }
    public string? PreviousFuelType { get; set; }
    public string? FuelTypeOther { get; set; }
    public string? RuralStatus { get; set; }
    public bool? IsGasGrid { get; set; }
    public bool? EpcExists { get; set; }
    public string? EpcReferenceNumber { get; set; }
    public string? PropertyType { get; set; }
    public bool? IsLoftCavityExempt { get; set; }
    public bool? IsNewBuild { get; set; }
    public bool? IsSelfBuild { get; set; }
    public bool? IsAssistedDigital { get; set; }
    public bool? IsWelshTranslation { get; set; }
    public DateTime? DateOfQuote { get; set; }
    public string? QuoteReference { get; set; }
    public decimal? TechnologyCost { get; set; }
    public Guid? TechTypeID { get; set; }
    public CreateApplicationRequestPropertyOwnerDetail? PropertyOwnerDetail { get; set; }
    public decimal? QuoteAmount { get; set; }

    #endregion
}
