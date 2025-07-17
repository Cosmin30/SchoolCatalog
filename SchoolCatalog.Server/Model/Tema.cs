using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolCatalog.Server.Model
{
    public class Tema
    {
        [Key]
        public int IdTema { get; set; }
        public string? Descriere { get; set; }
        [Required, DataType(DataType.Date)]
        public DateTime TermenLimita { get; set; }
        [ForeignKey(nameof(Materie))]
        public int IdMaterie { get; set; }
        public Materie Materie { get; set; } = null!;
        [ForeignKey(nameof(Clasa))]
        public int IdClasa { get; set; }
        public Clasa Clasa { get; set; } = null!;
        public ICollection<FisierTema>? Fisiere { get; set; }



    }

}
