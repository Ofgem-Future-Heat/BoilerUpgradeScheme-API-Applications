using AutoMapper;
using Ofgem.API.BUS.Applications.Core.AutoMapper.Converters;
using Ofgem.API.BUS.Applications.Domain;
using Ofgem.API.BUS.Applications.Domain.Entities.CommsObjects;

namespace Ofgem.API.BUS.Applications.Core.AutoMapper.Profiles;

/// <summary>
/// Autompper profile for CreateApplicationRequest
/// </summary>
public class CreateApplicationRequestProfiler : Profile
{
    /// <summary>
    /// This method sets up the profiler for Automapper and creates the map for converting the
    /// request to the domain type
    /// </summary>
    public CreateApplicationRequestProfiler()
    {
        CreateMap<CreateApplicationRequest, Application>()
            .ConvertUsing<ApplicationConverter>();
    }
}
