using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HackPro.Models
{
    public class Usuarios
    {
        public int id { get; set; }
        public bool admin { get; set; }

        [Required(ErrorMessage = "Campo Requerido")]
        [RegularExpression(".+\\@.+\\..+", ErrorMessage = "Correo de formato inválido.")]
        [DisplayName("E-Mail*")]
        public string correo { get; set; }

        [Required(ErrorMessage = "Campo Requerido")]
        [DisplayName("Primer Nombre*")]
        public string p_nombre { get; set; }

        [DisplayName("Segundo Nombre")]
        public string s_nombre { get; set; }

        [Required(ErrorMessage = "Campo Requerido")]
        [DisplayName("Primer Apellido*")]
        public string p_apellido { get; set; }

        [DisplayName("Segundo Apellido")]
        public string s_apellido { get; set; }

        [Required(ErrorMessage = "Campo Requerido")]
        [DisplayName("Contrasena*")]
        public string password { get; set; }

        [Required(ErrorMessage = "Campo Requerido")]
        [DisplayName("Repita Contrasena*")]
        public string r_password { get; set; }

        [Required(ErrorMessage = "Campo Requerido")]
        [DisplayName("Nombre de Usuario*")]
        public string username { get; set; }

        [Required(ErrorMessage = "Campo Requerido")]
        [DisplayName("Genero*")]
        public string genero { get; set; }

        [Required(ErrorMessage = "Campo Requerido")]
        [DisplayName("Ocupacion*")]
        public string ocupacion { get; set; }

        [Required(ErrorMessage = "Campo Requerido")]
        [DisplayName("Numero Celular*")]
        public string celular { get; set; }

        [DisplayName("Fecha Nacimiento")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public System.DateTime fecha_nac { get; set; }
    }
}