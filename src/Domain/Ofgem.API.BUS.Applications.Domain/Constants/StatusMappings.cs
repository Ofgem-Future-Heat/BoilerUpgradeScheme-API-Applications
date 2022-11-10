using System.Collections.ObjectModel;
using static Ofgem.API.BUS.Applications.Domain.ApplicationStatus;
using static Ofgem.API.BUS.Applications.Domain.ApplicationSubStatus;
using static Ofgem.API.BUS.Applications.Domain.VoucherStatus;
using static Ofgem.API.BUS.Applications.Domain.VoucherSubStatus;

namespace Ofgem.API.BUS.Applications.Domain.Constants;

/// <summary>
/// Provides mappings between status enums and their database ID.
/// </summary>
public static class StatusMappings
{
    public static readonly IReadOnlyDictionary<ApplicationStatusCode, Guid> ApplicationStatus = new ReadOnlyDictionary<ApplicationStatusCode, Guid>(
        new Dictionary<ApplicationStatusCode, Guid>
        {
            { ApplicationStatusCode.VPEND, new Guid("C04AEF93-8688-4019-8BB9-4F84BBFABCD8") },
            { ApplicationStatusCode.CNTRD, new Guid("5F336322-6BDD-495F-ADB2-3289B7FA45AB") },
            { ApplicationStatusCode.CNTRW, new Guid("5B83EAB0-D57E-45AB-8382-20C52716610B") },
            { ApplicationStatusCode.REJECTED, new Guid("A52CA3B1-6B12-484C-8788-F192B9CD1B2F") },
            { ApplicationStatusCode.VISSD, new Guid("4E41B6B2-65BA-445E-8A79-B9BFCBEBFE0D") },
            { ApplicationStatusCode.SUB, new Guid("7D26A195-D305-4EB7-A3E5-9B884D826B9D") },
            { ApplicationStatusCode.RPEND, new Guid("1B610D12-6ADF-4717-97D7-F6FC9CCFCEEF") },
            { ApplicationStatusCode.WITHDRAWN, new Guid("551EEDBE-1DF4-4E68-A089-582B30A66C4D") },
            { ApplicationStatusCode.WITH, new Guid("08D7249A-E74B-4E46-8F0C-DBAC9E23AA0E") },
            { ApplicationStatusCode.VEXPD, new Guid("672A6414-5F47-4300-9C1F-9CB6ACD7B4E7") },
            { ApplicationStatusCode.INRW, new Guid("5B505F21-8955-40B2-AA63-4B6513D71C7F") },
            { ApplicationStatusCode.VQUED, new Guid("E2C06149-BE85-4093-88BF-89B2D5851F97") },
            { ApplicationStatusCode.CNTPS, new Guid("8C7769D8-7151-48F5-85ED-8BBC4E8F779C") }
        });

    public static readonly IReadOnlyDictionary<ApplicationSubStatusCode, Guid> ApplicationSubStatus = new ReadOnlyDictionary<ApplicationSubStatusCode, Guid>(
        new Dictionary<ApplicationSubStatusCode, Guid>
        {
            { ApplicationSubStatusCode.SUB, new Guid("9E8FA3F3-8E5A-4C54-893A-70D6EF1B0EC6") },
            { ApplicationSubStatusCode.INRW, new Guid("EF1DC50E-CDD6-4D2D-B85A-BBDD80D31EF2") },
            { ApplicationSubStatusCode.WITH, new Guid("67AA07DE-A9A2-498D-B3EB-A1C0674D7D8C") },
            { ApplicationSubStatusCode.QC, new Guid("B0FD63E0-FAC7-4793-BAC1-CAD1AABB7B36") },
            { ApplicationSubStatusCode.DA, new Guid("C5408F1D-D934-446F-9DE8-67E1B28C2938") },
            { ApplicationSubStatusCode.VPEND, new Guid("CB257734-56BF-4E61-A402-1DF32F6B8D9F") },
            { ApplicationSubStatusCode.VISSD, new Guid("64DFCE53-A2AB-4CE4-9EED-30C26D7C9542") },
            { ApplicationSubStatusCode.VQUED, new Guid("23647A88-7A40-41FF-A05F-F3AE97FC221A") },
            { ApplicationSubStatusCode.VEXPD, new Guid("17D4A77A-FC48-4167-B708-BC5ED1351691") },
            { ApplicationSubStatusCode.CNTRW, new Guid("DB4E2757-3A4F-4661-AA0D-28702533FB09") },
            { ApplicationSubStatusCode.CNTPS, new Guid("8D4B7FB7-E56E-4457-9225-FAC405F969ED") },
            { ApplicationSubStatusCode.CNTRD, new Guid("D15C5F8D-D63E-4D0F-B7A2-2300BD886CB3") },
            { ApplicationSubStatusCode.RPEND, new Guid("6764C024-494D-4978-B88F-8DD9BF36A5AD") },
            { ApplicationSubStatusCode.REJECTED, new Guid("B85ED635-9268-4021-8872-2C956C57D256") },
            { ApplicationSubStatusCode.WITHDRAWN, new Guid("F8744701-E998-4E7B-BF08-935A39230C66") },
        });

    public static readonly IReadOnlyDictionary<VoucherStatusCode, Guid> VoucherStatus = new ReadOnlyDictionary<VoucherStatusCode, Guid>(
        new Dictionary<VoucherStatusCode, Guid>
        {
            { VoucherStatusCode.SUB, new Guid("7D26A195-D305-4EB7-A3E5-9B884D826B9D") },
            { VoucherStatusCode.WITHIN, new Guid("20014E3A-20F7-4DFA-A77E-0DD166E3C81B") },
            { VoucherStatusCode.REJPEND, new Guid("CC4D8724-8CD3-45C3-BAD4-4BE79DC0BF44") },
            { VoucherStatusCode.REJECTED, new Guid("10B3EB4C-67FB-43C4-A62B-9397E4D3F6D6") },
            { VoucherStatusCode.SENTPAY, new Guid("AE6BCB78-9D71-4E9C-858C-8426F1703FF7") },
            { VoucherStatusCode.PAYSUS, new Guid("E95B4F1B-6CFC-4C60-89B2-71836E72C0AF") },
            { VoucherStatusCode.REDAPP, new Guid("C9924A48-58D8-4B53-A92D-5E81320F13A3") },
            { VoucherStatusCode.REDREC, new Guid("F202AC26-AB70-4306-B13E-7B18AAFFFC19") },
            { VoucherStatusCode.REVOKED, new Guid("C6123CF0-C4E1-45F4-96BF-DAFA94F69CC4") },
            { VoucherStatusCode.REDREV, new Guid("EF0EDB18-801A-4840-A875-18A0D59AF7E8") },
            { VoucherStatusCode.PAID, new Guid("C7AEC5DF-9FA3-45E9-9682-809DD3B41CEF") },
        });

    public static readonly IReadOnlyDictionary<VoucherSubStatusCode, Guid> VoucherSubStatus = new ReadOnlyDictionary<VoucherSubStatusCode, Guid>(
        new Dictionary<VoucherSubStatusCode, Guid>
        {
            { VoucherSubStatusCode.SUB, new Guid("B0A81122-18BC-4D4A-8271-3FDA3CB14E6E") },
            { VoucherSubStatusCode.REDREV, new Guid("41ADDEAC-B9CD-45C5-A5C3-C5464536A9DE") },
            { VoucherSubStatusCode.WITHIN, new Guid("8A841B1D-8625-4BFE-8DAC-1813CDAE5903") },
            { VoucherSubStatusCode.QC, new Guid("9F69F2A5-2447-4BF8-AA1C-65D203BCB039") },
            { VoucherSubStatusCode.DA, new Guid("4E0DFA71-F281-481C-BC44-BAF40E92FFFC") },
            { VoucherSubStatusCode.REDAPP, new Guid("A7F0C859-AF7B-4C6F-8371-B0DAC4C4C5CB") },
            { VoucherSubStatusCode.SENTPAY, new Guid("53651862-460A-4B56-9B6F-8EF0EBFEFC74") },
            { VoucherSubStatusCode.PAID, new Guid("20694B74-E7D6-4472-8C06-DE4650EACA0D") },
            { VoucherSubStatusCode.REJPEND, new Guid("16DDF730-5128-4159-9C46-7A8F7B2E8811") },
            { VoucherSubStatusCode.REJECTED, new Guid("50D09BB7-30D6-461E-A3F9-873C5F450191") },
            { VoucherSubStatusCode.PAYSUS, new Guid("0477592B-EA28-469A-98FE-936637EB074F") },
            { VoucherSubStatusCode.REVOKED, new Guid("F68C7F33-AF07-477D-A39B-BB28D5C1494C") }
        });

    public static readonly IReadOnlyDictionary<ApplicationSubStatusCode, HashSet<Guid>> ApplyTransitions = new ReadOnlyDictionary<ApplicationSubStatusCode, HashSet<Guid>>(
        new Dictionary<ApplicationSubStatusCode, HashSet<Guid>>
        {
            { ApplicationSubStatusCode.SUB, new HashSet<Guid> { ApplicationSubStatus[ApplicationSubStatusCode.SUB], ApplicationSubStatus[ApplicationSubStatusCode.INRW], ApplicationSubStatus[ApplicationSubStatusCode.WITH], ApplicationSubStatus[ApplicationSubStatusCode.CNTRW], ApplicationSubStatus[ApplicationSubStatusCode.WITHDRAWN] } },
            { ApplicationSubStatusCode.INRW, new HashSet<Guid> { ApplicationSubStatus[ApplicationSubStatusCode.INRW], ApplicationSubStatus[ApplicationSubStatusCode.WITH], ApplicationSubStatus[ApplicationSubStatusCode.QC], ApplicationSubStatus[ApplicationSubStatusCode.CNTRW], ApplicationSubStatus[ApplicationSubStatusCode.WITHDRAWN] } },
            { ApplicationSubStatusCode.WITH, new HashSet<Guid> { ApplicationSubStatus[ApplicationSubStatusCode.INRW], ApplicationSubStatus[ApplicationSubStatusCode.WITH], ApplicationSubStatus[ApplicationSubStatusCode.CNTRW], ApplicationSubStatus[ApplicationSubStatusCode.WITHDRAWN] } },
            { ApplicationSubStatusCode.QC, new HashSet<Guid> { ApplicationSubStatus[ApplicationSubStatusCode.INRW], ApplicationSubStatus[ApplicationSubStatusCode.DA], ApplicationSubStatus[ApplicationSubStatusCode.VPEND], ApplicationSubStatus[ApplicationSubStatusCode.VISSD], ApplicationSubStatus[ApplicationSubStatusCode.VQUED], ApplicationSubStatus[ApplicationSubStatusCode.VEXPD], ApplicationSubStatus[ApplicationSubStatusCode.RPEND], ApplicationSubStatus[ApplicationSubStatusCode.CNTRD], ApplicationSubStatus[ApplicationSubStatusCode.CNTRW], ApplicationSubStatus[ApplicationSubStatusCode.WITHDRAWN] } },
            { ApplicationSubStatusCode.DA, new HashSet<Guid> { ApplicationSubStatus[ApplicationSubStatusCode.INRW], ApplicationSubStatus[ApplicationSubStatusCode.QC], ApplicationSubStatus[ApplicationSubStatusCode.VPEND], ApplicationSubStatus[ApplicationSubStatusCode.VISSD], ApplicationSubStatus[ApplicationSubStatusCode.VQUED], ApplicationSubStatus[ApplicationSubStatusCode.VEXPD], ApplicationSubStatus[ApplicationSubStatusCode.RPEND], ApplicationSubStatus[ApplicationSubStatusCode.CNTRD], ApplicationSubStatus[ApplicationSubStatusCode.CNTRW], ApplicationSubStatus[ApplicationSubStatusCode.WITHDRAWN] } },
            { ApplicationSubStatusCode.VPEND, new HashSet<Guid> { ApplicationSubStatus[ApplicationSubStatusCode.VISSD], ApplicationSubStatus[ApplicationSubStatusCode.VQUED], ApplicationSubStatus[ApplicationSubStatusCode.CNTRW], ApplicationSubStatus[ApplicationSubStatusCode.WITHDRAWN], ApplicationSubStatus[ApplicationSubStatusCode.QC], ApplicationSubStatus[ApplicationSubStatusCode.DA] } },
            { ApplicationSubStatusCode.VISSD, new HashSet<Guid> { ApplicationSubStatus[ApplicationSubStatusCode.VEXPD], ApplicationSubStatus[ApplicationSubStatusCode.CNTRD], VoucherSubStatus[VoucherSubStatusCode.SUB], VoucherSubStatus[VoucherSubStatusCode.REVOKED] } },
            { ApplicationSubStatusCode.VQUED, new HashSet<Guid> { ApplicationSubStatus[ApplicationSubStatusCode.VISSD], ApplicationSubStatus[ApplicationSubStatusCode.CNTRW], ApplicationSubStatus[ApplicationSubStatusCode.WITHDRAWN], ApplicationSubStatus[ApplicationSubStatusCode.CNTRD] } },
            { ApplicationSubStatusCode.VEXPD, new HashSet<Guid> { ApplicationSubStatus[ApplicationSubStatusCode.QC], ApplicationSubStatus[ApplicationSubStatusCode.DA] } },
            { ApplicationSubStatusCode.CNTRW, new HashSet<Guid> { ApplicationSubStatus[ApplicationSubStatusCode.INRW], ApplicationSubStatus[ApplicationSubStatusCode.WITH], ApplicationSubStatus[ApplicationSubStatusCode.QC], ApplicationSubStatus[ApplicationSubStatusCode.DA], ApplicationSubStatus[ApplicationSubStatusCode.VPEND], ApplicationSubStatus[ApplicationSubStatusCode.VQUED], ApplicationSubStatus[ApplicationSubStatusCode.CNTRD], ApplicationSubStatus[ApplicationSubStatusCode.CNTPS] } },
            { ApplicationSubStatusCode.CNTPS, new HashSet<Guid> { ApplicationSubStatus[ApplicationSubStatusCode.INRW], ApplicationSubStatus[ApplicationSubStatusCode.WITH], ApplicationSubStatus[ApplicationSubStatusCode.QC], ApplicationSubStatus[ApplicationSubStatusCode.DA], ApplicationSubStatus[ApplicationSubStatusCode.CNTRW], ApplicationSubStatus[ApplicationSubStatusCode.RPEND] } },
            { ApplicationSubStatusCode.CNTRD, new HashSet<Guid> { ApplicationSubStatus[ApplicationSubStatusCode.INRW] } },
            { ApplicationSubStatusCode.RPEND, new HashSet<Guid> { ApplicationSubStatus[ApplicationSubStatusCode.INRW], ApplicationSubStatus[ApplicationSubStatusCode.REJECTED] } },
            { ApplicationSubStatusCode.REJECTED, new HashSet<Guid> { ApplicationSubStatus[ApplicationSubStatusCode.INRW], ApplicationSubStatus[ApplicationSubStatusCode.QC], ApplicationSubStatus[ApplicationSubStatusCode.DA] } },
            { ApplicationSubStatusCode.WITHDRAWN, new HashSet<Guid> { ApplicationSubStatus[ApplicationSubStatusCode.INRW] } }
        });

    public static readonly IReadOnlyDictionary<ApplicationSubStatusCode, HashSet<Guid>> BackwardsApplyTransitionsForL2ApplyManager = new ReadOnlyDictionary<ApplicationSubStatusCode, HashSet<Guid>>(
        new Dictionary<ApplicationSubStatusCode, HashSet<Guid>>
        {
            { ApplicationSubStatusCode.INRW, new HashSet<Guid> { ApplicationSubStatus[ApplicationSubStatusCode.SUB] } },
            { ApplicationSubStatusCode.WITH, new HashSet<Guid> { ApplicationSubStatus[ApplicationSubStatusCode.SUB] } },
            { ApplicationSubStatusCode.QC, new HashSet<Guid> { ApplicationSubStatus[ApplicationSubStatusCode.SUB], ApplicationSubStatus[ApplicationSubStatusCode.WITH] } },
            { ApplicationSubStatusCode.DA, new HashSet<Guid> { ApplicationSubStatus[ApplicationSubStatusCode.SUB], ApplicationSubStatus[ApplicationSubStatusCode.WITH] } },
            { ApplicationSubStatusCode.VPEND, new HashSet<Guid> { ApplicationSubStatus[ApplicationSubStatusCode.SUB], ApplicationSubStatus[ApplicationSubStatusCode.INRW], ApplicationSubStatus[ApplicationSubStatusCode.WITH] } },
            { ApplicationSubStatusCode.VISSD, new HashSet<Guid> { ApplicationSubStatus[ApplicationSubStatusCode.SUB], ApplicationSubStatus[ApplicationSubStatusCode.INRW], ApplicationSubStatus[ApplicationSubStatusCode.WITH], ApplicationSubStatus[ApplicationSubStatusCode.QC], ApplicationSubStatus[ApplicationSubStatusCode.DA], ApplicationSubStatus[ApplicationSubStatusCode.VPEND] } },
            { ApplicationSubStatusCode.VQUED, new HashSet<Guid> { ApplicationSubStatus[ApplicationSubStatusCode.SUB], ApplicationSubStatus[ApplicationSubStatusCode.INRW], ApplicationSubStatus[ApplicationSubStatusCode.WITH], ApplicationSubStatus[ApplicationSubStatusCode.QC], ApplicationSubStatus[ApplicationSubStatusCode.DA], ApplicationSubStatus[ApplicationSubStatusCode.VPEND] } },
            { ApplicationSubStatusCode.VEXPD, new HashSet<Guid> { ApplicationSubStatus[ApplicationSubStatusCode.SUB], ApplicationSubStatus[ApplicationSubStatusCode.INRW], ApplicationSubStatus[ApplicationSubStatusCode.WITH], ApplicationSubStatus[ApplicationSubStatusCode.VISSD] } },
            { ApplicationSubStatusCode.CNTRW, new HashSet<Guid> { ApplicationSubStatus[ApplicationSubStatusCode.SUB] } },
            { ApplicationSubStatusCode.CNTPS, new HashSet<Guid> { ApplicationSubStatus[ApplicationSubStatusCode.SUB] } },
            { ApplicationSubStatusCode.CNTRD, new HashSet<Guid> { ApplicationSubStatus[ApplicationSubStatusCode.SUB], ApplicationSubStatus[ApplicationSubStatusCode.WITH], ApplicationSubStatus[ApplicationSubStatusCode.QC], ApplicationSubStatus[ApplicationSubStatusCode.DA], ApplicationSubStatus[ApplicationSubStatusCode.VQUED], ApplicationSubStatus[ApplicationSubStatusCode.VISSD] } },
            { ApplicationSubStatusCode.RPEND, new HashSet<Guid> { ApplicationSubStatus[ApplicationSubStatusCode.SUB], ApplicationSubStatus[ApplicationSubStatusCode.WITH], ApplicationSubStatus[ApplicationSubStatusCode.QC], ApplicationSubStatus[ApplicationSubStatusCode.DA], ApplicationSubStatus[ApplicationSubStatusCode.CNTPS] } },
            { ApplicationSubStatusCode.REJECTED, new HashSet<Guid> { ApplicationSubStatus[ApplicationSubStatusCode.SUB], ApplicationSubStatus[ApplicationSubStatusCode.RPEND] } },
            { ApplicationSubStatusCode.WITHDRAWN, new HashSet<Guid> { ApplicationSubStatus[ApplicationSubStatusCode.SUB], ApplicationSubStatus[ApplicationSubStatusCode.INRW], ApplicationSubStatus[ApplicationSubStatusCode.WITH], ApplicationSubStatus[ApplicationSubStatusCode.QC], ApplicationSubStatus[ApplicationSubStatusCode.DA], ApplicationSubStatus[ApplicationSubStatusCode.VPEND], ApplicationSubStatus[ApplicationSubStatusCode.VQUED] } },
        });


    public static readonly IReadOnlyDictionary<VoucherSubStatusCode, HashSet<Guid>> RedeemTransitions = new ReadOnlyDictionary<VoucherSubStatusCode, HashSet<Guid>>(
        new Dictionary<VoucherSubStatusCode, HashSet<Guid>>
        {
            { VoucherSubStatusCode.SUB, new HashSet<Guid> { VoucherSubStatus[VoucherSubStatusCode.SUB], VoucherSubStatus[VoucherSubStatusCode.REDREV], VoucherSubStatus[VoucherSubStatusCode.WITHIN] } },
            { VoucherSubStatusCode.REDREV, new HashSet<Guid> { VoucherSubStatus[VoucherSubStatusCode.REDREV], VoucherSubStatus[VoucherSubStatusCode.WITHIN], VoucherSubStatus[VoucherSubStatusCode.QC] } },
            { VoucherSubStatusCode.WITHIN, new HashSet<Guid> { VoucherSubStatus[VoucherSubStatusCode.REDREV], VoucherSubStatus[VoucherSubStatusCode.WITHIN], VoucherSubStatus[VoucherSubStatusCode.QC], VoucherSubStatus[VoucherSubStatusCode.DA] } },
            { VoucherSubStatusCode.QC,new HashSet<Guid> { VoucherSubStatus[VoucherSubStatusCode.REDREV], VoucherSubStatus[VoucherSubStatusCode.DA], VoucherSubStatus[VoucherSubStatusCode.SENTPAY], VoucherSubStatus[VoucherSubStatusCode.REJPEND], ApplicationSubStatus[ApplicationSubStatusCode.QC] } },
            { VoucherSubStatusCode.DA,new HashSet<Guid> { VoucherSubStatus[VoucherSubStatusCode.REDREV], VoucherSubStatus[VoucherSubStatusCode.QC], VoucherSubStatus[VoucherSubStatusCode.SENTPAY], VoucherSubStatus[VoucherSubStatusCode.PAYSUS], VoucherSubStatus[VoucherSubStatusCode.REJPEND], ApplicationSubStatus[ApplicationSubStatusCode.QC] } },
            { VoucherSubStatusCode.REDAPP,new HashSet<Guid> { VoucherSubStatus[VoucherSubStatusCode.PAID], VoucherSubStatus[VoucherSubStatusCode.SENTPAY], VoucherSubStatus[VoucherSubStatusCode.PAYSUS], VoucherSubStatus[VoucherSubStatusCode.QC], VoucherSubStatus[VoucherSubStatusCode.DA] } },
            { VoucherSubStatusCode.SENTPAY,new HashSet<Guid> { VoucherSubStatus[VoucherSubStatusCode.SENTPAY], VoucherSubStatus[VoucherSubStatusCode.PAYSUS],VoucherSubStatus[VoucherSubStatusCode.REDAPP], VoucherSubStatus[VoucherSubStatusCode.PAID], VoucherSubStatus[VoucherSubStatusCode.QC], VoucherSubStatus[VoucherSubStatusCode.DA] } },
            { VoucherSubStatusCode.PAID,new HashSet<Guid> { VoucherSubStatus[VoucherSubStatusCode.DA] } },
            { VoucherSubStatusCode.REJPEND,new HashSet<Guid> { VoucherSubStatus[VoucherSubStatusCode.DA], VoucherSubStatus[VoucherSubStatusCode.REJECTED] } },
            { VoucherSubStatusCode.REJECTED,new HashSet<Guid> { VoucherSubStatus[VoucherSubStatusCode.DA] } },
            { VoucherSubStatusCode.PAYSUS,new HashSet<Guid> { VoucherSubStatus[VoucherSubStatusCode.DA], VoucherSubStatus[VoucherSubStatusCode.SENTPAY], VoucherSubStatus[VoucherSubStatusCode.REDAPP], VoucherSubStatus[VoucherSubStatusCode.REVOKED] } },
            { VoucherSubStatusCode.REVOKED,new HashSet<Guid> { VoucherSubStatus[VoucherSubStatusCode.DA], VoucherSubStatus[VoucherSubStatusCode.QC] } },
        });

    public static readonly IReadOnlyDictionary<VoucherSubStatusCode, HashSet<Guid>> BackwardsRedeemTransitionsForL2RedeemManager = new ReadOnlyDictionary<VoucherSubStatusCode, HashSet<Guid>>(
        new Dictionary<VoucherSubStatusCode, HashSet<Guid>>
        {
             { VoucherSubStatusCode.REDREV, new HashSet<Guid> { VoucherSubStatus[VoucherSubStatusCode.SUB] } },
             { VoucherSubStatusCode.WITHIN, new HashSet<Guid> { VoucherSubStatus[VoucherSubStatusCode.SUB] } },
             { VoucherSubStatusCode.QC, new HashSet<Guid> { VoucherSubStatus[VoucherSubStatusCode.SUB], VoucherSubStatus[VoucherSubStatusCode.WITHIN] } },
             { VoucherSubStatusCode.DA, new HashSet<Guid> { VoucherSubStatus[VoucherSubStatusCode.SUB], VoucherSubStatus[VoucherSubStatusCode.WITHIN] } },
             { VoucherSubStatusCode.REDAPP, new HashSet<Guid> { VoucherSubStatus[VoucherSubStatusCode.SUB], VoucherSubStatus[VoucherSubStatusCode.WITHIN], VoucherSubStatus[VoucherSubStatusCode.REDREV] } },
             { VoucherSubStatusCode.SENTPAY, new HashSet<Guid> { VoucherSubStatus[VoucherSubStatusCode.SUB], VoucherSubStatus[VoucherSubStatusCode.WITHIN], VoucherSubStatus[VoucherSubStatusCode.REDREV] } },
             { VoucherSubStatusCode.PAID, new HashSet<Guid> { VoucherSubStatus[VoucherSubStatusCode.SUB], VoucherSubStatus[VoucherSubStatusCode.WITHIN], VoucherSubStatus[VoucherSubStatusCode.REDREV], VoucherSubStatus[VoucherSubStatusCode.QC], VoucherSubStatus[VoucherSubStatusCode.REDAPP], VoucherSubStatus[VoucherSubStatusCode.SENTPAY] } },
             { VoucherSubStatusCode.REJPEND, new HashSet<Guid> { VoucherSubStatus[VoucherSubStatusCode.SUB], VoucherSubStatus[VoucherSubStatusCode.WITHIN], VoucherSubStatus[VoucherSubStatusCode.REDREV], VoucherSubStatus[VoucherSubStatusCode.QC] } },
             { VoucherSubStatusCode.REJECTED, new HashSet<Guid> { VoucherSubStatus[VoucherSubStatusCode.SUB], VoucherSubStatus[VoucherSubStatusCode.WITHIN], VoucherSubStatus[VoucherSubStatusCode.REDREV], VoucherSubStatus[VoucherSubStatusCode.QC], VoucherSubStatus[VoucherSubStatusCode.REJPEND] } },
             { VoucherSubStatusCode.PAYSUS, new HashSet<Guid> { VoucherSubStatus[VoucherSubStatusCode.SUB], VoucherSubStatus[VoucherSubStatusCode.WITHIN], VoucherSubStatus[VoucherSubStatusCode.REDREV], VoucherSubStatus[VoucherSubStatusCode.QC] } },
             { VoucherSubStatusCode.REVOKED, new HashSet<Guid> { VoucherSubStatus[VoucherSubStatusCode.SUB], VoucherSubStatus[VoucherSubStatusCode.WITHIN], VoucherSubStatus[VoucherSubStatusCode.REDREV], VoucherSubStatus[VoucherSubStatusCode.QC], VoucherSubStatus[VoucherSubStatusCode.PAYSUS] } },
        });
}
