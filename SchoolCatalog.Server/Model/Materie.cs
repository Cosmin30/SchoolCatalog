using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolCatalog.Server.Model
{
    public class Materie
    {
        [Key]
        public int IdMaterie { get; set; }
        [Required(ErrorMessage ="Numele materiei este obligatoriu"),
        MinLength(2,ErrorMessage ="Numele materiei trebuie sa aiba minim 2 caractere"),
        MaxLength(100,ErrorMessage ="Numele materiei trebuie sa aiba maxim 100 caractere")]
        public string NumeMaterie { get; set; }= string.Empty;
        [ForeignKey(nameof(Profesor))]
        public int? ProfesorId { get; set; }
        public Profesor? Profesor { get; set; } 
        public ICollection<Nota> Note { get; set; } = new List<Nota>(); 
        public ICollection<Tema> Teme { get; set; } = new List<Tema>();
        public ICollection<Clasa> Clase { get; set; } = new List<Clasa>();  
        public ICollection<Media> Medii { get; set; } = new List<Media>();



    }
}
