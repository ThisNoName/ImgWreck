using System;

namespace PortalSvc.Data
{
    public class BillStatement
    {
        /// <summary>
        /// Physician: 20160922_04.pdf_MSKA
        /// Hospital: 36119464_20180628_2.pdf; 36119464_20180628_2D.pdf; 
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Physician: Empty
        /// Hospital: Summary, Detail
        /// </summary>
        public string IdType { get; set; }

        public DateTime? BillDate { get; set; }

        /// <summary>
        /// Possible multiple bills on same day
        /// </summary>
        public string Group { get; set; }

        /// <summary>
        /// MSKA: Physician Bills
        /// MSKH: Hospital Bills
        /// </summary>
        public string BillType { get; set; }
    }
}
