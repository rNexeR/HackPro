using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HackPro.Models
{
    public class Cat_Evento
    {
        public int id { get; set; }

        [Required(ErrorMessage="Campo Requerido")]
        [DisplayName("Descripcion")]
        public string tbl_cat_evento_desc { get; set; }
    }
}