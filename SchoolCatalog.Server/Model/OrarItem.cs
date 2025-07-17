using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolCatalog.Server.Model
{
    public class OrarItem
    {
        [Key]
        public int IdOrarItem { get; set; }
        [Required(ErrorMessage = "Ziua saptamanii este obligatorie")]
        [StringLength(10, ErrorMessage = "Ziua saptamanii nu poate depasi 10 caractere")]
        public string? ZiSaptamana { get; set; }    
        [Required(ErrorMessage = "Ora inceput este obligatorie")]   
        [StringLength(5, ErrorMessage = "Ora inceput nu poate depasi 5 caractere")]
        public string? OraInceput { get; set; }
        [Required(ErrorMessage = "Ora sfarsit este obligatorie")]
        [StringLength(5, ErrorMessage = "Ora sfarsit nu poate depasi 5 caractere")]
        public string? OraSfarsit { get; set; }
        [ForeignKey(nameof(Materie))]
        [Required(ErrorMessage = "Materie este obligatorie")]
        public int IdMaterie { get; set; }
        public Materie Materie { get; set; } = null!;
        [Required(ErrorMessage = "Profesorul este obligatoriu")]
        [ForeignKey(nameof(Profesor))]
        public int IdProfesor { get; set; }
        public Profesor Profesor { get; set; } = null!;
        [ForeignKey(nameof(Orar))]
        [Required(ErrorMessage = "Orar este obligatoriu")]
        public int IdOrar { get; set; }
        public Orar Orar { get; set; } = null!;

    }
}
