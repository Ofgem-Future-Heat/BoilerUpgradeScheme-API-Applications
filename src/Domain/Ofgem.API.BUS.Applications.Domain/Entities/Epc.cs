namespace Ofgem.API.BUS.Applications.Domain;

/// <summary>
/// Model for EPC db table
/// </summary>
public class Epc
{
    /// <summary>
    /// Unique internal identifier
    /// </summary>
    public Guid ID { get; set; }

    /// <summary>
    /// This is the unique EPC Reference Number
    /// </summary>
    public string? EpcReferenceNumber { get; set; }
}

