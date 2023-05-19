using ImgWreck.Common;
using PortalCommon.Utilities;
using PortalSvc.Common;

namespace PortalSvc.Data
{
    public class MedicalDocCis : MedicalDocEntry
    {
        public override string DocId => $"MedicalDocCis_{Id}";
        public override string ContentType => ContentTypeEnum.rtf.GetDescription();

        // CIS Last updated
        public DateTime LastUpdatedDtm { get; set; }
        public DateTime ReleaseDate
        {
            get
            {
                if (App.Settings.MedicalDocDelayCalendarDays.ContainsKey(TypeDescription))
                {
                    return LastUpdatedDtm.AddDays(App.Settings.MedicalDocDelayCalendarDays[TypeDescription]);
                }
                return LastUpdatedDtm;
            }
        }

        // Portal refresh tracking
        public DateTime LastUpdatedDate { get; set; }

        public DateTime? ReadDate { get; set; }

        /// <summary>
        /// First ReadDate
        /// </summary>
        public DateTime? ReadDate0 { get; set; }
    }
}
