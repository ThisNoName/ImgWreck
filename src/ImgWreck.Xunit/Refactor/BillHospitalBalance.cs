using System;

namespace PortalSvc.Data
{
    public class BillHospitalBalance
    {
        private DateTime? _statementdate;
        public DateTime? StatementDate
        {
            get => _statementdate < new DateTime(1980, 1, 1) ? null : _statementdate;
            set => _statementdate = value;
        }

        public decimal LastStatementBalance { get; set; }
        public decimal LastStatementPayment { get; set; }
        public decimal CurrentBalance { get; set; }
        public decimal LastPayment { get; set; }
        public decimal LastPaymentAmount { get; set; }

        private DateTime? _lastpaymentdate;

        public DateTime? LastPaymentDate
        {
            get => _lastpaymentdate < new DateTime(1980, 1, 1) ? null : _lastpaymentdate;
            set => _lastpaymentdate = value;
        }

        public DateTime? ReadDate { get; set; }

        /// <summary>
        /// First Read Date
        /// </summary>
        public DateTime? ReadDate0 { get; set; }
    }
}
