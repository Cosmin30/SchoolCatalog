using System.ComponentModel.DataAnnotations;

namespace SchoolCatalog.Server.Dtos
{
    public class TemaDto
    {
        public int IdTema { get; set; }
        public string? Descriere { get; set; }
        public DateTime TermenLimita { get; set; }
        public int IdMaterie { get; set; }
        public string? NumeMaterie { get; set; }
        public int IdClasa { get; set; }
        public string? NumeClasa { get; set; }
    }

    public class CreateTemaDto
    {
        [Required(ErrorMessage = "Descrierea este obligatorie.")]
        public string? Descriere { get; set; }

        [Required(ErrorMessage = "Termenul limit? este obligatoriu.")]
        [DataType(DataType.Date)]
        public DateTime TermenLimita { get; set; }

        [Required(ErrorMessage = "ID-ul materiei este obligatoriu.")]
        public int IdMaterie { get; set; }

        [Required(ErrorMessage = "ID-ul clasei este obligatoriu.")]
        public int IdClasa { get; set; }
    }

    public class UpdateTemaDto
    {
        [Required(ErrorMessage = "Descrierea este obligatorie.")]
        public string? Descriere { get; set; }

        [Required(ErrorMessage = "Termenul limit? este obligatoriu.")]
        [DataType(DataType.Date)]
        public DateTime TermenLimita { get; set; }

        [Required(ErrorMessage = "ID-ul materiei este obligatoriu.")]
        public int IdMaterie { get; set; }

        [Required(ErrorMessage = "ID-ul clasei este obligatoriu.")]
        public int IdClasa { get; set; }
    }

    public class UpdateTermenLimitaDto
    {
        [Required(ErrorMessage = "Termenul limit? este obligatoriu.")]
        [DataType(DataType.Date)]
        public DateTime TermenLimita { get; set; }
    }
}
