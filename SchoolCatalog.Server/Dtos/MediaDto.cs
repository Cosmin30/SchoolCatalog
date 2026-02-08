using System.ComponentModel.DataAnnotations;

namespace SchoolCatalog.Server.Dtos
{
    public class MediaDto
    {
        public int IdMedie { get; set; }
        public int IdElev { get; set; }
        public string? NumeElev { get; set; }
        public string? PrenumeElev { get; set; }
        public int IdMaterie { get; set; }
        public string? NumeMaterie { get; set; }
        public double Valoare { get; set; }
    }

    public class CreateMediaDto
    {
        [Required(ErrorMessage = "ID-ul elevului este obligatoriu")]
        public int IdElev { get; set; }

        [Required(ErrorMessage = "ID-ul materiei este obligatoriu")]
        public int IdMaterie { get; set; }

        [Required(ErrorMessage = "Valoarea mediei este obligatorie")]
        [Range(1, 10, ErrorMessage = "Media trebuie s? fie între 1 ?i 10")]
        public double Valoare { get; set; }
    }

    public class UpdateMediaDto
    {
        [Required(ErrorMessage = "ID-ul elevului este obligatoriu")]
        public int IdElev { get; set; }

        [Required(ErrorMessage = "ID-ul materiei este obligatoriu")]
        public int IdMaterie { get; set; }

        [Required(ErrorMessage = "Valoarea mediei este obligatorie")]
        [Range(1, 10, ErrorMessage = "Media trebuie s? fie între 1 ?i 10")]
        public double Valoare { get; set; }
    }
}
