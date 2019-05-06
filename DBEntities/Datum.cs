using System;
using System.Collections.Generic;

namespace DBEntities
{
    public partial class Datum
    {
        public Datum()
        {
            DatumAttrib = new HashSet<DatumAttrib>();
        }

        public int PkDatumId { get; set; }

        public virtual ICollection<DatumAttrib> DatumAttrib { get; set; }
    }
}
