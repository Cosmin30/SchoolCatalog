using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
namespace SchoolCatalog.Server.Model
{
    public class Clasa

    {
        [Key]
        public int IdClasa { get; set; }
        [Required,
        MinLength(1,ErrorMessage ="Numele clasei este obligatoriu"),
        MaxLength(20,ErrorMessage ="Numele clasei trebuie sa aiba maxim 20 caractere")]
        public string NumeClasa { get; set; } = string.Empty;
        [Required,
        MinLength(1,ErrorMessage = "Profilul clasei este obligatoriu"),
        MaxLength(50,ErrorMessage ="Profilul clasei trebuie sa aiba maxim 50 caractere")]
        public string ProfilClasa { get; set; } = string.Empty;

        public ICollection<Elev>? Elevi { get; set; }
        [ForeignKey(nameof(Orar))]
        public int? IdOrar { get; set; }
        public Orar? Orar { get; set; }
        public ICollection<Materie>? Materii { get; set; }
        public ICollection<Profesor>? Profesori { get; set; }
        public ICollection<Tema>? Teme { get; set; } 


    }
}
