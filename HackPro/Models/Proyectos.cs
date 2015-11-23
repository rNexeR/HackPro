using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HackPro.Models
{
    public class Proyectos
    {
        [Required(ErrorMessage = "Campo Requerido")]
        [DisplayName("Nombre")]
        public string tbl_proyecto_nombre { get; set; }
        [Required(ErrorMessage = "Campo Requerido")]
        [DisplayName("Equipo")]
        public int tbl_equipo_id { get; set; }
        [Required(ErrorMessage = "Campo Requerido")]
        [DisplayName("Evento")]
        public int tbl_evento_id { get; set; }
        [DisplayName("URL")]
        public string tbl_proyecto_url { get; set; }
        [DisplayName("Repositorio")]
        public string tbl_proyecto_git { get; set; }
    }
}