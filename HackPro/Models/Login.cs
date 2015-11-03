using System.ComponentModel.DataAnnotations;

namespace HackPro.Models
{
    public class Login
    {
        [Required(ErrorMessage = "Llenar el campo de email.")]
        [RegularExpression(".+\\@.+\\..+", ErrorMessage = "Correo de formato inválido.")]
        public string email { get; set; }

        [Required(ErrorMessage = "El campo de password es requerido.")]        
        public string password { get; set; }
    }
}