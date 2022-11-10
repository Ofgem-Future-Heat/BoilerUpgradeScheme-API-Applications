using AutoMapper;
using Ofgem.API.BUS.Applications.Domain;
using Ofgem.API.BUS.Applications.Domain.Entities.CommsObjects;

namespace Ofgem.API.BUS.Applications.Core.AutoMapper.Converters;

/// <summary>
/// Converter to convert the AddVoucherRequest type to the Voucher type
/// </summary>
public class VoucherConverter : ITypeConverter<AddVoucherRequest, Voucher>
{
    /// <summary>
    /// Converter
    /// </summary>
    /// <param name="source">Source type</param>
    /// <param name="destination">Destination type</param>
    /// <param name="context"></param>
    /// <returns></returns>
    public Voucher Convert(AddVoucherRequest source, Voucher destination, ResolutionContext context)
    {
        return new Voucher()
        {
            ApplicationID = source.ApplicationID
        };
    }
}
