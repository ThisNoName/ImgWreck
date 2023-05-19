using System;
using Insight.Database;

namespace PortalSvc.Data
{
    public class BillPbdBalPayment
    {
        [Column("PAT_BAL")]
        public decimal? PatientBalance { get; set; }

        [Column("MOST_REC_PAYMENT_AMT")]
        public decimal? LastPayment { get; set; }

        [Column("MOST_REC_PAYMENT_DT")]
        public DateTime? LastPaymentDate { get; set; }
    }
}
