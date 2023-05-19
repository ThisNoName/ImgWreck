using PortalSvc.Data;
using System.Text.RegularExpressions;

namespace PortalSvc.Core
{
    public static class VisitNotesMappingExtension
    {
        public static List<MedicalDocCis> MapDisplayName(
            this List<MedicalDocCis> meddocs, List<VisitNotesMapping> mappings)
        {
            foreach (var doc in meddocs)
            {   // get exact mapping or default, come back as
                // subcat/.default
                // subcat/some doc
                // subcat/other doc - wildcard
                var mapped = mappings.Where(x =>
                    x.SubCategoryName == doc.TypeDescription &&
                    (x.DocumentName == ".default" || Regex.IsMatch(
                        doc.Description, x.DocumentName, RegexOptions.IgnoreCase)
                    )).OrderBy(o => o.DocumentName);

                if (mapped.Any())
                {
                    doc.TypeDescription = (mapped.Count() == 1)
                        ? mapped.First().DisplayName // exact match name, or .default
                        : mapped.Last().DisplayName;
                }
            }

            return meddocs;
        }
    }
}
