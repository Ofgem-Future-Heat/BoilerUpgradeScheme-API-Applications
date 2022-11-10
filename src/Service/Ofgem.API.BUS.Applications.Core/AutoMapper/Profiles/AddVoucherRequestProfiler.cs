using AutoMapper;
using Ofgem.API.BUS.Applications.Core.AutoMapper.Converters;
using Ofgem.API.BUS.Applications.Domain;
using Ofgem.API.BUS.Applications.Domain.Entities.CommsObjects;

namespace Ofgem.API.BUS.Applications.Core.AutoMapper.Profiles;

/// <summary>
/// Automapper profile for AddVoucherRequest
/// </summary>
public class AddVoucherRequestProfiler : Profile
{
    /// <summary>
    /// This method sets up the profiler for Automapper and creates the map for converting the
    /// request to the domain type
    /// </summary>
    public AddVoucherRequestProfiler()
    {
        CreateMap<AddVoucherRequest, Voucher>()
            .ConvertUsing<VoucherConverter>();
    }
}
