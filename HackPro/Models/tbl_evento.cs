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
    
    public partial class tbl_evento
    {
        public tbl_evento()
        {
            this.tbl_calificacion = new HashSet<tbl_calificacion>();
            this.tbl_comentario_evento = new HashSet<tbl_comentario_evento>();
            this.tbl_equipo_evento = new HashSet<tbl_equipo_evento>();
            this.tbl_jurado = new HashSet<tbl_jurado>();
            this.tbl_patrocinio_evento = new HashSet<tbl_patrocinio_evento>();
            this.tbl_proyecto = new HashSet<tbl_proyecto>();
        }
    
        public int tbl_evento_id { get; set; }
        public string tbl_evento_nombre { get; set; }
        public string tbl_evento_desc { get; set; }
        public string tbl_evento_lugar { get; set; }
        public int tbl_evento_lugar_x { get; set; }
        public int tbl_evento_lugar_y { get; set; }
        public bool tbl_evento_activo { get; set; }
        public int tbl_evento_duracion { get; set; }
        public System.DateTime tbl_evento_fecha_inicio { get; set; }
        public System.DateTime tbl_evento_fecha_fin { get; set; }
        public int tbl_usuario_id { get; set; }
        public int tbl_cat_evento_id { get; set; }
        public decimal tbl_evento_presupuesto { get; set; }
        public string tbl_evento_url { get; set; }
        public bool tbl_evento_cal_jurado { get; set; }
        public bool tbl_evento_cal_pueblo { get; set; }
        public decimal tbl_evento_precio { get; set; }
    
        public virtual ICollection<tbl_calificacion> tbl_calificacion { get; set; }
        public virtual tbl_cat_evento tbl_cat_evento { get; set; }
        public virtual ICollection<tbl_comentario_evento> tbl_comentario_evento { get; set; }
        public virtual ICollection<tbl_equipo_evento> tbl_equipo_evento { get; set; }
        public virtual ICollection<tbl_jurado> tbl_jurado { get; set; }
        public virtual ICollection<tbl_patrocinio_evento> tbl_patrocinio_evento { get; set; }
        public virtual ICollection<tbl_proyecto> tbl_proyecto { get; set; }
    }
}
