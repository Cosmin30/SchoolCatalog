using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolCatalog.Server.Model
{
    public class Media
    {
        [Key]
        public int IdMedie { get; set; }
        [Required]
        [ForeignKey(nameof(Elev))]

        public int IdElev { get; set; }
        public Elev? Elev { get; set; } 
        [Required]
        [ForeignKey(nameof(Materie))]
        public int IdMaterie { get; set; }
        public Materie? Materie { get; set; }
        [Range(1, 10, ErrorMessage = "Media trebuie să fie între 1 și 10")]
        public double Valoare { get; set; }

    }
}
