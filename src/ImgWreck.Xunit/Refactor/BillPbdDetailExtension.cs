using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text.RegularExpressions;
using PortalSvc.Data;

namespace PortalSvc.Data
{
    public static class BillPbdDetailExtension
    {
        public static BillPbdDetail ToPbdDetail(this List<BillPbdDetailDb> bills)
        {
            var pbdDetail = new BillPbdDetail();
            var invoice = new BillPbdInvoice();
            var fscexclusion = ("CL1|CL2|CL3|EC1|EC2|EC3").Split('|');
            var provreg = new Regex("(.{2,15}),(.{2,15})");

            bills.Where(x => x.PayCode != null && !fscexclusion.Contains(x.Fsc))
                .OrderBy(x => x.InvoiceNumber).ThenBy(x => x.TransactionNumber)
                .ToList().ForEach(x =>
                {
                    // start a new invoice
                    if (x.InvoiceNumber != invoice.InvoiceNumber)
                    {
                        if (invoice.InvoiceNumber != 0)
                        {
                            pbdDetail.Invoices.Add(invoice);
                        }

                        invoice = new BillPbdInvoice
                        {
                            BillingArea = x.BillingArea,
                            InvoiceNumber = x.InvoiceNumber,
                            Provider = provreg.Replace(x.Provider, "$2 $1"),
                            IsPatientBalance = x.SendStatement.Equals("Y", StringComparison.OrdinalIgnoreCase)
                        };
                    }

                    if (x.PayCode == 99) // charges
                    {
                        invoice.Charges.Add(new BillPbdTransaction
                        {
                            Amount = x.Debit ?? 0,
                            Date = x.ServiceDate,
                            Description = x.Description
                        });
                    }
                    else // payment
                    {
                        if ((x.Credit ?? 0) != 0)
                        {
                            invoice.Payments.Add(new BillPbdTransaction
                            {
                                Amount = -1 * x.Credit,
                                Date = x.PostDate,
                                Description = x.PaycodeName
                            });
                        }
                        else if (x.Payment != null && x.Adjustment != null && (x.Payment != 0 || x.Adjustment != 0))
                        {
                            invoice.Payments.Add(new BillPbdTransaction
                            {
                                Amount = -1 * x.Payment,
                                Date = x.PostDate,
                                Description = x.PaycodeName + " Payment"
                            });

                            invoice.Payments.Add(new BillPbdTransaction
                            {
                                Amount = -1 * x.Adjustment,
                                Date = x.PostDate,
                                Description = x.PaycodeName + " Adjustment"
                            });
                        }
                        else
                        {
                            invoice.Payments.Add(new BillPbdTransaction
                            {
                                Amount = -1 * x.Payment,
                                Date = x.PostDate,
                                Description = (x.PayCategory == "FORM") ? "INSURANCE CLAIM FILED" : x.PaycodeName
                            });
                        }
                    }

                    invoice.Note = (x.ClaimRunDate != null) ? "" :
                        "NOTE: NO INSURANCE CLAIM WAS FILED FOR THIS SERVICE. IF YOU HAVE INSURANCE THAT COVERS THIS SERVICE, PLEASE CONTACT OUR OFFICE.";
                });

            if (invoice != null && invoice.InvoiceNumber != 0)
            {
                pbdDetail.Invoices.Add(invoice);
            }

            return pbdDetail;
        }
    }
}
