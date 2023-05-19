using System.ComponentModel.DataAnnotations.Schema;

namespace PortalSvc
{
    public class VisitNotesMapping
    {
        [Column("DocumentName")]
        public string DocumentName { get; set; }

        //[Column("DocumentType")]
        //public string DocumentType { get; set; }

        [Column("SubCategoryName")]
        public string SubCategoryName { get; set; }

        [Column("DisplayName")]
        public string DisplayName { get; set; }
    }
}
