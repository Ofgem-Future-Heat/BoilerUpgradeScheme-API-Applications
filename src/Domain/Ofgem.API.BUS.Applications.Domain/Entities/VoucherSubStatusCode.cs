namespace Ofgem.API.BUS.Applications.Domain;

public partial class VoucherSubStatus
{
    public enum VoucherSubStatusCode
    {
        SUB,
        REDREV,
        WITHIN,
        REDAPP,
        SENTPAY,
        PAID,
        REJPEND,
        REJECTED,
        PAYSUS,
        REVOKED,
        QC,
        DA
    }
}