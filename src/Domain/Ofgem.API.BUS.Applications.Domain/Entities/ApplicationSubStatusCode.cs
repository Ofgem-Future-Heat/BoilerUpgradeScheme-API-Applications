namespace Ofgem.API.BUS.Applications.Domain;

public partial class ApplicationSubStatus
{
    public enum ApplicationSubStatusCode
    {
        SUB,
        INRW,
        WITH,
        VPEND,
        VISSD,
        VQUED,
        VEXPD,
        CNTRW,
        CNTPS,
        CNTRD,
        RPEND,
        REJECTED,
        WITHDRAWN,
        QC,
        DA
    }
}