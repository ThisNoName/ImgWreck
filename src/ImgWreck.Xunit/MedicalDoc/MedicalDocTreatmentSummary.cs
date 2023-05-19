using PortalCommon.Utilities;
using PortalSvc.Common;

namespace PortalSvc.Data
{
    public class MedicalDocTreatmentSummary : MedicalDocEntry
    {
        public override string DocId => $"MedicalDocTreatmentSummary_{Id}";
        public override string ContentType => ContentTypeEnum.pdf.GetDescription();
        public override string TypeDescription => "Patient Treatment Summary and Care Plan";
        public override string Description => PhysicianName;
        public string PhysicianName { get; set; }
    }
}
