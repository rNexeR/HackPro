using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HackPro.Models
{
    public class TipoAporte
    {
        [Required(ErrorMessage = "Campo Requerido")]
        [DisplayName("Descripcion")]
        public string tbl_tipo_aporte_desc { get; set; }
    }
}