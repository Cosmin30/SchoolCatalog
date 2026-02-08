using System.ComponentModel.DataAnnotations;

namespace SchoolCatalog.Server.Dtos
{
    public class ProfesorDto
    {
        public int IdProfesor { get; set; }
        public string NumeProfesor { get; set; } = string.Empty;
        public string PrenumeProfesor { get; set; } = string.Empty;
        public string EmailProfesor { get; set; } = string.Empty;
        public DateTime DataNasterii { get; set; }
    }

    public class CreateProfesorDto
    {
        [Required(ErrorMessage = "Numele este obligatoriu")]
        [MinLength(2, ErrorMessage = "Numele profesorului trebuie sa aiba minim 2 caractere")]
        [MaxLength(100, ErrorMessage = "Numele profesorului trebuie sa aiba maxim 100 de caractere")]
        public string NumeProfesor { get; set; } = string.Empty;

        [Required(ErrorMessage = "Prenumele este obligatoriu")]
        [MinLength(2, ErrorMessage = "Prenumele trebuie sa aiba minim 2 de caractere")]
        [MaxLength(100, ErrorMessage = "Prenumele trebuie sa aiba maxim 100 de caractere")]
        public string PrenumeProfesor { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email-ul este obligatoriu")]
        [EmailAddress(ErrorMessage = "Email-ul nu este valid")]
        public string EmailProfesor { get; set; } = string.Empty;

        [Required(ErrorMessage = "Data na?terii este obligatorie")]
        [DataType(DataType.Date)]
        public DateTime DataNasterii { get; set; }
    }

    public class UpdateProfesorDto
    {
        [Required(ErrorMessage = "Numele este obligatoriu")]
        [MinLength(2, ErrorMessage = "Numele profesorului trebuie sa aiba minim 2 caractere")]
        [MaxLength(100, ErrorMessage = "Numele profesorului trebuie sa aiba maxim 100 de caractere")]
        public string NumeProfesor { get; set; } = string.Empty;

        [Required(ErrorMessage = "Prenumele este obligatoriu")]
        [MinLength(2, ErrorMessage = "Prenumele trebuie sa aiba minim 2 de caractere")]
        [MaxLength(100, ErrorMessage = "Prenumele trebuie sa aiba maxim 100 de caractere")]
        public string PrenumeProfesor { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email-ul este obligatoriu")]
        [EmailAddress(ErrorMessage = "Email-ul nu este valid")]
        public string EmailProfesor { get; set; } = string.Empty;

        [Required(ErrorMessage = "Data na?terii este obligatorie")]
        [DataType(DataType.Date)]
        public DateTime DataNasterii { get; set; }
    }
}

