namespace Ofgem.API.BUS.Applications.Domain.Concrete;

public record SendEmailResult
{
    public bool IsSuccess { get; set; }
    public string? ErrorMessage { get; set; }
}
