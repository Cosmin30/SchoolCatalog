using System.ComponentModel.DataAnnotations;

namespace SchoolCatalog.Server.Dtos
{
    public class NotaDto
    {
        public int IdNota { get; set; }
        public int Valoare { get; set; }
        public DateTime DataNotei { get; set; }
        public bool EsteAnulata { get; set; }
        public int IdElev { get; set; }
        public string? NumeElev { get; set; }
        public string? PrenumeElev { get; set; }
        public int IdMaterie { get; set; }
        public string? NumeMaterie { get; set; }
    }

    public class CreateNotaDto
    {
        [Required(ErrorMessage = "Valoarea notei este obligatorie")]
        [Range(1, 10, ErrorMessage = "Nota trebuie s? fie între 1 ?i 10")]
        public int Valoare { get; set; }

        [Required(ErrorMessage = "Data notei este obligatorie")]
        [DataType(DataType.Date)]
        public DateTime DataNotei { get; set; }

        [Required(ErrorMessage = "ID-ul elevului este obligatoriu")]
        public int IdElev { get; set; }

        [Required(ErrorMessage = "ID-ul materiei este obligatoriu")]
        public int IdMaterie { get; set; }
    }

    public class UpdateNotaDto
    {
        [Required(ErrorMessage = "Valoarea notei este obligatorie")]
        [Range(1, 10, ErrorMessage = "Nota trebuie s? fie între 1 ?i 10")]
        public int Valoare { get; set; }

        [Required(ErrorMessage = "Data notei este obligatorie")]
        [DataType(DataType.Date)]
        public DateTime DataNotei { get; set; }

        public bool EsteAnulata { get; set; }

        [Required(ErrorMessage = "ID-ul elevului este obligatoriu")]
        public int IdElev { get; set; }

        [Required(ErrorMessage = "ID-ul materiei este obligatoriu")]
        public int IdMaterie { get; set; }
    }
}
