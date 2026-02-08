using System.ComponentModel.DataAnnotations;

namespace SchoolCatalog.Server.Dtos
{
    public class FisierTemaDto
    {
        public int IdFisier { get; set; }
        public string NumeFisier { get; set; } = string.Empty;
        public string UrlFisier { get; set; } = string.Empty;
        public int IdElev { get; set; }
        public string? NumeElev { get; set; }
        public string? PrenumeElev { get; set; }
        public int TemaId { get; set; }
        public string? DescriereTeam { get; set; }
        public DateTime DataIncarcare { get; set; }
    }

    public class CreateFisierTemaDto
    {
        [Required(ErrorMessage = "Numele fi?ierului este obligatoriu")]
        [StringLength(255, ErrorMessage = "Numele fi?ierului nu poate dep??i 255 de caractere.")]
        public string NumeFisier { get; set; } = string.Empty;

        [Required(ErrorMessage = "URL-ul fi?ierului este obligatoriu")]
        [Url(ErrorMessage = "URL-ul fi?ierului este invalid.")]
        public string UrlFisier { get; set; } = string.Empty;

        [Required(ErrorMessage = "ID-ul elevului este obligatoriu")]
        public int IdElev { get; set; }

        [Required(ErrorMessage = "ID-ul temei este obligatoriu")]
        public int TemaId { get; set; }
    }

    public class UpdateFisierTemaDto
    {
        [Required(ErrorMessage = "Numele fi?ierului este obligatoriu")]
        [StringLength(255, ErrorMessage = "Numele fi?ierului nu poate dep??i 255 de caractere.")]
        public string NumeFisier { get; set; } = string.Empty;

        [Required(ErrorMessage = "URL-ul fi?ierului este obligatoriu")]
        [Url(ErrorMessage = "URL-ul fi?ierului este invalid.")]
        public string UrlFisier { get; set; } = string.Empty;

        [Required(ErrorMessage = "ID-ul elevului este obligatoriu")]
        public int IdElev { get; set; }

        [Required(ErrorMessage = "ID-ul temei este obligatoriu")]
        public int TemaId { get; set; }
    }
}
