namespace Ofgem.API.BUS.Applications.Domain;

public partial class VoucherStatus
{
    public enum VoucherStatusCode
    {
        SUB,
        REDREC,
        REDREV,
        WITHIN,
        REDAPP,
        SENTPAY,
        PAID,
        REJPEND,
        REJECTED,
        PAYSUS,
        REVOKED
    }
}