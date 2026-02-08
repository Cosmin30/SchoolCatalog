using System.ComponentModel.DataAnnotations;

namespace SchoolCatalog.Server.Dtos
{
    public class OrarDto
    {
        public int IdOrar { get; set; }
        public string? DescriereOrar { get; set; }
        public string? AnScolar { get; set; }
        public int IdClasa { get; set; }
        public string? NumeClasa { get; set; }
    }

    public class CreateOrarDto
    {
        public string? DescriereOrar { get; set; }
        public string? AnScolar { get; set; }

        [Required(ErrorMessage = "ID-ul clasei este obligatoriu")]
        public int IdClasa { get; set; }
    }

    public class UpdateOrarDto
    {
        public string? DescriereOrar { get; set; }
        public string? AnScolar { get; set; }

        [Required(ErrorMessage = "ID-ul clasei este obligatoriu")]
        public int IdClasa { get; set; }
    }
}
