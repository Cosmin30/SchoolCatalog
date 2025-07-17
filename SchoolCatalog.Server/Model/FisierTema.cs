using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolCatalog.Server.Model
{
    public class FisierTema
    {
        [Key]
        public int IdFisier { get; set; }
        [Required]
        [StringLength(255, ErrorMessage = "Numele fișierului nu poate depăși 255 de caractere.")]
        public string NumeFisier { get; set; } = string.Empty;
        [Required]
        [Url(ErrorMessage = "URL-ul fișierului este invalid.")]
        public string UrlFisier { get; set; } = string.Empty;
        [ForeignKey(nameof(Elev))]
        public int IdElev { get; set; }
        public Elev Elev { get; set; } = null!;
        [ForeignKey(nameof(Tema))]
        public int TemaId { get; set; }
        public Tema Tema { get; set; } = null!;
        public DateTime DataIncarcare { get; set; } = DateTime.UtcNow;
    }
}
