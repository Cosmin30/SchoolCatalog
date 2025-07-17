using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolCatalog.Server.Model
{
    public class Nota
    {
        [Key]
        public int IdNota { get; set; }
        [Required,Range(1,10)]
        public int Valoare { get; set; }
        [Required,DataType(DataType.Date)]
        public DateTime DataNotei { get; set; }  
        public bool EsteAnulata { get; set; } = false;
        [ForeignKey(nameof(Elev))]
        public int IdElev { get; set; }
        public Elev Elev { get; set; } = null!;
        [ForeignKey(nameof(Materie))]
        public int IdMaterie { get; set; }
        public Materie Materie { get; set; } = null!;       
    }
}
