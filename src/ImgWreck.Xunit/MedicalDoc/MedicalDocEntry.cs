using PortalSvc.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PortalSvc.Data
{
    /// <summary>
    /// Base class for all MedicalDoc entries.
    /// </summary>
    public class MedicalDocEntry
    {
        // override
        public virtual string DocId => $"MedicalDocEntry_{Id}";
        // DB:SubCategory; Svc:TypeDescription; UI:Document Type; 
        public virtual string TypeDescription { get; set; }
        public virtual string ContentType { get; set; }
        // DB:DocumentName; Svc:Description; UI:Description
        public virtual string Description { get; set; }
        // override

        public virtual string Id { get; set; }

        public virtual DateTime EntryDate { get; set; }
        public byte[] Content { get; set; }

        public static List<string> AllFileDocTypes =>
            typeof(MedicalDocTypeEnum).GetEnumNames().ToList();
    }
}

