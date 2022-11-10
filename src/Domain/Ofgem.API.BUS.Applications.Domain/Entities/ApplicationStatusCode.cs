namespace Ofgem.API.BUS.Applications.Domain;

public partial class ApplicationStatus
{
    public enum ApplicationStatusCode
    {
        SUB,
        INRW,
        WITH,
        VPEND,
        VISSD,
        VQUED,
        VEXPD,
        CNTRW,
        CNTRD,
        RPEND,
        REJECTED,
        WITHDRAWN,
        CNTPS
    }
}