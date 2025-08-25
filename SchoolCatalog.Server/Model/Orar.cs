using System.ComponentModel.DataAnnotations;

namespace SchoolCatalog.Server.Model
{
    public class Orar
    {
        [Key]
        public int IdOrar { get; set; }
        public string? DescriereOrar { get; set; }
        public string? AnScolar { get; set; }
        [Required(ErrorMessage = "Clasa este obligatorie")]
        public int IdClasa { get; set; }
        public Clasa Clasa { get; set; } = null!;
        public ICollection<OrarItem> OrarItems { get; set; } = new List<OrarItem>();  

    }
}
