using Microsoft.AspNetCore.Mvc;
using Ofgem.API.BUS.Applications.Api.Extensions;
using Ofgem.API.BUS.Applications.Core.Interfaces;
using Ofgem.API.BUS.Applications.Domain;
using Ofgem.Lib.BUS.APIClient.Domain.Exceptions;

namespace Ofgem.API.BUS.Applications.Api.Controllers;

/// <summary>
/// Routes all TechType operations to the Applications Service
/// </summary>
[ApiController]
[Route("TechTypes")]
public class TechTypesController : ControllerBase
{
    private readonly IApplicationsService _applicationsService;

    public TechTypesController(IApplicationsService applicationsService)
    {
        _applicationsService = applicationsService ?? throw new ArgumentNullException(nameof(applicationsService));
    }

    /// <summary>
    /// Gets Tech Types.
    /// </summary>
    /// <returns>All <see cref="TechType"/> objects.</returns>
    [Route("")]
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<TechType>), 200)]
    public async Task<IActionResult> GetTechTypes()
    {
        try
        {
            return Ok(await _applicationsService.GetTechTypeListAsync());
        }
        catch (BadRequestException ex)
        {
            return this.AsObjectResult(ex);
        }
    }
}
