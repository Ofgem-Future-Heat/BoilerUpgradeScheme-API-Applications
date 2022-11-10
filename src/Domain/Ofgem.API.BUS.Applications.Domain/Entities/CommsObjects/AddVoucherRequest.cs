namespace Ofgem.API.BUS.Applications.Domain.Entities.CommsObjects;

/// <summary>
/// Dto containing the data required to add a new Voucher
/// </summary>
public class AddVoucherRequest
{
    public Guid ApplicationID { get; set; }
    public Guid TechTypeId { get; set; }
}
