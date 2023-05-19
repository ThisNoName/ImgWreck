using System;
using Insight.Database;

namespace PortalSvc.Data
{
    public class BillPbdDetailDb
    {
        [Column("autoid")]
        public int AutoId { get; set; }
        [Column("MRN")]
        public string Mrn { get; set; }

        [Column("PAT_NM")]
        public string PatientName { get; set; }

        [Column("GRP_NUM")]
        public Int16 GroupNumber { get; set; }

        [Column("INV_NUM")]
        public int InvoiceNumber { get; set; }

        [Column("TXN_NUM")]
        public int TransactionNumber { get; set; }

        [Column("PROV")]
        public string Provider { get; set; }

        [Column("BILLING_AREA")]
        public string BillingArea { get; set; }

        [Column("INV_SER_DT")]
        public DateTime? ServiceDate { get; set; }

        [Column("TXN_POST_DT")]
        public DateTime PostDate { get; set; }

        [Column("PAYCODE_NUM")]
        public int? PayCode { get; set; }

        [Column("PROC")]
        public string Description { get; set; }

        [Column("PAYCODE_NM")]
        public string PaycodeName { get; set; }

        [Column("DEBIT_AMT")]
        public decimal? Debit { get; set; }

        [Column("PAY_AMT")]
        public decimal? Payment { get; set; }

        [Column("CR_AMT")]
        public decimal? Credit { get; set; }

        [Column("ADJ_AMT")]
        public decimal? Adjustment { get; set; }

        [Column("INV_BAL")]
        public decimal? InvoiceBalance { get; set; }

        [Column("SEND_STMT")]
        public string SendStatement { get; set; }

        [Column("CLM_RUN_DT")]
        public DateTime? ClaimRunDate { get; set; }

        [Column("PAY_CAT")]
        public string PayCategory { get; set; }

        [Column("FSC")]
        public string Fsc { get; set; }
    }
}
