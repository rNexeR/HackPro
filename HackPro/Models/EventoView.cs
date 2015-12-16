using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HackPro.Models
{
    public class EventoView
    {
        public int id { get; set; }
        
        [DisplayName("Nombre de Evento")]
        public string tbl_evento_nombre { get; set; }
        
        [DisplayName("Descripcion")]
        public string tbl_evento_desc { get; set; }
        
        [DisplayName("Lugar")]
        public string tbl_evento_lugar { get; set; }
        
        [DisplayName("Coordenada X")]
        public decimal tbl_evento_lugar_x { get; set; }
        
        [DisplayName("Coordenada Y")]
        public decimal tbl_evento_lugar_y { get; set; }
        
        [DisplayName("Duracion (hrs)")]
        public int tbl_evento_duracion { get; set; }
        
        [DisplayName("Fecha Inicio")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public string tbl_evento_fecha_inicio { get; set; }

        [Required(ErrorMessage = "Campo Requerido")]
        [DisplayName("Fecha Finalizacion")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public string tbl_evento_fecha_fin { get; set; }
        
        [DisplayName("Presupuesto")]
        public decimal tbl_evento_presupuesto { get; set; }

        [DisplayName("URL Evento")]
        public string tbl_evento_url { get; set; }
        
        [DisplayName("Cal. Jurado")]
        public bool tbl_evento_cal_jurado { get; set; }

        [Required(ErrorMessage = "Campo Requerido")]
        [DisplayName("Cal. Pueblo")]
        public bool tbl_evento_cal_pueblo { get; set; }

        [Required(ErrorMessage = "Campo Requerido")]
        [DisplayName("Precio")]
        public decimal tbl_evento_precio { get; set; }

        [DisplayName("Categoria")]
        public string tbl_cat_evento { get; set; }

        public int usuarios_registrados { get; set; }
        public int equipos_registrados { get; set; }
        public int jurados_registrados { get; set; }
        public int proyectos_registrados { get; set; }
    }
}