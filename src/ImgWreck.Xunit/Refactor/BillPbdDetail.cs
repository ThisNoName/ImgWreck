using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace PortalSvc.Data
{
    [ExcludeFromCodeCoverage]
    public class BillPbdTransaction
    {
        public DateTime? Date { get; set; }
        public string Description { get; set; }
        public decimal? Amount { get; set; }
    }

    [ExcludeFromCodeCoverage]
    public class BillPbdInvoice
    {
        public BillPbdInvoice()
        {
            Charges = new List<BillPbdTransaction>();
            Payments = new List<BillPbdTransaction>();
        }

        public int InvoiceNumber { get; set; } = 0;
        public string Provider { get; set; }
        public string BillingArea { get; set; }

        public List<BillPbdTransaction> Charges { get; set; }
        public List<BillPbdTransaction> Payments { get; set; }

        public decimal TotalCharges
        {
            get { return Charges.Sum(x => x.Amount ?? 0); }
        }

        public bool IsPatientBalance { get; set; }

        public decimal InvoiceBalance
        {
            get { return TotalCharges + Payments.Sum(x => x.Amount ?? 0); }
        }

        public string Note { get; set; }

        public string BalanceDescription => IsPatientBalance
            ? (InvoiceBalance > 0 ? "Amount You Owe" : "Credit Balance")
            : ("Amount Pending With Insurance");
    }

    [ExcludeFromCodeCoverage]
    public class BillPbdDetail
    {
        public BillPbdDetail()
        {
            Invoices = new List<BillPbdInvoice>();
        }

        public List<BillPbdInvoice> Invoices { get; set; }

        public decimal InsuranceBalance => Invoices.Where(x =>
            !x.IsPatientBalance).Sum(s => s.InvoiceBalance);

        public decimal PatientBalance => Invoices.Where(x =>
            x.IsPatientBalance).Sum(s => s.InvoiceBalance);

        public decimal AccountBalance => InsuranceBalance + PatientBalance;
    }
}
