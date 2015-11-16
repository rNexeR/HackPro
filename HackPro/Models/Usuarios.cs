using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HackPro.Models
{
    public class Usuarios
    {
        [Required(ErrorMessage = "Campo Requerido")]
        [RegularExpression(".+\\@.+\\..+", ErrorMessage = "Correo de formato inválido.")]
        public string correo { get; set; }

        [Required(ErrorMessage = "Campo Requerido")]
        public string p_nombre { get; set; }

        public string s_nombre { get; set; }

        [Required(ErrorMessage = "Campo Requerido")]
        public string p_apellido { get; set; }

        public string s_apellido { get; set; }

        [Required(ErrorMessage = "Campo Requerido")]
        public string password { get; set; }

        [Required(ErrorMessage = "Campo Requerido")]
        public string r_password { get; set; }

        [Required(ErrorMessage = "Campo Requerido")]
        public string username { get; set; }

        [Required(ErrorMessage = "Campo Requerido")]
        public string genero { get; set; }

        [Required(ErrorMessage = "Campo Requerido")]
        public string ocupacion { get; set; }

        public System.DateTime fecha_nac { get; set; }
    }
}