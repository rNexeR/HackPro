//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HackPro.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbl_calificacion
    {
        public int tbl_evento { get; set; }
        public int tbl_proyecto { get; set; }
        public Nullable<int> tbl_calificacion_jurado { get; set; }
        public Nullable<int> tbl_calificacion_pueblo { get; set; }
    
        public virtual tbl_evento tbl_evento1 { get; set; }
        public virtual tbl_proyecto tbl_proyecto1 { get; set; }
    }
}
