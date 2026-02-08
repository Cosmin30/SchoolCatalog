using System.ComponentModel.DataAnnotations;

namespace SchoolCatalog.Server.Dtos
{
    public class ElevDto
    {
        public int IdElev { get; set; }
        public string NumeElev { get; set; } = string.Empty;
        public string PrenumeElev { get; set; } = string.Empty;
        public DateTime DataNasterii { get; set; }
        public int? ClasaId { get; set; }
        public string? NumeClasa { get; set; }
        public string? ProfilClasa { get; set; }
    }

    public class CreateElevDto
    {
        [Required(ErrorMessage = "Numele este obligatoriu")]
        [MinLength(2, ErrorMessage = "Numele elevului trebuie sa aiba minim 2 caractere")]
        [MaxLength(50, ErrorMessage = "Numele elevului trebuie sa aiba maxim 50 de caractere")]
        [RegularExpression(@"^(?=.{1,50}$)[a-zA-Z]+(?:[-'\s][a-zA-Z]+)*$",
            ErrorMessage = "Numele elevului trebuie sa contina doar litere si sau -")]
        public string NumeElev { get; set; } = string.Empty;

        [Required(ErrorMessage = "Prenumele este obligatoriu")]
        [MinLength(2, ErrorMessage = "Prenumele trebuie sa aiba minim 2 de caractere")]
        [MaxLength(50, ErrorMessage = "Prenumele trebuie sa aiba maxim 50 de caractere")]
        [RegularExpression(@"^(?=.{1,50}$)[A-Z]([a-z]+)+(?:[-'\s][a-zA-Z]+)*$",
            ErrorMessage = "Prenumele trebuie s? înceap? cu majuscul? ?i s? con?in? doar litere ?i/sau: ' - ")]
        public string PrenumeElev { get; set; } = string.Empty;

        [Required(ErrorMessage = "Data na?terii este obligatorie")]
        [DataType(DataType.Date)]
        public DateTime DataNasterii { get; set; }

        public int? ClasaId { get; set; }
    }

    public class UpdateElevDto
    {
        [Required(ErrorMessage = "Numele este obligatoriu")]
        [MinLength(2, ErrorMessage = "Numele elevului trebuie sa aiba minim 2 caractere")]
        [MaxLength(50, ErrorMessage = "Numele elevului trebuie sa aiba maxim 50 de caractere")]
        [RegularExpression(@"^(?=.{1,50}$)[a-zA-Z]+(?:[-'\s][a-zA-Z]+)*$",
            ErrorMessage = "Numele elevului trebuie sa contina doar litere si sau -")]
        public string NumeElev { get; set; } = string.Empty;

        [Required(ErrorMessage = "Prenumele este obligatoriu")]
        [MinLength(2, ErrorMessage = "Prenumele trebuie sa aiba minim 2 de caractere")]
        [MaxLength(50, ErrorMessage = "Prenumele trebuie sa aiba maxim 50 de caractere")]
        [RegularExpression(@"^(?=.{1,50}$)[A-Z]([a-z]+)+(?:[-'\s][a-zA-Z]+)*$",
            ErrorMessage = "Prenumele trebuie s? înceap? cu majuscul? ?i s? con?in? doar litere ?i/sau: ' - ")]
        public string PrenumeElev { get; set; } = string.Empty;

        [Required(ErrorMessage = "Data na?terii este obligatorie")]
        [DataType(DataType.Date)]
        public DateTime DataNasterii { get; set; }

        public int? ClasaId { get; set; }
    }
}
