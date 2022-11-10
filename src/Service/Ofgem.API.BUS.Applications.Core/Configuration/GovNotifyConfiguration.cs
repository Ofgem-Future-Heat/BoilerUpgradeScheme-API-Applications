namespace Ofgem.API.BUS.Applications.Core.Configuration;

public class GovNotifyConfiguration
{
    public string ApiKey { get; set; } = string.Empty;
    public Guid InstallerPostApplicationEmailReplyToId { get; set; }
    public InstallerPostApplicationTemplatesConfiguration InstallerPostApplicationTemplates { get; set; } = null!;
}
