using System.ComponentModel.DataAnnotations;

namespace SchoolCatalog.Server.Dtos
{
    public class MaterieDto
    {
        public int IdMaterie { get; set; }
        public string NumeMaterie { get; set; } = string.Empty;
        public int? ProfesorId { get; set; }
        public string? NumeProfesor { get; set; }
        public string? PrenumeProfesor { get; set; }
    }

    public class CreateMaterieDto
    {
        [Required(ErrorMessage = "Numele materiei este obligatoriu")]
        [MinLength(2, ErrorMessage = "Numele materiei trebuie sa aiba minim 2 caractere")]
        [MaxLength(100, ErrorMessage = "Numele materiei trebuie sa aiba maxim 100 caractere")]
        public string NumeMaterie { get; set; } = string.Empty;

        public int? ProfesorId { get; set; }
    }

    public class UpdateMaterieDto
    {
        [Required(ErrorMessage = "Numele materiei este obligatoriu")]
        [MinLength(2, ErrorMessage = "Numele materiei trebuie sa aiba minim 2 caractere")]
        [MaxLength(100, ErrorMessage = "Numele materiei trebuie sa aiba maxim 100 caractere")]
        public string NumeMaterie { get; set; } = string.Empty;

        public int? ProfesorId { get; set; }
    }
}
