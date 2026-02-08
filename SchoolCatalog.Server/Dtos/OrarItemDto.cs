using System.ComponentModel.DataAnnotations;

namespace SchoolCatalog.Server.Dtos
{
    public class OrarItemDto
    {
        public int IdOrarItem { get; set; }
        public string? ZiSaptamana { get; set; }
        public string? OraInceput { get; set; }
        public string? OraSfarsit { get; set; }
        public int IdMaterie { get; set; }
        public string? NumeMaterie { get; set; }
        public int IdProfesor { get; set; }
        public string? NumeProfesor { get; set; }
        public string? PrenumeProfesor { get; set; }
        public int IdOrar { get; set; }
    }

    public class CreateOrarItemDto
    {
        [Required(ErrorMessage = "Ziua saptamanii este obligatorie")]
        [StringLength(10, ErrorMessage = "Ziua saptamanii nu poate depasi 10 caractere")]
        public string? ZiSaptamana { get; set; }

        [Required(ErrorMessage = "Ora inceput este obligatorie")]
        [StringLength(5, ErrorMessage = "Ora inceput nu poate depasi 5 caractere")]
        public string? OraInceput { get; set; }

        [Required(ErrorMessage = "Ora sfarsit este obligatorie")]
        [StringLength(5, ErrorMessage = "Ora sfarsit nu poate depasi 5 caractere")]
        public string? OraSfarsit { get; set; }

        [Required(ErrorMessage = "ID-ul materiei este obligatoriu")]
        public int IdMaterie { get; set; }

        [Required(ErrorMessage = "ID-ul profesorului este obligatoriu")]
        public int IdProfesor { get; set; }

        [Required(ErrorMessage = "ID-ul orarului este obligatoriu")]
        public int IdOrar { get; set; }
    }

    public class UpdateOrarItemDto
    {
        [Required(ErrorMessage = "Ziua saptamanii este obligatorie")]
        [StringLength(10, ErrorMessage = "Ziua saptamanii nu poate depasi 10 caractere")]
        public string? ZiSaptamana { get; set; }

        [Required(ErrorMessage = "Ora inceput este obligatorie")]
        [StringLength(5, ErrorMessage = "Ora inceput nu poate depasi 5 caractere")]
        public string? OraInceput { get; set; }

        [Required(ErrorMessage = "Ora sfarsit este obligatorie")]
        [StringLength(5, ErrorMessage = "Ora sfarsit nu poate depasi 5 caractere")]
        public string? OraSfarsit { get; set; }

        [Required(ErrorMessage = "ID-ul materiei este obligatoriu")]
        public int IdMaterie { get; set; }

        [Required(ErrorMessage = "ID-ul profesorului este obligatoriu")]
        public int IdProfesor { get; set; }

        [Required(ErrorMessage = "ID-ul orarului este obligatoriu")]
        public int IdOrar { get; set; }
    }
}
