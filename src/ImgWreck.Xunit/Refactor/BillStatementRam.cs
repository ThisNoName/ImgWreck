using System;
using System.Runtime.Serialization;
using Insight.Database;

namespace PortalSvc.Data
{
    // [TableName("BillStatementRam"), PrimaryKey("FNDocId")]
    public class BillStatementRam
    {
        public DateTime BillDate { get; set; }

        [Column("FNDocId")]
        public int DocId { get; set; }

        [IgnoreDataMember]
        public string PatId { get; set; }

        //public string MRN { get; set; }

        //public string Account { get; set; }
        //public string DocType { get; set; }
        //public byte AccountType { get; set; }
        //public int? Pages { get; set; }
        //public string FNZro { get; set; }
        //public DateTime? AdmDate { get; set; }
        //public DateTime? DischDate { get; set; }
        //[NPoco.Column("LoadTimeStamp")]
        //public DateTime LastUpdatedDate { get; set; }
    }
}
