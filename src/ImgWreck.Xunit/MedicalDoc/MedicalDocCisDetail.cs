using ImgWreck.Common;
using Insight.Database;
using System.Text.RegularExpressions;

namespace PortalSvc.Data
{
    public class MedicalDocCisDetail
    {
        [Column("ClientDisplayName")]
        public string PatientName { get; set; }

        public string Mrn { get; set; }

        [Column("AdmitDtm")]
        public DateTime AdmitDate { get; set; }

        [Column("ServiceDtm")]
        public DateTime VisitDate { get; set; }

        [Column("VisitIDCode")]
        public string VisitCode { get; set; }

        [Column("ProviderDisplayName")]
        public string ProviderName { get; set; }

        [Column("CurrentLocation")]
        public string Location { get; set; }

        [Column("DocumentName")]
        public string DocumentName { get; set; }

        public byte[] DocumentImage { get; set; }

        public string TemplateId
        {
            get
            {
                foreach (var key in App.Settings.MedicalDocTemplates.Keys)
                {
                    if (Regex.IsMatch(DocumentName,
                        App.Settings.MedicalDocTemplates[key]))
                    {
                        return $"MedicalDoc_{key}.cshtml";
                    }
                }

                return "MedicalDoc_Default.cshtml";
            }
        }
    }
}
