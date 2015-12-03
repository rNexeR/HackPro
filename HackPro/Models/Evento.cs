using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HackPro.Models
{
    public class Evento
    {
        [Required(ErrorMessage = "Campo Requerido")]
        [DisplayName("Nombre de Evento")]
        public string tbl_evento_nombre { get; set; }

        [Required(ErrorMessage = "Campo Requerido")]
        [DisplayName("Descripcion")]
        public string tbl_evento_desc { get; set; }

        [Required(ErrorMessage = "Campo Requerido")]
        [DisplayName("Lugar")]
        public string tbl_evento_lugar { get; set; }

        [Required(ErrorMessage = "Campo Requerido")]
        [DisplayName("Coordenada X")]
        public int tbl_evento_lugar_x { get; set; }

        [Required(ErrorMessage = "Campo Requerido")]
        [DisplayName("Coordenada Y")]
        public int tbl_evento_lugar_y { get; set; }

        [Required(ErrorMessage = "Campo Requerido")]
        [DisplayName("Duracion (hrs)")]
        public int tbl_evento_duracion { get; set; }

        [Required(ErrorMessage = "Campo Requerido")]
        [DisplayName("Inicio")]
        public System.DateTime tbl_evento_fecha_inicio { get; set; }

        [Required(ErrorMessage = "Campo Requerido")]
        [DisplayName("Fin")]
        public System.DateTime tbl_evento_fecha_fin { get; set; }

        [Required(ErrorMessage = "Campo Requerido")]
        [DisplayName("Categoria")]
        public int tbl_cat_evento { get; set; }
        public IEnumerable<SelectListItem> cat_evento { get; set; }
        

        [Required(ErrorMessage = "Campo Requerido")]
        [DisplayName("Presupuesto")]
        public decimal tbl_evento_presupuesto { get; set; }

        [DisplayName("URL Evento")]
        public string tbl_evento_url { get; set; }

        [Required(ErrorMessage = "Campo Requerido")]
        [DisplayName("Cal. Jurado")]
        public bool tbl_evento_cal_jurado { get; set; }

        [Required(ErrorMessage = "Campo Requerido")]
        [DisplayName("Cal. Pueblo")]
        public bool tbl_evento_cal_pueblo { get; set; }

        [Required(ErrorMessage = "Campo Requerido")]
        [DisplayName("Precio")]
        public decimal tbl_evento_precio { get; set; }
    }
}