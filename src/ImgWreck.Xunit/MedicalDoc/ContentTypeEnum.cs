using System.ComponentModel;

namespace PortalSvc.Common
{
    public enum ContentTypeEnum
    {
        [Description("application/pdf")] pdf,
        [Description("text/html")] html,
        [Description("text/xml")] xml,
        [Description("text/plain")] text,
        [Description("application/zip")] zip,
        [Description("image/jpeg")] jpeg,
        [Description("application/rtf")] rtf,
        [Description("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")] xlsx,
    }
}
