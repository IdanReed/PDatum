using System;
using System.Collections.Generic;

namespace DataSinkProject
{
    public partial class DatumAttrib
    {
        public int PkDatumAttribId { get; set; }
        public string Tag { get; set; }
        public string Value { get; set; }
        public int FkDatumId { get; set; }

        public virtual Datum FkDatum { get; set; }
    }
}
