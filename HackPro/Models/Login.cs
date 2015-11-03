using System.ComponentModel.DataAnnotations;

namespace HackPro.Models
{
    public class Login
    {
        [Required]
        [RegularExpression(".+\\@.+\\..+")]
        public string email { get; set; }

        [Required]
        public string password { get; set; }
    }
}