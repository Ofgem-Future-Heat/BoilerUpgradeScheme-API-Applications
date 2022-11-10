namespace Ofgem.API.BUS.Applications.Domain;

/// <summary>
/// Model for the InstallationDetail DB object.
/// </summary>
public class InstallationDetail
{
    /// <summary>
    /// Unique internal ID.
    /// </summary>
    public Guid Id { get; set; }

    public DateTime? CommissioningDate { get; set; }

    public string? MCSProductCode { get; set; }

    public int? MCSProductId { get; set; }

    /// <summary>
    /// SCOP value of the installed product
    /// </summary>
    public string? SCOP { get; set; }

    /// <summary>
    /// Flow Temperature of the installed product
    /// </summary>
    public int? FlowTemperature { get; set; }

    /// <summary>
    /// MCS MID db ID of the 'System Designed To Provide' description.
    /// </summary>
    public string? SystemDesignedToProvideId { get; set; }

    /// <summary>
    /// MCS MID description of the 'System Designed To Provide'.
    /// </summary>
    public string? SystemDesignedToProvideDescription { get; set; }

    /// <summary>
    /// MCS MID db ID of the 'Alternative Heating System' description.
    /// </summary>
    public string? AlternativeHeatingSystemId { get; set; }

    /// <summary>
    /// MCS MID description of the 'Alternative Heating System' description.
    /// </summary>
    public string? AlternativeHeatingSystemDescription { get; set; }

    /// <summary>
    /// MCS MID db ID of the 'Alternative Heating System Fuel' description.
    /// </summary>
    public int AlternativeHeatingSystemFuelId { get; set; }

    /// <summary>
    /// MCS MID description of the 'Alternative Heating System Fuel' description.
    /// </summary>
    public string? AlternativeHeatingFuelDescription { get; set; }

    /// <summary>
    /// The Manufacturer name of the product
    /// </summary>
    public string? ProductManufacturer { get; set; }

    /// <summary>
    /// The Product name of the Installed solution
    /// </summary>
    public string? MCSCertifiedProductName { get; set; }

    /// <summary>
    /// The capacity of the Installed solution
    /// </summary>
    public string? Capacity { get; set; }

    /// <summary>
    /// The total installation cost of the Installed solution
    /// </summary>
    public decimal? TotalInstallationCost { get; set; }
}
