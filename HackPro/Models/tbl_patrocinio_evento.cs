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
    
    public partial class tbl_patrocinio_evento
    {
        public int tbl_patrocinio_id { get; set; }
        public int tbl_evento { get; set; }
        public int tbl_patrocinio_evento_tipo_aporte { get; set; }
        public string tbl_patrocinio_evento_desc_aporte { get; set; }
        public bool tbl_patrocinio_evento_activo { get; set; }
    
        public virtual tbl_evento tbl_evento1 { get; set; }
        public virtual tbl_patrocinio tbl_patrocinio { get; set; }
        public virtual tbl_tipo_aporte tbl_tipo_aporte { get; set; }
    }
}
