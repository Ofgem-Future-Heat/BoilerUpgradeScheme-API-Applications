using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Ofgem.Lib.BUS.APIClient.Domain.Exceptions;
using System.Net;

namespace Ofgem.API.BUS.Applications.Api.Extensions;

/// <summary>
/// Controller extensions
/// </summary>
public static class ControllerExtensions
{
    /// <summary>
    /// Bad request object result.
    /// </summary>
    /// <param name="controllerBase">The controller.</param>
    /// <param name="ex">BadRequestException.</param>
    /// <returns></returns>
    public static ActionResult AsObjectResult(this ControllerBase controllerBase, BadRequestException ex)
    {
        var request = FormatRequest(ex);
        
        if (ex.StatusCode == HttpStatusCode.NotFound)
        {
            return controllerBase.NotFound(request);
        }
        else if (ex.StatusCode == HttpStatusCode.NoContent)
        {
            return controllerBase.NoContent();
        }
        
        return controllerBase.BadRequest(request);
    }

    public static ActionResult AsObjectResult(this ControllerBase controllerBase, List<string>  errorMessages)
    {
        var request = FormatRequest(errorMessages);
        return controllerBase.Conflict(request);
    }

    private static object FormatRequest(List<string> errorMessages)
    {
        return JsonConvert.SerializeObject(errorMessages);
    }

    private static object FormatRequest(BadRequestException ex)
    {
        if (ex.Errors is not null && ex.Errors.Any())
        {
            return new { title = ex.Message, status = ex.StatusCode, errors = ex.Errors };
        }
        return new { title = ex.Message, status = ex.StatusCode };
    }
}
