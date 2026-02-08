using System.ComponentModel.DataAnnotations;

namespace SchoolCatalog.Server.Dtos
{
    public class ClasaDto
    {
        public int IdClasa { get; set; }
        public string NumeClasa { get; set; } = string.Empty;
        public string ProfilClasa { get; set; } = string.Empty;
        public int? IdOrar { get; set; }
    }

    public class CreateClasaDto
    {
        [Required(ErrorMessage = "Numele clasei este obligatoriu")]
        [MinLength(1, ErrorMessage = "Numele clasei este obligatoriu")]
        [MaxLength(20, ErrorMessage = "Numele clasei trebuie sa aiba maxim 20 caractere")]
        public string NumeClasa { get; set; } = string.Empty;

        [Required(ErrorMessage = "Profilul clasei este obligatoriu")]
        [MinLength(1, ErrorMessage = "Profilul clasei este obligatoriu")]
        [MaxLength(50, ErrorMessage = "Profilul clasei trebuie sa aiba maxim 50 caractere")]
        public string ProfilClasa { get; set; } = string.Empty;
    }

    public class UpdateClasaDto
    {
        [Required(ErrorMessage = "Numele clasei este obligatoriu")]
        [MinLength(1, ErrorMessage = "Numele clasei este obligatoriu")]
        [MaxLength(20, ErrorMessage = "Numele clasei trebuie sa aiba maxim 20 caractere")]
        public string NumeClasa { get; set; } = string.Empty;

        [Required(ErrorMessage = "Profilul clasei este obligatoriu")]
        [MinLength(1, ErrorMessage = "Profilul clasei este obligatoriu")]
        [MaxLength(50, ErrorMessage = "Profilul clasei trebuie sa aiba maxim 50 caractere")]
        public string ProfilClasa { get; set; } = string.Empty;
    }
}
