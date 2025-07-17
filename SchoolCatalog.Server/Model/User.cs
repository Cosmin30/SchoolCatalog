using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolCatalog.Server.Model
{
    public class User
    {
        [Key]
        public int IdUser{ get; set; }
        [Required(ErrorMessage = "E-mailul este obligatoriu")]
        [EmailAddress(ErrorMessage = "E-mailul nu este valid")]
        public string Email { get; set; } = string.Empty;
        [Required(ErrorMessage = "Parola este obligatorie")]
        [StringLength(100, ErrorMessage = "Parola trebuie sa aiba minim 6 caractere", MinimumLength = 6)]
        public string Parola { get; set; } = string.Empty;
        [Required(ErrorMessage ="Rolul este obligatoriu")]
        [RegularExpression("^(Elev|Profesor)$", ErrorMessage = "Rolul trebuie sa fie 'Elev' sau 'Profesor'")]
        public string Rol { get; set; } = string.Empty;
        [ForeignKey(nameof(Elev))]
        public int? IdElev { get; set; }
        public Elev? Elev { get; set; }
        [ForeignKey(nameof(Profesor))]
        public int? IdProfesor { get; set; }
        public Profesor? Profesor { get; set; }


    }
}
